using Microsoft.AspNetCore.Mvc.Rendering;

namespace ExpenseManagement.UI.Models.ViewModels
{
    public class AddExpenseViewModel
    {
        public string Amount { get; set; }
        public string Location { get; set; }
        public long CategoryId { get; set; }

        public List<SelectListItem> ExpenseCategories { get; set; } = new();
        public long SelectedCategory { get; set; }
        public List<IFormFile> Documents { get; set; }
    }
}
