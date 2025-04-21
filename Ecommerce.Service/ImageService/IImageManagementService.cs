using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Service.ImageService
{
    public interface IImageManagementService
    {
        Task<List<string>> AddImageAsync(IFormFileCollection formFiles, string src);
        void DeleteImageAsync(string src);
    }
}
