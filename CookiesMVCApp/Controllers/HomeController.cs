using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CookiesMVCApp.Models;
using Microsoft.AspNetCore.Http;

namespace CookiesMVCApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //THis will be the View for the Survey....
        //It fetches an Email COokie, if it's present...
        public IActionResult Index()
        {
            string Email = Request.Cookies["Email"];
            return View("Index", Email);
        }

        public IActionResult Survey()
        {
            string Email = Request.Cookies["Email"];
            return View("Survey", Email);
        }


        [HttpPost]
        public IActionResult Index(IFormCollection form)
        {
            string Email = form["Email"].ToString();

            //Set Cookie Value...and options
            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddMinutes(10);
            Response.Cookies.Append("Email", Email, option);

            return RedirectToAction(nameof(Survey));
        }

        public IActionResult RemoveCookie()
        {
            //Delete the Cookie
            Response.Cookies.Delete("Email");
            return View("Index");
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
