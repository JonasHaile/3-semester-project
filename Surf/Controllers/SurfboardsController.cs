using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Surf.Data;
using Surf.Models;

namespace Surf.Controllers
{
    public class SurfboardsController : Controller
    {
        private readonly SurfContext _context;

        public SurfboardsController(SurfContext context)
        {
            _context = context;
        }

        // GET: Surfboards
<<<<<<< HEAD
        public async Task<IActionResult> Index(
    string sortOrder,
    string currentFilter,
    string searchString,
    int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var surfboards = from s in _context.Surfboard
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                surfboards = surfboards.Where(s => s.Name.Contains(searchString)
                                       || s.Type.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    surfboards = surfboards.OrderByDescending(s => s.Name);
                    break;
                case "Date":
                    surfboards = surfboards.OrderBy(s => s.Price);
                    break;
                case "date_desc":
                    surfboards = surfboards.OrderByDescending(s => s.Type);
                    break;
                default:
                    surfboards = surfboards.OrderBy(s => s.Equipment);
                    break;
            }

            int pageSize = 5;
            return View(await PaginatedList<Surfboard>.CreateAsync(surfboards.AsNoTracking(), pageNumber ?? 1, pageSize));
=======
        public async Task<IActionResult> Index(string searchString, string sortOrder, string surfboardEquipment, decimal? price)
        {

            // Retrieve all surfboards from _context local database
            // And storing in the 'surfboards' variable
            var surfboards = from s in _context.Surfboard select s;

            // Sort
            ViewData["NameSortParm"] = sortOrder == "Name" ? "Name_desc" : "Name";
            ViewData["LengthSortParm"] = sortOrder == "Length" ? "Length_desc" : "Length";
            ViewData["WidthSortParm"] = sortOrder == "Width" ? "Width_desc" : "Width";
            ViewData["ThicknessSortParm"] = sortOrder == "Thickness" ? "Thickness_desc" : "Thickness";
            ViewData["VolumeSortParm"] = sortOrder == "Volume" ? "Volume_desc" : "Volume";
            ViewData["TypeSortParm"] = sortOrder == "Type" ? "Type_desc" : "Type";
            ViewData["PriceSortParm"] = sortOrder == "Price" ? "Price_desc" : "Price";
            ViewData["EquipmentSortParm"] = sortOrder == "Equipment" ? "Equipment_desc" : "Equipment";

            // Sort
            switch (sortOrder)
            {
                case "Name":
                    surfboards = surfboards.OrderBy(s => s.Name);
                    break;
                case "Name_desc":
                    surfboards = surfboards.OrderByDescending(s => s.Name);
                    break;
                case "Length":
                    surfboards = surfboards.OrderBy(s => s.Length);
                    break;
                case "Length_desc":
                    surfboards = surfboards.OrderByDescending(s => s.Length);
                    break;
                case "Width":
                    surfboards = surfboards.OrderBy(s => s.Width);
                    break;
                case "Width_desc":
                    surfboards = surfboards.OrderByDescending(s => s.Width);
                    break;
                case "Thickness":
                    surfboards = surfboards.OrderBy(s => s.Thickness);
                    break;
                case "Thickness_desc":
                    surfboards = surfboards.OrderByDescending(s => s.Thickness);
                    break;
                case "Volume":
                    surfboards = surfboards.OrderBy(s => s.Volume);
                    break;
                case "Volume_desc":
                    surfboards = surfboards.OrderByDescending(s => s.Volume);
                    break;
                case "Type":
                    surfboards = surfboards.OrderBy(s => s.Type);
                    break;
                case "Type_desc":
                    surfboards = surfboards.OrderByDescending(s => s.Type);
                    break;
                case "Price":
                    surfboards = surfboards.OrderBy(s => s.Price);
                    break;
                case "Price_desc":
                    surfboards = surfboards.OrderByDescending(s => s.Price);
                    break;
                case "Equipment":
                    surfboards = surfboards.OrderBy(s => s.Equipment);
                    break;
                case "Equipment_desc":
                    surfboards = surfboards.OrderByDescending(s => s.Equipment);
                    break;
            }

            // Search
            if (!string.IsNullOrEmpty(searchString))
            {
                surfboards = surfboards.Where(s => s.Name!.Contains(searchString));
            }

            return View(await surfboards.ToListAsync());
>>>>>>> SearchBranch
        }

        // GET: Surfboards/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Surfboard == null)
            {
                return NotFound();
            }

            var surfboard = await _context.Surfboard
                .FirstOrDefaultAsync(m => m.ID == id);
            if (surfboard == null)
            {
                return NotFound();
            }

            return View(surfboard);
        }

        // GET: Surfboards/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Surfboards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Length,Width,Thickness,Volume,Price,Type,Equipment,Image")] Surfboard surfboard)
        {
            if (ModelState.IsValid)
            {
                _context.Add(surfboard);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(surfboard);
        }

        // GET: Surfboards/Edit/5
        public async Task<IActionResult> Edit(int? id)
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

        // POST: Surfboards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Length,Width,Thickness,Volume,Price,Type,Equipment,Image")] Surfboard surfboard)
        {
            if (id != surfboard.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(surfboard);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SurfboardExists(surfboard.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(surfboard);
        }

        // GET: Surfboards/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Surfboard == null)
            {
                return NotFound();
            }

            var surfboard = await _context.Surfboard
                .FirstOrDefaultAsync(m => m.ID == id);
            if (surfboard == null)
            {
                return NotFound();
            }

            return View(surfboard);
        }

        // POST: Surfboards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Surfboard == null)
            {
                return Problem("Entity set 'SurfContext.Surfboard'  is null.");
            }
            var surfboard = await _context.Surfboard.FindAsync(id);
            if (surfboard != null)
            {
                _context.Surfboard.Remove(surfboard);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SurfboardExists(int id)
        {
            return (_context.Surfboard?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
