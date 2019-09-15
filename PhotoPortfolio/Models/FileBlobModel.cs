using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace PhotoPortfolio.Models
{
    public class FileBlobModel
    {
        public IFormFile FilePackage { get; set; }
        public string FileName { get; set; }
        public int Number { get; set; }


    }
}
