using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CitiesAPI.Models;
using CitiesAPI.Migration;
using CitiesAPI.Helpers;

namespace CitiesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly CityContext _context;

        public CitiesController(CityContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<City>>> GetCities()
        {
            return await _context.Cities.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<City>> GetCity(int id)
        {
            var city = await _context.Cities.FindAsync(id);

            if (city == null)
            {
                return NotFound();
            }

            return city;
        }

        [HttpGet("getbyname/{name}")]
        public ActionResult<IEnumerable<City>> GetCityByName(string name)
        {
            var cities = _context.Cities.
                Where(e => e.Name.ToLower().Contains(name.ToLower())).ToList();

            if (cities == null || !cities.Any())
            {
                return NotFound();
            }

            return cities;
        }

        [HttpGet("getbybordername/{name}")]
        public ActionResult<IEnumerable<City>> GetCityByBorderName(string name)
        {
            var cities = _context.Cities.ToList().
                Where(e => e.Borders.Any(b => b.ToLower().Contains(name.ToLower()))).
                ToList();

            if (cities == null || !cities.Any())
            {
                return NotFound();
            }

            return cities;
        }

        [HttpGet("gettotaldata")]
        public ActionResult<TotalData> GetTotalData()
        {
            var cities = _context.Cities.ToList();

            if (cities == null || !cities.Any())
            {
                return NotFound();
            }

            var totalPopulation = cities.Sum(c => c.Population);
            var citiesCount = cities.Count;

            return new TotalData() { TotalPopulation = totalPopulation, CitiesCount = citiesCount };
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCity(int id, City city)
        {
            if (id != city.Id)
            {
                return BadRequest();
            }

            _context.Entry(city).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<City>> PostCity(City city)
        {
            try
            {
                city.Validate();

                _context.Cities.Add(city);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetCity), new { id = city.Id }, city);
            }
            catch (InvalidOperationException e)
            {
                return Problem(title: "Erro ao cadastrar usuário!", detail: e.Message, statusCode: 400);
            }

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<City>> DeleteCity(int id)
        {
            var city = await _context.Cities.FindAsync(id);
            if (city == null)
            {
                return NotFound();
            }

            _context.Cities.Remove(city);
            await _context.SaveChangesAsync();

            return city;
        }

        private bool CityExists(int id)
        {
            return _context.Cities.Any(e => e.Id == id);
        }

        [HttpGet("/run")]
        public ActionResult RunInitialMigration()
        {            
            InitialMigration migration = new InitialMigration(_context);
            migration.CreateCities();
            return Redirect("/api/cities");
        }
    }
}
