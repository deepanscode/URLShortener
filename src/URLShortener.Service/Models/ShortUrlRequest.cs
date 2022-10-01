using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URLShortener.Service.Models
{
    public record ShortUrlRequest
    {
        public string Url { get; init; }
    }
}
