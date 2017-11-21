using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PagueVeloz.Models
{    
    [Table("PeoplePhones")]
    public class PeoplePhone
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Phone { get; set; }

        public People People { get; set; }

        public int PeopleId { get; set; }

        public PeoplePhone() {
            
        }
    }
}