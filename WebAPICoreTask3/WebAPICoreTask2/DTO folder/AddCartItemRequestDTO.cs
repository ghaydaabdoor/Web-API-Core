using WebAPICoreTask2.Models;

namespace WebAPICoreTask2.DTO_folder
{
    public class AddCartItemRequestDTO
    {

        public int? CartId { get; set; }

        public int? ProductId { get; set; }

        public int Quantity { get; set; }

    }
}
