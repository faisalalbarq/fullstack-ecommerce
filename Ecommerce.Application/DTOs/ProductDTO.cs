namespace Ecommerce.Application.DTOs
{
    public class ProductDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string CategoryName { get; set; }

        public virtual List<PictureDTO> Pictures { get; set; } = new List<PictureDTO>();
    }
}
