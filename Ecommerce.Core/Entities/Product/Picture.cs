using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Core.Entities.Product
{
    public class Picture : BaseEntity<int>
    {
        public string ImageName { get; set; }


        public int ProductId { get; set; }
        //[ForeignKey(nameof(ProductId))]
        //public virtual Product Product { get; set; }
    }
}
