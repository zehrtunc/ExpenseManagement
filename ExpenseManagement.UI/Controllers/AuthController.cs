﻿using ExpenseManagement.Base;
using ExpenseManagement.Schema;
using ExpenseManagement.UI.Models.ViewModels.Auth;
using ExpenseManagement.UI.Services;
using ExpenseManagement.UI.Services.ExpenseManagement.UI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManagement.UI.Controllers
{
    public class AuthController : BaseController
    {
        private readonly AuthCookieService _authCookieService;
        public AuthController(ApiRequestService api, AuthCookieService authCookieService) : base(api)
        {
            _authCookieService = authCookieService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            var request = new RegisterRequest()
            {
                Name = model.Name,
                Surname = model.Surname,
                Email = model.Email,
                Password = model.Password
            };

            var result = await _api.PostAsync<RegisterRequest, ApiResponse>("Auth/register", request);

            if (result.Success)
            {
                RedirectToAction("Login", "Auth");
            }
            else
            {
                ModelState.AddModelError("", result.Message);
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var request = new LoginRequest()
            {
                Email = model.Email,
                Password = model.Password,
                RememberMe = model.RememberMe
            };

            var result = await _api.PostAsync<LoginRequest, ApiResponse<LoginResponse>>("Auth/login", request);

            if (result.Success)
            {
                _authCookieService.SetToken(result.Response.Token, false);
                return RedirectToAction("MyExpenses", "Expense");
            }
            else
            {
                ModelState.AddModelError("", result.Message);
            }

            return View(model);
        }
    }
}
