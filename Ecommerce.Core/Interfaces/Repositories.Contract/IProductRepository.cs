﻿using Ecommerce.Core.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.Interfaces.Repositories.Contract
{
    public interface IProductRepository: IGenericRepository<Product>
    {
    }
}
