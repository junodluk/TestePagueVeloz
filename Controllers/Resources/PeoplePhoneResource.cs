using System.ComponentModel.DataAnnotations;

namespace PagueVeloz.Controllers.Resources
{
    public class PeoplePhoneResource
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Phone { get; set; }

        // [Required]
        public int PeopleId { get; set; }

        public PeoplePhoneResource() {
            
        }
    }
}