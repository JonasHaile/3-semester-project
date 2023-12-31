﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Surf.Areas.Identity.Data;
using Surf.Data;
using Surf.Models;

namespace Surf.Controllers
{
    public class UserController : Controller
    {
        private readonly SurfDbContext _context;
        private readonly UserManager<ApplicationUser> _usermananger;

        public UserController(SurfDbContext context, UserManager<ApplicationUser> usermanager)
        {
            _context = context;
            _usermananger = usermanager;

        }

        // GET: User
        public async Task<IActionResult> Index(
    string sortOrder,
    string currentFilter,
    string searchString,
    int? pageNumber)
        {


            // Sort
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = sortOrder == "Name" ? "name_desc" : "Name";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["LengthSortParm"] = sortOrder == "Length" ? "length_desc" : "Length";
            ViewData["WidthSortParm"] = sortOrder == "Width" ? "width_desc" : "Width";
            ViewData["ThicknessSortParm"] = sortOrder == "Thickness" ? "thickness_desc" : "Thickness";
            ViewData["VolumeSortParm"] = sortOrder == "Volume" ? "volume_desc" : "Volume";
            ViewData["TypeSortParm"] = sortOrder == "Type" ? "type_desc" : "Type";
            ViewData["PriceSortParm"] = sortOrder == "Price" ? "price_desc" : "Price";
            ViewData["EquipmentSortParm"] = sortOrder == "Equipment" ? "equipment_desc" : "Equipment";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            // Retrieve all surfboards from _context local database
            //And storing in the 'surfboards' variable
            //var user = await _usermananger.GetUserAsync(User);
            //var unavailableDate = DateTime.Now.AddDays(5);
            //var surfboards = from s in _context.Surfboard
            //                 where !_context.Rental.Any(r => r.SurfboardId == s.ID) ||
            //                       _context.Rental.Any(r => r.SurfboardId == s.ID && (DateTime.Now > r.EndDate || unavailableDate < r.StartDate)) &&
            //                       (User != null && _context.Rental.Any(r => r.UserId == user.Id))
            //                 select s;

            var user = await _usermananger.GetUserAsync(User);
            var unavailableDate = DateTime.Today.AddDays(5);
            var surfboards = from s in _context.Surfboard
                             where !_context.Rental.Any(r => r.SurfboardId == s.ID) ||
                                   (_context.Rental.Any(r => r.SurfboardId == s.ID && (DateTime.Today > r.EndDate || unavailableDate < r.StartDate)) ||
                                    (user != null && _context.Rental.Any(r => r.SurfboardId == s.ID && r.UserId == user.Id)))
                             select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                surfboards = surfboards.Where(s => s.Name.Contains(searchString)
                                       || s.Type.Contains(searchString)
                                       || s.Equipment.Contains(searchString)
                                       || s.Length.ToString().Contains(searchString)
                                       || s.Width.ToString().Contains(searchString)
                                       || s.Thickness.ToString().Contains(searchString)
                                       || s.Volume.ToString().Contains(searchString)
                                       || s.Price.ToString().Contains(searchString));
            }

            //RentalCheck(); // tjekker om surfboardet er udlejet eller ej. 
            //sort
            switch (sortOrder)
            {
                case "Name":
                    surfboards = surfboards.OrderBy(s => s.Name);
                    break;
                case "name_desc":
                    surfboards = surfboards.OrderByDescending(s => s.Name);
                    break;
                case "Length":
                    surfboards = surfboards.OrderBy(s => s.Length);
                    break;
                case "length_desc":
                    surfboards = surfboards.OrderByDescending(s => s.Length);
                    break;
                case "Width":
                    surfboards = surfboards.OrderBy(s => s.Width);
                    break;
                case "width_desc":
                    surfboards = surfboards.OrderByDescending(s => s.Width);
                    break;
                case "Thickness":
                    surfboards = surfboards.OrderBy(s => s.Thickness);
                    break;
                case "thickness_desc":
                    surfboards = surfboards.OrderByDescending(s => s.Thickness);
                    break;
                case "Volume":
                    surfboards = surfboards.OrderBy(s => s.Volume);
                    break;
                case "volume_desc":
                    surfboards = surfboards.OrderByDescending(s => s.Volume);
                    break;
                case "Type":
                    surfboards = surfboards.OrderBy(s => s.Type);
                    break;
                case "type_desc":
                    surfboards = surfboards.OrderByDescending(s => s.Type);
                    break;
                case "Price":
                    surfboards = surfboards.OrderBy(s => s.Price);
                    break;
                case "price_desc":
                    surfboards = surfboards.OrderByDescending(s => s.Price);
                    break;
                case "Equipment":
                    surfboards = surfboards.OrderBy(s => s.Equipment);
                    break;
                case "equipment_desc":
                    surfboards = surfboards.OrderByDescending(s => s.Equipment);
                    break;
            }

            int pageSize = 5;
            return View(await PaginatedList<Surfboard>.CreateAsync(surfboards.AsNoTracking(), pageNumber ?? 1, pageSize));
        }


        // GET: User/Edit/5
        [Authorize]
        public async Task<IActionResult> Create(int? id) // Rental
        {
            if (id == null || _context.Surfboard == null)
            {
                return NotFound();
            }

            var surfboard = await _context.Surfboard.FindAsync(id);
            if (surfboard == null)
            {
                return NotFound();
            }
            return View(surfboard);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DateTime startDate, [Bind("ID")] Surfboard surfboard)
        {

            var surfboardRental = await _context.Surfboard.FirstOrDefaultAsync(s => s.ID == surfboard.ID);
            var user = await _usermananger.GetUserAsync(User);
            var rental = await _context.Rental.FirstOrDefaultAsync(s => s.SurfboardId == surfboard.ID);
            var enddate = startDate.AddDays(5);


            if (surfboardRental != null && User != null && (rental == null || (rental.StartDate > enddate || rental.EndDate < startDate)))
            {
                var rentalToBeMade = new Rental
                {
                    SurfboardId = surfboardRental.ID,
                    UserId = user.Id,
                    StartDate = startDate,
                    EndDate = enddate
                };
                if (startDate == DateTime.Today)
                {
                    surfboardRental.IsRented = true;
                }


                await _context.Rental.AddAsync(rentalToBeMade);
                await _context.SaveChangesAsync();

                TempData["SuccesMessage"] = $"Surfboard is now rented from {startDate.ToShortDateString()} to {enddate.ToShortDateString()}";
            }
         else
            {
                ModelState.AddModelError(string.Empty, "Surfboard not available");
            }

            return View(surfboardRental);
        }

        private async void RentalCheck()
        {
            var rental = _context.Rental.Where(r => r.StartDate < DateTime.Now).ToList();
            if (rental != null || rental.Any())
            {
                foreach (var r in rental)
                {
                    var surfboard = await _context.Surfboard.FirstOrDefaultAsync(s => s.ID == r.SurfboardId);
                    if (r.StartDate < DateTime.Now && r.EndDate > DateTime.Now)
                    {
                        surfboard.IsRented = true;
                    }
                    else if (r.EndDate < DateTime.Now)
                    {
                        surfboard.IsRented = false;
                    }

                }
                await _context.SaveChangesAsync();

            }
        }

        private bool SurfboardExists(int id)
        {
            return _context.Surfboard.Any(e => e.ID == id);
        }


    }
}
