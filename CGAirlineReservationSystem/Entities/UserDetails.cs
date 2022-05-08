using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CGAirlineReservationSystem.Entities
{
    public class UserDetails
    {
        [ForeignKey("Users")]
        [Column(TypeName = "int")]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar")]
        [StringLength(40)]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "nvarchar")]
        [MinLength(10), MaxLength(13)]
        public string ContactNo { get; set; }

        [Key]
        [Column(TypeName = "nvarchar")]
        [StringLength(40)]
        public string Email { get; set; }

    }
}
