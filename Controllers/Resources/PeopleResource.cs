using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace PagueVeloz.Controllers.Resources
{
    public class PeopleResource
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Cpf { get; set; }

        public string Rg { get; set; }

        public DateTime RegistrationDate { get; set; }
        
        public DateTime LastUpdate { get; set; }

        public DateTime BirthDate { get; set; }

        public StateResource State { get; set; }

        public ICollection<PeoplePhoneResource> Phones { get; set; }

        public PeopleResource()
        {
            Phones = new Collection<PeoplePhoneResource>();
        }
    }
}