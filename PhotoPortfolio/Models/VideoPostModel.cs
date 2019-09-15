using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PhotoPortfolio.Models
{
    public class VideoPostModel
    {
        [FromForm]
        public string CollectionName { get; set; }

        [FromForm]
        public IFormFile Video { get; set; }

        [FromForm]
        public string VideoName { get; set; }
    }
}
