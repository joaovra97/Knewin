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
    [Route("api/cities")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly CityContext _context;

        public CitiesController(CityContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Lista todas as cidades
        /// </summary>  
        /// <remarks>
        /// Apresenta todas as cidades cadastradas no banco.
        /// </remarks>
        /// <response code="200">Retorna a lista de cidades caso haja</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<City>>> GetCities()
        {
            return await _context.Cities.ToListAsync();
        }

        /// <summary>
        /// Lista uma cidade pelo seu id
        /// </summary>  
        /// <remarks>
        /// Lista uma cidade específica pelo seu id.
        /// </remarks>
        /// <param name="id"></param>
        /// <response code="200">Retorna a cidade específica</response>
        /// <response code="404">Mensagem padrão de erro do ASP.NET Core</response>
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

        /// <summary>
        /// Lista cidades pelo seu nome
        /// </summary>  
        /// <remarks>
        /// Lista cidades baseado na busca pelo nome.
        /// </remarks>
        /// <param name="name"></param>
        /// <response code="200">Retorna a lista de cidades</response>
        /// <response code="404">Mensagem padrão de erro do ASP.NET Core</response>
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

        /// <summary>
        /// Lista cidades pelo nome de suas fronteiras
        /// </summary>  
        /// <remarks>
        /// Lista cidades baseado na busca pelo nome de suas fronteiras.
        /// </remarks>
        /// <param name="name"></param>
        /// <response code="200">Retorna a lista de cidades</response>
        /// <response code="404">Mensagem padrão de erro do ASP.NET Core</response>
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

        /// <summary>
        /// Apresenta informações gerais do total de cidades
        /// </summary>  
        /// <remarks>
        /// Apresenta o total de cidades cadastrados no banco e o total de habitantes nessas cidades.
        /// </remarks>
        /// <response code="200">Retorna o total de cidades e o total de habitantes</response>
        /// <response code="404">Mensagem padrão de erro do ASP.NET Core</response>
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

        /// <summary>
        /// Edita uma cidade
        /// </summary>  
        /// <remarks>
        /// Edita uma cidade já existente.
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="city"></param>
        /// <response code="204">Editado</response>
        /// <response code="404">Mensagem padrão de erro do ASP.NET Core</response>
        /// <response code="400">Mensagem padrão de erro do ASP.NET Core</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCity(int id, City city)
        {
            if (id != city.Id)
            {
                return BadRequest();
            }

            try
            {
                city.Validate();
            }
            catch (InvalidOperationException e)
            {
                return Problem(title: "Erro ao editar usuário!", detail: e.Message, statusCode: 400);
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

        /// <summary>
        /// Insere uma cidade
        /// </summary>  
        /// <remarks>
        /// Insere uma nova cidade.
        /// </remarks>
        /// <param name="city"></param>
        /// <response code="200">Retorna a cidade cadastrada e seu id</response>
        /// <response code="404">Mensagem padrão de erro do ASP.NET Core</response>
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

        /// <summary>
        /// Remove uma cidade
        /// </summary>  
        /// <remarks>
        /// Remove uma cidade específica pelo seu id.
        /// </remarks>
        /// <param name="id"></param>
        /// <response code="200">Retorna o item deletado</response>
        /// <response code="404">Mensagem padrão de erro do ASP.NET Core</response>
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
        [ApiExplorerSettings(IgnoreApi = true)]
        public ActionResult RunInitialMigration()
        {            
            InitialMigration migration = new InitialMigration(_context);
            migration.CreateCities();
            return Redirect("/swagger");
        }
    }
}
