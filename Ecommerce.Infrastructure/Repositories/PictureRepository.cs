using Ecommerce.Core.Entities.Product;
using Ecommerce.Core.Interfaces.Repositories.Contract;
using Ecommerce.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.Repositories
{
    public class PictureRepository : GenericRepository<Picture>, IPictureRepository
    {
        public PictureRepository(StoreContext storeContext) : base(storeContext)
        {

        }
    }
}
