using System.ComponentModel.DataAnnotations;

namespace SelfHostedPasswordManager.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "E-mail address")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Passwords aren't the same")]
        public string ConfirmPassword { get; set; }
        public bool RememberMe { get; set; }    
    }
}
