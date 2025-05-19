using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity;

namespace Grad_WebApp.Models
{
    public class User : IdentityUser
    {
        public int? Year { get; set; }
    }
}
