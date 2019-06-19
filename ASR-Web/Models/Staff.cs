using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASR_Web.Models
{
    public class Staff
    {
        [Required, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string StaffID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }
    }
}
