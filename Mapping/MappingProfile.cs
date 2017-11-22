using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using PagueVeloz.Controllers.Resources;
using PagueVeloz.Models;

namespace PagueVeloz.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to API Resource
            CreateMap<People, SavePeopleResource>();
            CreateMap<People, PeopleResource>();
            CreateMap<PeoplePhone, PeoplePhoneResource>();
            CreateMap<State, StateResource>();

            // API Resource to Domain
            // Eu poderia fazer uma chamada API apenas para os telefones, mas eu prefiro que o usuário
            // possa editar os telefones da pessoa e clicar em cancelar para descartar todas as edições
            // do que cada vez que que o usuário fizer alguma alteração ela seja imediatamente gravada no banco
            CreateMap<SavePeopleResource, People>()
                .ForMember(p => p.Id, opt => opt.Ignore())
                .ForMember(p => p.StateId, opt => opt.MapFrom(pr => pr.StateId))
                // .ForMember(p => p.Phones, opt => opt.MapFrom(pr => pr.Phones.Select(php => new PeoplePhone{Id = php.Id, PeopleId = pr.Id, Phone = php.Phone})))
                .ForMember(p => p.Phones, opt => opt.Ignore())
                .AfterMap((pr, p) => {
                    var removedPhones = p.Phones.Where(pp => !pr.Phones.Any(prp => prp.Id == pp.Id)).ToList();
                    foreach (var ph in removedPhones)
                        p.Phones.Remove(ph);

                    var addedPhones = pr.Phones.Where(prp => !p.Phones.Any(pp => pp.Id == prp.Id)).ToList();
                    var modifiedPhones = pr.Phones.Where(prp => p.Phones.Any(pp => pp.Id == prp.Id)).ToList();
                    foreach (var mp in modifiedPhones) {
                        var phone = p.Phones.SingleOrDefault(pp => pp.Id == mp.Id);
                        if (phone != null)
                            phone.Phone = mp.Phone;
                    }
                    foreach (var ap in addedPhones)
                        p.Phones.Add(new PeoplePhone{Id = ap.Id, PeopleId = p.Id, Phone = ap.Phone});
                });
        }
    }
}