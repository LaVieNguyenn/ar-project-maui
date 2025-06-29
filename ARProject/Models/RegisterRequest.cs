using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARProject.Models
{
    public class RegisterRequest
    {
        public string Email { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public DateOnly Birthday { get; set; }

        public string RoleId { get; set; } = "661fcf75e40000551e02a001";

        public int Gender { get; set; }

        public double Height { get; set; }

        public double Weight { get; set; }
    }
}
