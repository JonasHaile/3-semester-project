﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SurfApi.Data;
using SurfApi.Models;

namespace SurfApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RentalsApiController : ControllerBase
    {
        private readonly SurfApiContext _context;

        public RentalsApiController(SurfApiContext context)
        {
            _context = context;
        }

        // GET: RentalsAPI/Boards
        [HttpGet("Boards")]
        public async Task<ActionResult<IEnumerable<Surfboard>>> GetAllBoards()
        {

            if (!_context.Surfboard.Any())
            {
                return NoContent();
            }
            else
            { return Ok(await _context.Surfboard.ToListAsync()); }
        }

        [HttpGet("Rentals")]
        public async Task<ActionResult<IEnumerable<Rental>>> GetAllRentals()
        {

            if (!_context.Rental.Any())
            {
                return NoContent();
            }
            else
            { return Ok(await _context.Rental.ToListAsync()); }
        }


        //GET: api/API/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Surfboard>> GetSurfboard(int id)
        {
            var surfboard = await _context.Surfboard.FindAsync(id);

            if (surfboard == null)
            {
                return NotFound();
            }

            return Ok(surfboard);
        }

        //// PUT: api/RentalsApi/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutRental(int id, Rental rental)
        //{
        //    if (id != rental.RentalId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(rental).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!RentalExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/RentalsApi
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Rental>> PostRental(Rental rental)
        //{
        //  if (_context.Rental == null)
        //  {
        //      return Problem("Entity set 'SurfApiContext.Rental'  is null.");
        //  }
        //    _context.Rental.Add(rental);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetRental", new { id = rental.RentalId }, rental);
        //}

        // POST: api/API
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Surfboard>> Post([FromBody]Rental rental)
        {
            rental.Surfboard = await _context.Surfboard.FirstOrDefaultAsync(s => s.ID == rental.SurfboardId);
            var rentalExist = await _context.Rental.FirstOrDefaultAsync(s => s.SurfboardId == rental.SurfboardId);
             


            if (rental.Surfboard != null && rental.UserId != null && (rentalExist == null || (rentalExist.StartDate > rental.EndDate || rentalExist.EndDate < rental.StartDate)))
            {

                await _context.Rental.AddAsync(rental);
                await _context.SaveChangesAsync();
                
            }
            else
            {
                return NotFound("Surfboard not available");
            }
            return Ok(rental.Surfboard);
        }

        //// DELETE: api/RentalsApi/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteRental(int id)
        //{
        //    if (_context.Rental == null)
        //    {
        //        return NotFound();
        //    }
        //    var rental = await _context.Rental.FindAsync(id);
        //    if (rental == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Rental.Remove(rental);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool RentalExists(int id)
        //{
        //    return (_context.Rental?.Any(e => e.RentalId == id)).GetValueOrDefault();
        //}
    }
}
