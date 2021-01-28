using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OutOfSchool.DAL.Models
{
    public class Parent
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter your name")]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter your name")]
        [MaxLength(20)]
        public string MiddleName { get; set; }
        
        [Required(ErrorMessage = "Please enter your name")]
        [MaxLength(20)]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Please enter your name")]
        [MaxLength(15)]
        public string Phone { get; set; }
    }
}
