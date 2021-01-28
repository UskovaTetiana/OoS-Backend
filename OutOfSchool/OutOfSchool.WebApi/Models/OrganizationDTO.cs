using OutOfSchool.WebApi.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OutOfSchool.WebApi.Models
{
    public class OrganizationDTO
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public string Description { get; set; }

        [Required]
        [MaxLength(20)]
        public string EDRPOU { get; set; }

        [Required]
        [MaxLength(20)]
        public string MFO { get; set; }

        [Required]
        [MaxLength(20)]
        public string INPP { get; set; }

        [Required]
        public OrganizationType Type { get; set; }
    }
}
