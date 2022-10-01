using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLShortener.Service.Models;

namespace URLShortener.Service.Contracts
{
    public interface IUrlMappinRepository
    {
        Task<UrlMapping> Save(string url);
        Task<UrlMapping> Get(string id);
    }
}
