using ExpenseManagement.Base;
using ExpenseManagement.Schema;
using ExpenseManagement.UI.Filters;
using ExpenseManagement.UI.Models.ViewModels;
using ExpenseManagement.UI.Services.ExpenseManagement.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ExpenseManagement.UI.Controllers;

public class ExpenseController : BaseController
{
    public ExpenseController(ApiRequestService api) : base(api)
    {
    }

    [RoleAuthorize("admin")]
    public IActionResult Manage()
    {

        return View();
    }

    public IActionResult MyExpenses()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        var model = new AddExpenseViewModel();
        var result = await _api.GetAsync<ApiResponse<List<ExpenseCategoryResponse>>>("ExpenseCategory/GetAll");

        if (result.Success)
        {
            foreach(ExpenseCategoryResponse category in result.Response)
            {
                model.ExpenseCategories.Add(new SelectListItem(category.Name, category.Id.ToString()));
            }
        }

        return View(model);
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
            UserId = 0, // to do: user servisi yaz,
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

