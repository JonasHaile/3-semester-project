using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Surf.Areas.Identity.Data;
using Surf.Data;
using Surf.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Surf.Controllers
{
    public class UserController : Controller
    {
        private readonly SurfDbContext _context;
        private readonly UserManager<ApplicationUser> _usermananger;
        private readonly IHttpClientFactory _iHttpClientFactory;

        public UserController(SurfDbContext context, UserManager <ApplicationUser> usermanager, IHttpClientFactory iHttpClientFactory)
        {
            _context = context;
            _usermananger = usermanager;
            _iHttpClientFactory = iHttpClientFactory;
        }

        // GET: User
        public async Task<IActionResult> Index(
    string sortOrder,
    string currentFilter,
    string searchString,
    int? pageNumber)
        {
            IEnumerable<Surfboard> surfboards = Enumerable.Empty<Surfboard>();
            HttpClient client = _iHttpClientFactory.CreateClient("API");
            HttpResponseMessage response = client.GetAsync("API").Result;
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                surfboards = JsonConvert.DeserializeObject<IEnumerable<Surfboard>>(data);
            }



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

            //var user = await _usermananger.GetUserAsync(User);
            //var unavailableDate = DateTime.Today.AddDays(5);
            //var surfboards = from s in _context.Surfboard
            //                 where !_context.Rental.Any(r => r.SurfboardId == s.ID) ||
            //                       (_context.Rental.Any(r => r.SurfboardId == s.ID && (DateTime.Today > r.EndDate || unavailableDate < r.StartDate)) ||
            //                        (user != null && _context.Rental.Any(r => r.SurfboardId == s.ID && r.UserId == user.Id)))
            //                 select s;

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
            return View(await PaginatedList<Surfboard>.CreateAsync(surfboards, pageNumber ?? 1, pageSize));


        }


        // GET: User/Edit/5
        [Authorize]
        public async Task<IActionResult> Create(int? id) // Rental
        {
            HttpClient client = _iHttpClientFactory.CreateClient("API");
            HttpResponseMessage response = client.GetAsync($"API/{id}").Result;
            Surfboard surfboard = new();
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                surfboard = JsonConvert.DeserializeObject<Surfboard>(data);
            }
            if (id == null || surfboard.ID == 0)
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
            HttpClient client = _iHttpClientFactory.CreateClient("API");

            // Opret en dictionary med data, der skal sendes
            var requestData = new Dictionary<string, string>
    {
        { "startDate", startDate.ToString("yyyy-MM-dd") }, // Konverter til ønsket datoformat
        { "id", surfboard.ID.ToString() }
    };

            var content = new FormUrlEncodedContent(requestData);

            HttpResponseMessage response = await client.PostAsync("API/Post", content); // Send data i anmodningens krop

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                surfboard = JsonConvert.DeserializeObject<Surfboard>(data);
                TempData["SuccessMessage"] = "Surfboard is now rented";
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Unable to rent surfboard");
            }
            return View("Index");
        }

        private async void RentalCheck()
        {
            var rental =  _context.Rental.Where(r => r.StartDate < DateTime.Now).ToList();
            if (rental != null || rental.Any())
            {
                foreach (var r in rental)
                {
                    var surfboard = await _context.Surfboard.FirstOrDefaultAsync(s => s.ID == r.SurfboardId);
                    if(r.StartDate < DateTime.Now && r.EndDate > DateTime.Now)
                    {
                        surfboard.IsRented = true;  
                    }
                   else if(r.EndDate < DateTime.Now)
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
