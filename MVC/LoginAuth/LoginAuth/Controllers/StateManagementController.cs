using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace LoginAuth.Controllers
{
    public class StateManagementController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SetCookie()
        {
            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddMinutes(10);

            Response.Cookies.Append("UserName", "Abc", option);
            return Content("Cookie Created.");
        }
        public IActionResult GetCookies()
        {
            string username = Request.Cookies["UserName"];
            return Content("Cookie Value: " + username);
        }
        public IActionResult DeleteCookie()
        {
            Response.Cookies.Delete("UserName");
            return Content("Cookie Deleted");
        }

        //hidden Field
        public IActionResult SaveData()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SaveData(int userId)
        {
            return Content("UserId is : " + userId);
        }

        public IActionResult Index2()
        {
            return RedirectToAction("Details", new { id= 10});
        }
        public IActionResult Details(int id) {
            return Content("Product Id: "+id);
        }

        public IActionResult SetSession()
        {
            HttpContext.Session.SetString("UserName", "Abc");
            return Content("Session Created");
        }
        public IActionResult GetSession()
        {
            string name = HttpContext.Session.GetString("UserName");
            return Content("User Name : " + name);
        }

        private readonly IMemoryCache _cache;
        public StateManagementController(IMemoryCache _cache)
        {
            this._cache = _cache;
        }
        public IActionResult CacheDemo()
        {
            _cache.Set("User", "Abc");
            string user = _cache.Get<string>("User");
            return Content(user);
        }
    }
}
