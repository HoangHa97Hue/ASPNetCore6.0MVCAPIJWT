using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Entities.Identity
{
    public class RegistrationResponse
    {
        public string UserId { get; set; }

        public KeyValuePair<string, string> ModelStateErrors { get; set; }
    }
}
