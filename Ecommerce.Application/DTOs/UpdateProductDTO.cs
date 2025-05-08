using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.DTOs
{
    public record UpdateProductDTO : AddProductDTO
    {
        public int Id { get; set; }
    }
}
