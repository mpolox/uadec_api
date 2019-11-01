using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace uadec.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string LastName { get; set; }

        public string LastNameMother { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 10)]
        public string Email { get; set; }

        public string Phone { get; set; }
    }
}
