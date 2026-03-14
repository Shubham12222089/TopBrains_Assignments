using Microsoft.AspNetCore.Mvc;

namespace CodeFirstDemo.Controllers
{
    public class ActionResultControllerDemo : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Google()
        {
            return Redirect("https://www.google.com");
        }
        public IActionResult Cricbuzz()
        {
            return Redirect("https://www.cricbuzz.com");
        }
        public IActionResult Login()
        {
            return RedirectToAction("Dashboard");
        }
        public IActionResult Dashboard()
        {
            return Content("Welcome to the Dashboard!");
        }
        public IActionResult DrivingLicence(int age)
        {
            if (age < 18)
            {
                return BadRequest("Age Must be Greater than 18");
            }
            return Ok("Valid Age for Driving Licence");
        }
        public IActionResult Success()
        {
            return Ok("Operation Successful");
        }
        public IActionResult StudentJson()
        {
            var student = new
            {
                Id = 1,
                Name = "Abc",
                Course = ".Net"
            };
            return Json(student);
        }
    }
    
}
