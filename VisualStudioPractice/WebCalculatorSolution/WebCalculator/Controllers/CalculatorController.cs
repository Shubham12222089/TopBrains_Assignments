using Microsoft.AspNetCore.Mvc;
using WebCalculator.Models;

namespace WebCalculator.Controllers
{
    public class CalculatorController : Controller
    {
        Calculator calc = new Calculator();

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(int a, int b, string op)
        {
            ViewBag.Result = op switch
            {
                "+" => calc.Add(a, b).ToString(),
                "-" => calc.Sub(a, b).ToString(),
                "*" => calc.Mul(a, b).ToString(),
                "/" => calc.Div(a, b).ToString(),
                _ => "Invalid"
            };

            return View();
        }
    }
}
