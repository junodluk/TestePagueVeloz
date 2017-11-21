using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PagueVeloz.Models
{
    [Table("Peoples")]
    public class State
    {
        // public int Id { get; set; }
        [Key]
        [StringLength(10)]
        public string Initials { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public int MinimumAge { get; set; }

        public bool RequireRG { get; set; }

        public State() {
            MinimumAge = 0;
            RequireRG = false;
        }
    }
}