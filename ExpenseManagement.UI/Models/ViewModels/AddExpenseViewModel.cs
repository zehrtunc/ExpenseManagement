namespace ExpenseManagement.UI.Models.ViewModels
{
    public class AddExpenseViewModel
    {
        public string Amount { get; set; }
        public string Location { get; set; }
        public long CategoryId { get; set; }

        public List<IFormFile> Documents { get; set; }
    }
}
