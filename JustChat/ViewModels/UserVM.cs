﻿using JustChat.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JustChat.ViewModels
{
    public class UserVM
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
