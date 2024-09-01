using System.ComponentModel.DataAnnotations;
using WebAPICoreTask2.Models;

namespace WebAPICoreTask2.DTO_folder
{
    public class userRequestDTO
    {
        [Required(ErrorMessage = "Plz enter a UserName")] // if i forget to handle the error in JavaSxript
        public string? Username { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Password should be between 8 and 20 characters")]
        public string? Password { get; set; }

        [EmailAddress(ErrorMessage = "Plz enter a correct Email")]
        public string? Email { get; set; }
    }
}
