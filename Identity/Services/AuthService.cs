using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Service.Entities.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Services
{
    public class AuthService : IAuthService
    {
        private readonly CustomSignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtSettings _jwtSettings;

        public AuthService(UserManager<ApplicationUser> userManager, IOptions<JwtSettings> jwtSettings, CustomSignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public async Task<AuthResponse> Login(AuthRequest request)
        {
            //thuong la ho se querry theo 1 cach nua la check DBContext.Bang Identity .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                throw new NotFoundException($"User with {request.Email} not found."); 
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (result.Succeeded == false)
            {
                throw new BadRequestException($"Credentials for '{request.Email} aren't valid.");
            }
            //test
            //var password = await _userManager.CheckPasswordAsync(user, request.Password);
            //var result2 = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, isPersistent: false, false);

            //await _signInManager.SignInAsync(user, isPersistent: false);

            JwtSecurityToken jwtSecurityToken = await GenerateToken(user);

            //initialize auth response 
            var response = new AuthResponse
            {
                Id = user.Id,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Email = user.Email,
                UserName = user.UserName,
            };
            return response;
        }

        private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
        {
            //retrieve the user claims
            //claims is the key value pairs that tell you information about the user
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            //claims are used to determine your permissions in the client app
            var roleClaims = roles.Select(q => new Claim(ClaimTypes.Role, q)).ToList();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName), // sub is a standard for JWT authen and is usualuu sth to indicate User
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // no giong ma Nonce
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id) // customs claims 
            }
            .Union(userClaims)
            .Union(roleClaims);//ket hop lai tat ca vao 1 cai Claims , chua tat ca thong tin User

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            //symmetric key is a piece of metal that has been cut into a special shape and is used for opening or closing a lock 

            //hash 
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }

        public virtual async Task<RegistrationResponse> Registration(RegistrationRequest request)
        {
            var user = new ApplicationUser
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                //Add neu role chua ton tai 
                string[] roleNames = { "Admin", "Member" };
                // Add the Admin role to the database
                IdentityResult roleResult;
                foreach (var roleName in roleNames)
                {
                    bool adminRoleExists = await _roleManager.RoleExistsAsync(roleName);
                    if (!adminRoleExists)
                    {
                        roleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));
                    }

                }

                //cho nay tuy business ==> nen de cho User ho chi dang ky Member , ko duoc dang ky Admins
                //await _userManager.AddToRoleAsync(user, "Member"); //doan nay chia truong hop ra => Admin hoac User ,
                var addRoleResult =  await _userManager.AddToRoleAsync(user, request.Role); //doan nay chia truong hop ra => Admin hoac User , 

                //if (_userManager.Options.SignIn.RequireConfirmedAccount)
                //{
                //    //confirm email before login
                //    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                //    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                //    //Send email 
                //    var verifyLink = Url.ActionLink(action: "ConfirmEmail", controller: "Account",
                //        values: new { userID = user.Id, code });

                //    TempData["verifyLink"] = verifyLink;

                //    return View();
                //}

                RegistrationResponse registrationResponse = new RegistrationResponse();
                //foreach (var item in addRoleResult.Errors)
                //{
                //    registrationResponse.ModelStateErrors. Add(item.Code, item.Description);
                //}
                registrationResponse.UserId = user.Id;
                return registrationResponse;
                //return new RegistrationResponse() { UserId = user.Id };
            }
            else
            {
                StringBuilder str = new StringBuilder();    
                foreach(var err in result.Errors)
                {
                    str.AppendFormat("*{0}\n", err.Description);
                }
                throw new BadRequestException($"{str}");
            }
        }


        public async Task<bool> Logout()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
