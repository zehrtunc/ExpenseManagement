using System.ComponentModel.DataAnnotations;

namespace ExpenseManagement.UI.Models.ViewModels.Auth
{
    public class LoginViewModel
    {
        [EmailAddress(ErrorMessage = "Email adresi giriniz")]
        public string Email { get; set; }
        [Length(6, 20)]
        public string Password { get; set; }
        public bool? RememberMe { get; set; }
    }
}
