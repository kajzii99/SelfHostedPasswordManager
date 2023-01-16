using System.ComponentModel.DataAnnotations;

namespace SelfHostedPasswordManager.ViewModels
{
    public class LoginViewModel
    {
        [Required] 
        [EmailAddress]
        [Display(Name = "E-mail address")]
        public string Email { get; set; }   

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }    
    }
}
