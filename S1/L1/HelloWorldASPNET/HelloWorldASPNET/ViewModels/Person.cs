using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HelloWorldASPNET.ViewModels
{
    public class Person
    {
        [HiddenInput]
        public int PersonId { get; set; }

        [Required]
        public string Name { get; set; }
        public Gender Gender { get; set; }
    }

    public enum Gender
    {
        Male,
        Female
    }
}
