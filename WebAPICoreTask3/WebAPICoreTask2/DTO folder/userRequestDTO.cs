using System.ComponentModel.DataAnnotations;
using WebAPICoreTask2.Models;

namespace WebAPICoreTask2.DTO_folder
{
    public class userRequestDTO
    {
        // if i forget to handle the error in JavaSxript
        public string? Username { get; set; }

        [Required]
        public string? Password { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "please enter a correct Email")]
        public string? Email { get; set; }
    }
}
