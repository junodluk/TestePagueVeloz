using System;
using System.Collections.Generic;
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
        public async Task<IEnumerable<PeopleResource>> GetPeoples()
        {
            var peoples = await context.Peoples.Include(p => p.Phones).Include(p => p.State).ToListAsync();
            
            return mapper.Map<List<People>, List<PeopleResource>>(peoples);
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