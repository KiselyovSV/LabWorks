﻿using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Grad_WebApp.ViewModels
{
    public class CreateUserViewModel
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int Year { get; set; }
    }
}
