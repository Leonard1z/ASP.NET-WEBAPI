using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SuperHeroAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SuperHeroController : ControllerBase
    {

        // private static List<SuperHero> heroes = new List<SuperHero>
        //     {
        //         new SuperHero{
        //             Id=1,
        //             Name="Spider Man",
        //             FirstName ="Peter",
        //             LastName="Parker",
        //             Place="New York City",
        //         },
        //         new SuperHero{
        //             Id=2,
        //             Name="Iron Man",
        //             FirstName ="Tony",
        //             LastName="Stark",
        //             Place="New York City",
        //         }
        //     };

        private readonly DataContext _dataContext;
        public SuperHeroController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
            return Ok(await _dataContext.SuperHeroes.ToListAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> Get(int id)
        {
            var hero = await _dataContext.SuperHeroes.FindAsync(id);
            if (hero == null)
            {
                return BadRequest("Hero with this id does not exist");
            }
            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            _dataContext.SuperHeroes.Add(hero);
            await _dataContext.SaveChangesAsync();
            return Ok(await _dataContext.SuperHeroes.ToListAsync());
        }
        [HttpPut]
        public async Task<ActionResult<SuperHero>> UpdateHero(SuperHero hero)
        {
            var dbHero = await _dataContext.SuperHeroes.FindAsync(hero.Id);
            if (dbHero == null)
            {
                return BadRequest("Hero not found");
            }

            dbHero.Name = hero.Name;
            dbHero.FirstName = hero.FirstName;
            dbHero.LastName = hero.LastName;
            dbHero.Place = hero.Place;

            await _dataContext.SaveChangesAsync();

            return Ok(dbHero);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<SuperHero>> DeleteHero(int id)
        {
            var dbHero = await _dataContext.SuperHeroes.FindAsync(id);
            if (dbHero == null)
            {
                return BadRequest("hero not found");
            }
            _dataContext.SuperHeroes.Remove(dbHero);
            await _dataContext.SaveChangesAsync();
            return Ok(await _dataContext.SuperHeroes.ToListAsync());
        }

    }
}