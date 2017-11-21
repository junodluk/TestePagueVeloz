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
    public class PeoplesController : Controller
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;

        public PeoplesController(AppDbContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;
        }

        [HttpGet("/api/peoples")]
        public async Task<IEnumerable<PeopleResource>> GetPeoples()
        {
            var peoples = await context.Peoples.Include(p => p.Phones).Include(p => p.State).ToListAsync();
            
            return mapper.Map<List<People>, List<PeopleResource>>(peoples);
        }
    }
}