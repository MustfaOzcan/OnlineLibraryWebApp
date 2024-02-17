using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Host;
using Newtonsoft.Json;
using System.Data;
using System.Diagnostics;
using System.Security.Policy;
using WebUygulamaProje1.Models;
using WebUygulamaProje1.Utility;

namespace WebUygulamaProje1.Controllers
{
    public class HomeController : Controller
    {
        private ILogger<HomeController> _logger;
        private UygulamaDbContext _context;
        string baseUrl = "https://dummyjson.com/";

        public HomeController(ILogger<HomeController> logger,UygulamaDbContext context)
        {
            _logger = logger;
            
            _context = context;
        }
        ///users
        public async Task<IActionResult> API()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://dummyjson.com/todos");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();
            var todoList = JsonConvert.DeserializeObject<TodoList>(jsonString);

            return View(todoList.todos);
        }

        public IActionResult HomePage()//ana sayfa
        {


            
            return View();
        }




        public IActionResult Index()//ülke listeleniyor 
        {
           

            var countries = _context.Countries.ToList();
            return View(countries);
        }


        public IActionResult Create()//ülke ekle
        {
            return View();

        }
        [HttpPost]
		public IActionResult Create(Country country)
		{
            _context.Countries.Add(country);
            _context.SaveChanges();
			return RedirectToAction("Index");

		}

        public IActionResult StateIndex()//eyalet listeleniyor
        {
            var states=_context.States.ToList();
            return View(states);
        }

		
		public IActionResult CreateState()//eyalet ekle
		{
            ViewBag.Countries = new SelectList(_context.Countries, "Id", "Name");
			return View();
		}


        [HttpPost]
		public IActionResult CreateState(State state)
		{
            _context.States.Add(state);
            _context.SaveChanges();
            return RedirectToAction("StateIndex");
		}

        public IActionResult CityIndex()//sehirlistesi
        {
            var cities = _context.Cities.ToList();
            return View(cities);
        }
        public IActionResult CreateCity()
        {
            ViewBag.State = new SelectList(_context.States, "Id", "Name");
            return View();
        }
        [HttpPost]
        public IActionResult CreateCity(City city)//sehir ekle
        {
            _context.Cities.Add (city);
            _context.SaveChanges();
			return RedirectToAction("CityIndex");

		}

        public IActionResult CascadeList()
        {
            ViewBag.Countries = new SelectList(_context.Countries, "Id", "Name");
            return View();
        }

        public JsonResult LoadState(int Id)
        {
            var state = _context.States.Where(z => z.CountryId == Id).ToList();
            return Json(new SelectList(state, "Id", "Name"));

        }

		public JsonResult LoadCity(int Id)
		{
			var cities = _context.Cities.Where(z => z.StateId == Id).ToList();
			return Json(new SelectList(cities, "Id", "Name"));

		}





		public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}