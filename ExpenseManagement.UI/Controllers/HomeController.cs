﻿using Microsoft.AspNetCore.Mvc;

namespace ExpenseManagement.UI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
