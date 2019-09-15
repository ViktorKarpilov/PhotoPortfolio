using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PhotoPortfolio.Models
{
    public class PhotoPostModel
    {

        [FromForm]
        public string CollectionName { get; set; }

        [FromForm]
        public IFormFile Image { get; set; }

        [FromForm]
        public string ImageName { get; set; }

    }
}
