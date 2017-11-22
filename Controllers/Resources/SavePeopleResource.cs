using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace PagueVeloz.Controllers.Resources
{
    public class SavePeopleResource
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string Cpf { get; set; }

        [StringLength(255)]
        public string Rg { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        public string StateId { get; set; }

        public ICollection<PeoplePhoneResource> Phones { get; set; }

        public SavePeopleResource()
        {
            Phones = new Collection<PeoplePhoneResource>();
        }
    }
}