using WebAPICoreTask2.Models;

namespace WebAPICoreTask2.DTO_folder
{
    public class categoryRequestDTO
    {
        public string? CategoryName { get; set; }

        public IFormFile? CategoryImage { get; set; }
    }
}
