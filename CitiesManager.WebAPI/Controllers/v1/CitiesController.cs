using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CitiesManager.WebAPI.DatabaseContext;
using CitiesManager.WebAPI.Models;

namespace CitiesManager.WebAPI.Controllers.v1
{
    [ApiVersion("1.0")]
    public class CitiesController : CustomControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// to get list of cities (with city name and city id ) from 'cities' table
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //[Consumes("application/json")] // response body to json format
        //[Produces("application/json")]// request body to json format
        public async Task<ActionResult<IEnumerable<City>>> GetCitys()
        {
            return await _context.Citys.OrderByDescending(temp => temp.CityName).ToListAsync();
        }

        // GET: api/Cities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<City>> GetCity(Guid id)
        {
            var city = await _context.Citys.FindAsync(id);

            if (city == null)
            {
                return NotFound();
            }

            return city;
        }

        // PUT: api/Cities/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCity(Guid id, [Bind(nameof(city.CityId), nameof(city.CityId))] City city)
        {
            if (id != city.CityId)
            {
                return Problem(detail: "id is invalid", statusCode: 400, title: "search city with id");
                return BadRequest();//400
            }

            var existingCity = await _context.Citys.FindAsync(id);
            if (existingCity == null)
            {
                return NotFound();//404
            }

            existingCity.CityName = city.CityName;
            existingCity.CityId = city.CityId;
            //_context.Entry(city).State = EntityState.Modified;
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

        // POST: api/Cities
        [HttpPost]
        public async Task<ActionResult<City>> PostCity([Bind(nameof(city.CityId), nameof(city.CityId))] City city)
        {
            _context.Citys.Add(city);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetCity", new { id = city.CityId }, city);//api/cities/{ACA31BB6-8438-4339-9379-210F628FFFF0}
        }

        // DELETE: api/Cities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity(Guid id)
        {
            var city = await _context.Citys.FindAsync(id);
            if (city == null)
            {
                return NotFound();
            }

            _context.Citys.Remove(city);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CityExists(Guid id)
        {
            return _context.Citys.Any(e => e.CityId == id);
        }
    }
}
