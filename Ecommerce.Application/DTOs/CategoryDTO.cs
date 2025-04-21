namespace Ecommerce.Application.DTOs
{
    public record CategoryDTO(string name, string description);
    public record UpdateCategoryDTO(string name, string description, int id);
}
