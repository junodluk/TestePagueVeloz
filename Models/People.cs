using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PagueVeloz.Models
{
    [Table("Peoples")]
    public class People
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string Cpf { get; set; }

        public DateTime RegistrationDate { get; set; }

        public DateTime BirthDate { get; set; }

        public State State { get; set; }

        public ICollection<PeoplePhone> Phones { get; set; }

        public People() {
            Phones = new Collection<PeoplePhone>();
        }
    }
}
