using EFRelationships.Data;
using EFRelationships.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EFRelationships.Web.Controllers
{
    public class HomeController : Controller
    {
        private string _connectionString;
        public HomeController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }

        public IActionResult Index()
        {
            var repo = new PeopleCarsRepository(_connectionString);
            return View(repo.GetPeople());
        }

        public IActionResult AddCar(int personId)
        {
            var repo = new PeopleCarsRepository(_connectionString);
            var person = repo.GetPerson(personId);
            return View(person);
        }

        [HttpPost]
        public IActionResult AddCar(Car car)
        {
            var repo = new PeopleCarsRepository(_connectionString);
            repo.AddCar(car);
            return RedirectToAction("Index");
        }

        public IActionResult Losers()
        {
            var repo = new PeopleCarsRepository(_connectionString);
            return View(repo.GetPeopleWithNoCars());
        }
    }
}