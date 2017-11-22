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
    public class StatesController : Controller
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;

        public StatesController(AppDbContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;
        }

        [HttpGet("/api/states")]
        public async Task<IEnumerable<StateResource>> GetStates()
        {
            var states = await context.States.ToListAsync();
            
            return mapper.Map<List<State>, List<StateResource>>(states);
        }
    }
}