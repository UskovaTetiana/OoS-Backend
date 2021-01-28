using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OutOfSchool.WebApi.Models
{
    public class ParentDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter your name")]
        [RegularExpression("^([А-Я]{1}[а-яё]{1,23}|[A-Z]{1}[a-z]{1,23})$", ErrorMessage = "Regex")]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter your middlename")]
        [RegularExpression("^([А-Я]{1}[а-яё]{1,23}|[A-Z]{1}[a-z]{1,23})$", ErrorMessage = "Regex")]
        [MaxLength(20)]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Please enter your surname")]
        [RegularExpression("^([А-Я]{1}[а-яё]{1,23}|[A-Z]{1}[a-z]{1,23})$", ErrorMessage = "Regex")]
        [MaxLength(20)]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Please enter your phone")]
        [RegularExpression(@"(^\+\d{1,2})?((\(\d{3}\))|(\-?\d{3}\-)|(\d{3}))((\d{3}\-\d{4})|(\d{3}\-\d\d\ -\d\d)|(\d{7})|(\d{3}\-\d\-\d{3}))", ErrorMessage = "Regex")]
        [MaxLength(15)]
        public string Phone { get; set; }
    }
}
