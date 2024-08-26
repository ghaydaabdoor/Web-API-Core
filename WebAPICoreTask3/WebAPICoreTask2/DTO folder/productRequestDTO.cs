using WebAPICoreTask2.Models;

namespace WebAPICoreTask2.DTO_folder
{
    public class productRequestDTO
    {
        public string? ProductName { get; set; }

        public string? Description { get; set; }

        public int? Price { get; set; }

        public int? CategoryId { get; set; }

        public IFormFile? ProductImage { get; set; }

    }
}
