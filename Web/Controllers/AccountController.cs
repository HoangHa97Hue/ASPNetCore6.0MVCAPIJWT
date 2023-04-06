using Identity;
using Identity.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Service.Entities.Identity;
using Service.OtherClass;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static Core.Constants.Common;

namespace ToDoApp.MVC.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private CustomSignInManager<ApplicationUser> _signInManager;
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private readonly IAuthService _authService;

        public AccountController(CustomSignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
            IAuthService authService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _authService = authService;
        }
        [AllowAnonymous]
        public IActionResult Login()
        {
            AuthRequest authRequest = new AuthRequest();
            return View(authRequest);
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(AuthRequest authRequest,
            [FromQuery(Name = "UrlReturn")] string? urlReturn)
        {
            AuthResponse authResponse = null;
            try
            {
                authResponse = await _authService.Login(authRequest);

                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);

                //get user from claim 
                string jwtToken = authResponse.Token;
                JwtSecurityToken token = new JwtSecurityTokenHandler().ReadJwtToken(jwtToken);

                var userRoles = token.Claims.Where(x => x.Type == ClaimTypes.Role).ToList();
                var userID = token.Claims.Where(x => x.Type == "uid").FirstOrDefault().Value;
                var user = await _userManager.FindByIdAsync(userID.ToString());

                identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName ?? string.Empty));
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id ?? string.Empty));

                //Cho nay can UT test cho 1 list chua nhieu role, hoac mocai bang 1 role
                identity.AddClaim(new Claim(ClaimTypes.Role, userRoles.FirstOrDefault().Value ?? string.Empty));

                //Add claim for token to access in MealCategoryController to acess api MealCategoriesController
                identity.AddClaim(new Claim("jwtToken", jwtToken));


                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    principal,
                    new AuthenticationProperties
                    {
                        IsPersistent = true,
                        AllowRefresh = true,
                        ExpiresUtc = DateTime.UtcNow.AddMinutes(20)
                    });

                var boolCheck = User.Identity.IsAuthenticated;

                //// Set current principal ==> setting to access claimidentity in another controler
                //Thread.CurrentPrincipal = principal;


                if (!string.IsNullOrEmpty(urlReturn))
                {
                    return Redirect(urlReturn);
                }
                return Redirect("/"); //route default = home ma minh da config
            }
            catch (NotFoundException e)
            {
                ModelState.AddModelError("NotFoundException", e.Message);
            }
            catch (BadRequestException b)
            {
                ModelState.AddModelError("BadRequestException", b.Message);
            }
            catch 
            {
                ModelState.AddModelError("LoginFailed", "Your input data is incorrect");
            }

            ModelState.AddModelError("LoginFailed", "Your UserName or Password is not corret");

            return View();          
        }

        public async Task<IActionResult> Logout()
        {
        //    await _signInManager.SignOutAsync();
            var identity = User.Identity as ClaimsIdentity;
            var claimNameList = identity.Claims.Select(x => x.Type).ToList();

            foreach (var name in claimNameList)
            {
                var claim = identity.Claims.FirstOrDefault(x => x.Type == name);
                identity.RemoveClaim(claim);
            }

            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login");
        }

        [AllowAnonymous]
        public async Task<IActionResult> SignUp()
        {
            RegisterViewModel registrationViewModel = new RegisterViewModel();
            registrationViewModel.RegistrationRequest = new RegistrationRequest();
            registrationViewModel.RoleListToSelect.Add(new SelectListItem { Text = Roles.Member, Value = Roles.Member });
            registrationViewModel.RoleListToSelect.Add(new SelectListItem { Text = Roles.Admin, Value = Roles.Admin });

            //C2 dung View bag
            //RegistrationRequest registrationRequest = new RegistrationRequest();
            //await HttpContext.SignOutAsync();
            //List<SelectListItem> rolesListItem = new List<SelectListItem>();

            //rolesListItem.Add(new SelectListItem { Text = Roles.Member, Value = Roles.Member });
            //rolesListItem.Add(new SelectListItem { Text = Roles.Admin, Value = Roles.Admin });
            //ViewBag.RolesListItem = rolesListItem;
            return View(registrationViewModel);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> SignUp(RegisterViewModel registrationViewModel)
        {
            RegistrationRequest registrationRequest = registrationViewModel.RegistrationRequest;
            registrationRequest.Role = registrationViewModel.RoleSelected;
            RegistrationResponse regisResponse = null;
            try
            {
                regisResponse = await _authService.Registration(registrationRequest);

                if (!string.IsNullOrEmpty(regisResponse.UserId))
                {
                    return Redirect("/"); //return ve trang chu 
                    //}
                }
            }
            catch (BadRequestException b)
            {
                ModelState.AddModelError("BadRequestException", b.Message);
            }
            catch(Exception e)
            {
                ModelState.AddModelError("RegisFailed", "Something happend wrong");
                ModelState.AddModelError("ErrosOrigin", e.Message);
            }
            return View();
        }
        //[AllowAnonymous]
        //public async Task<IActionResult> ConfirmEmail(string userID, string code)
        //{
        //    var codeDecoded = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
        //    var user = await _userManager.FindByIdAsync(userID);

        //    var result = await _userManager.ConfirmEmailAsync(user, codeDecoded);

        //    if (result.Succeeded)
        //    {
        //        await _signInManager.SignInAsync(user, isPersistent: false);
        //        return Redirect("/");//return ve trang chu 
        //    }

        //    foreach (var item in result.Errors)
        //    {
        //        ModelState.AddModelError(item.Code, item.Description);
        //    }

        //    return View("SignUp");
        //}
    }
}
