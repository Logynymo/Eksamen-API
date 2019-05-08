using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Eksamen_API.Models;

namespace Eksamen_API.Controllers
{
    /// <summary>
    /// Defines Controller/Routes Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ZipCitiesController : ControllerBase
    {
        /// <summary>
        /// Private readonly field containing data from context.
        /// </summary>
        private readonly ZipCityContext _context;
        /// <summary>
        /// Constructor with an implicit conversion of context to _context.
        /// </summary>
        /// <param name="context">ZipCityContext's Context</param>
        public ZipCitiesController(ZipCityContext context)
        {
            _context = context;
        }

        /// <summary>
        /// HTTPGet Request.
        /// async Task<ActionResult<IEnumerable<ZipCity>>> called GetZipCities
        /// </summary>
        /// <returns>Returns ZipCity as a List Asynchronous</returns>
        // GET: api/ZipCities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ZipCity>>> GetZipCities()
        {
            return await _context.ZipCity.ToListAsync();
        }

        /// <summary>
        /// HTTPGET Request on "id".
        /// Creates a new variable called zipCity,
        /// containing ZipCity.FindAsync which find an entity with the given value key.
        /// </summary>
        /// <param name="id">String id</param>
        /// <returns>If zipCity is null it will return code 404 on the api page.
        /// but if it is NOT null it will return zipCity with its data.</returns>
        // GET: api/ZipCities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ZipCity>> GetZipCity(string id)
        {
            var zipCity = await _context.ZipCity.FindAsync(id);

            if (zipCity == null)
            {
                return NotFound();
            }

            return zipCity;
        }

        /// <summary>
        /// HTTPPut Request on "id".
        /// Public Async Task called PutZipCity.
        /// if id is NULL in zipCity.Id, it will return Code 400 (Bad Request).
        /// then _context.Entry(zipCity).State tells the compiler what state it is being tracked in.
        /// Then there is a Try that saves the changes asynchronous, and then a catch that throws an exception 
        /// if ZipCity doesn't exist in the DB.
        /// </summary>
        /// <param name="id">string with id</param>
        /// <param name="zipCity">zipCity from ZipCity</param>
        /// <returns>Returns 204 no content code.</returns>
        // PUT: api/ZipCities/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutZipCity(string id, ZipCity zipCity)
        {
            if (id != zipCity.Id)
            {
                return BadRequest();
            }

            _context.Entry(zipCity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ZipCityExists(id))
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
        /// HTTPGet Request, requesting the api data from citysearch/cityname.
        /// </summary>
        /// <param name="cityname">String cityname</param>
        /// <returns>Returns a range variable (z) in _context.ZipCity where EF.Functions.Like(z.CityName, $"{cityname}%")
        /// select z) and then puts it in the list asynchronous.</returns>
        //Get: API/zipcities/citySearch/{cityname}
        [HttpGet("citysearch/{cityname}")]
        public async Task<ActionResult<IEnumerable<ZipCity>>> GetZipCitiesByName(string cityname)
        {
            return await (from z in _context.ZipCity
                          where EF.Functions.Like(z.CityName, $"{cityname}%")
                          select z).ToListAsync();
        }

        /// <summary>
        /// HTTPPost Request.
        /// zipCity gets added to the context of ZipCity.
        /// it then saves the changes in context with an await.
        /// </summary>
        /// <param name="zipCity">ZipCity's zipCity</param>
        /// <returns>Returns Status code 201, with zipCity's Id and zipCity itself.</returns>
        // POST: api/ZipCities
        [HttpPost]
        public async Task<ActionResult<ZipCity>> PostZipCity(ZipCity zipCity)
        {
            _context.ZipCity.Add(zipCity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetZipCity", new { id = zipCity.Id }, zipCity);
        }

        /// <summary>
        /// HTTPDelete Request, requesting "id".
        /// Deletes what the user wants to delete.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/ZipCities/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ZipCity>> DeleteZipCity(string id)
        {
            var zipCity = await _context.ZipCity.FindAsync(id);
            if (zipCity == null)
            {
                return NotFound();
            }

            _context.ZipCity.Remove(zipCity);
            await _context.SaveChangesAsync();

            return zipCity;
        }

        /// <summary>
        /// Boolean called ZipCityExists.
        /// </summary>
        /// <param name="id">string id</param>
        /// <returns>Checks if Id == id, which means that if ZipCity exists in DB everything is peaceful.</returns>
        private bool ZipCityExists(string id)
        {
            return _context.ZipCity.Any(e => e.Id == id);
        }
    }
}
