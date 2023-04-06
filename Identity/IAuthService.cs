using Service.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity
{
    public interface IAuthService
    {
        public Task<AuthResponse> Login(AuthRequest request);
        public Task<RegistrationResponse> Registration(RegistrationRequest request);

        public Task<bool> Logout();

    }
}
