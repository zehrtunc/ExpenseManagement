using ExpenseManagement.Base;
using ExpenseManagement.Schema;
using ExpenseManagement.UI.Models.ViewModels;
using ExpenseManagement.UI.Services.ExpenseManagement.UI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManagement.UI.Controllers;

public class ExpenseController : Controller
{
    private readonly ApiRequestService _api;

    public ExpenseController(ApiRequestService api)
    {
        _api = api;
    }

    public IActionResult Manage()
    {
        return View();
    }

    public IActionResult MyExpenses()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddExpenseViewModel model)
    {
        // islicez

        if (!ModelState.IsValid)
            return View(model);


        var request = new ExpenseRequest
        {
            Amount = Convert.ToDecimal(model.Amount),
            Location = model.Location,
            CategoryId = model.CategoryId,
            Status = Schema.Enums.ExpenseStatus.Pending,
            RequestDate = DateTime.Now,
            UserId = 0 // to do: user servisi yaz
        };

        var result = await _api.PostAsync<ExpenseRequest, ApiResponse<ExpenseResponse>>("Expense", request);

        if (!result.Success)
        {
            ModelState.AddModelError("", result.Message);
            return View(model);
        }

        return RedirectToAction("MyExpenses");
    }
}

