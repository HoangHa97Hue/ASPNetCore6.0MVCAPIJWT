using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Service.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity
{
    public class CustomSignInManager<T> : SignInManager<T> where T : class
    {
        public CustomSignInManager(UserManager<T> userManager, IHttpContextAccessor contextAccessor, IUserClaimsPrincipalFactory<T> claimsFactory, IOptions<IdentityOptions> optionsAccessor, ILogger<SignInManager<T>> logger, MyDbContext dbContext, IAuthenticationSchemeProvider schemes, IUserConfirmation<T> confirmation)
        : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
        {
        }

    }
}
