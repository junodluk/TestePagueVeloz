using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagueVeloz.Controllers.Resources;
using PagueVeloz.Models;
using PagueVeloz.Persistence;

namespace PagueVeloz.Controllers
{
    [Route("/api/peoples")]
    public class PeoplesController : Controller
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;

        public PeoplesController(AppDbContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;
        }

        [HttpGet]
        public async Task<QueryResultResource<PeopleResource>> GetPeoples(PeopleQueryResource queryObjResource)
        {
            var queryResult = new QueryResult<People>();
            var queryObj = mapper.Map<PeopleQueryResource, PeopleQuery>(queryObjResource);
            var query = context.Peoples.Include(p => p.Phones).Include(p => p.State).AsQueryable();

            if (queryObj.StateId != null && queryObj.StateId.Length > 0 && queryObj.StateId != "TODOS")
                query = query.Where(p => p.StateId == queryObj.StateId);
            
            if (queryObj.Name != null && queryObj.Name.Length > 0)
                query = query.Where(p => p.Name.Contains(queryObj.Name));
            
            if ((queryObj.RegistrationDateStartString != null && queryObj.RegistrationDateStartString.Length > 0) &&
                (queryObj.RegistrationDateEndString != null && queryObj.RegistrationDateEndString.Length > 0)) {
                
                DateTime startDate = DateTime.ParseExact(queryObj.RegistrationDateStartString, "yyyy-MM-dd HH:mm:ss",
                                       System.Globalization.CultureInfo.InvariantCulture);
                DateTime endDate = DateTime.ParseExact(queryObj.RegistrationDateEndString, "yyyy-MM-dd HH:mm:ss",
                                       System.Globalization.CultureInfo.InvariantCulture);
                query = query.Where(p => p.RegistrationDate >= startDate && p.RegistrationDate < endDate);
            }

            if ((queryObj.BirthDateStartString != null && queryObj.BirthDateStartString.Length > 0) &&
                (queryObj.BirthDateEndString != null && queryObj.BirthDateEndString.Length > 0)) {
                
                DateTime startDate = DateTime.ParseExact(queryObj.BirthDateStartString, "yyyy-MM-dd HH:mm:ss",
                                       System.Globalization.CultureInfo.InvariantCulture);
                DateTime endDate = DateTime.ParseExact(queryObj.BirthDateEndString, "yyyy-MM-dd HH:mm:ss",
                                       System.Globalization.CultureInfo.InvariantCulture);
                query = query.Where(p => p.BirthDate >= startDate && p.BirthDate < endDate);
            }

            var columnsMap = new Dictionary<string, Expression<Func<People, object>>>()
            {
                // ["id"] = p => p.Id,
                ["name"] = p => p.Name,
                ["cpf"] = p => p.Cpf,
                ["birthDate"] = p => p.BirthDate,
                ["state.id"] = p => p.StateId,
                ["registrationDate"] = p => p.RegistrationDate,
                ["lastUpdate"] = p => p.LastUpdate
            };

            if (queryObj.SortBy != null && queryObj.SortBy.Length > 0)
                query = ApplyOrdering(queryObj, query, columnsMap);

            queryResult.TotalItems = await query.CountAsync();

            if (queryObj.Page.HasValue && queryObj.PageSize.HasValue)
                query = query.Skip((queryObj.Page.Value - 1) * queryObj.PageSize.Value).Take(queryObj.PageSize.Value);

            queryResult.Items = await query.ToListAsync();

            return mapper.Map<QueryResult<People>, QueryResultResource<PeopleResource>>(queryResult);
        }

        private IQueryable<People> ApplyOrdering(PeopleQuery queryObj, IQueryable<People> query, Dictionary<string, Expression<Func<People, object>>> columnsMap) {
            if (queryObj.IsSortAscending)
                return query.OrderBy(columnsMap[queryObj.SortBy]);
            else
                return query.OrderByDescending(columnsMap[queryObj.SortBy]);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePeople([FromBody] SavePeopleResource savePeopleResource) {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var state = await context.States.FindAsync(savePeopleResource.StateId);
            if (state == null) {
                ModelState.AddModelError("StateId", "Invalid StateId.");
                return BadRequest(ModelState);
            }

            if (state.RequireRG && (savePeopleResource.Rg == null)) {
                ModelState.AddModelError("RG", "State require RG.");
                return BadRequest(ModelState);
            }
            
            if (state.MinimumAge > 0) {
                var today = DateTime.Today;
                var age = today.Year - savePeopleResource.BirthDate.Year;
                if (savePeopleResource.BirthDate > today.AddYears(-age)) age--;
                if (age < state.MinimumAge) {
                    ModelState.AddModelError("Age", "State require a minimum age of " + state.MinimumAge + " years.");
                    return BadRequest(ModelState);
                }
            }

            var people = mapper.Map<SavePeopleResource, People>(savePeopleResource);
            people.RegistrationDate = DateTime.Now;
            people.LastUpdate = people.RegistrationDate;

            context.Peoples.Add(people);
            await context.SaveChangesAsync();

            var result = mapper.Map<People, PeopleResource>(people);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePeople(int id, [FromBody] SavePeopleResource savePeopleResource) {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var people = await context.Peoples.Include(p => p.Phones).Include(p => p.State).SingleOrDefaultAsync(p => p.Id == id);
            if (people == null)
                return NotFound();

            var state = await context.States.FindAsync(savePeopleResource.StateId);
            if (state == null) {
                ModelState.AddModelError("StateId", "Invalid StateId.");
                return BadRequest(ModelState);
            }

            if (state.RequireRG && (savePeopleResource.Rg == null)) {
                ModelState.AddModelError("RG", "State require RG.");
                return BadRequest(ModelState);
            }
            
            if (state.MinimumAge > 0) {
                var today = DateTime.Today;
                var age = today.Year - savePeopleResource.BirthDate.Year;
                if (savePeopleResource.BirthDate > today.AddYears(-age)) age--;
                if (age < state.MinimumAge) {
                    ModelState.AddModelError("Age", "State require a minimum age of " + state.MinimumAge + " years.");
                    return BadRequest(ModelState);
                }
            }

            people.LastUpdate = DateTime.Now;
            mapper.Map<SavePeopleResource, People>(savePeopleResource, people);

            await context.SaveChangesAsync();

            var result = mapper.Map<People, PeopleResource>(people);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePeople(int id) {
            var people = await context.Peoples.FindAsync(id);

            if (people == null)
                return NotFound();

            context.Remove(people);
            await context.SaveChangesAsync();

            return Ok(id);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPeople(int id) {
            var people = await context.Peoples.Include(p => p.Phones).Include(p => p.State).SingleOrDefaultAsync(p => p.Id == id);

            if (people == null)
                return NotFound();

            var peopleResource = mapper.Map<People, PeopleResource>(people);

            return Ok(peopleResource);
        }
    }
}