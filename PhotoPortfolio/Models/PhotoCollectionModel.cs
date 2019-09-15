using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;

namespace PhotoPortfolio.Models
{
    public class PhotoCollectionModel
    {
        //First photo in dictionary will be face of collection 

        public string PhotoUrls { get; private set; } = "";
        public string PhotoNames { get; private set; } = "";
        public string Name { get; set; }
        public int Lenth { get; private set; } = 0;


        [Key]
        public int Id { get; set; }

        public PhotoCollectionModel(string name)
        {
            Name = name;
        }
        public PhotoCollectionModel(string name,string url,string faceName)
        {
            Name = name;
            PhotoUrls = url;
            PhotoNames = faceName;
        }

        public void AddPhoto(string url, string name)
        {
            if (Lenth == 0)
            {
                PhotoUrls =url;
                PhotoNames =name;
                Lenth++;
                return;
            }
            PhotoUrls += ","+url;
            PhotoNames += ","+name;
            Lenth++;
        }

        public string GetPhotoUrl(int number)
        {
            return PhotoUrls.Split(",").ToArray()[number];
        }
        public string GetPhotoName(int number)
        {
            return PhotoNames.Split(",").ToArray()[number];
        }

        // 
        public void DeletePhoto(int number)
        {
            if(number >= Lenth)return;

            this.PhotoUrls  = DataOperations.DeleteElementFromCsvString(this.PhotoUrls , number);
            this.PhotoNames = DataOperations.DeleteElementFromCsvString(this.PhotoNames, number);
            this.Lenth--;
        } 

        



    }
    
    public class PhotoCollectionContext : DbContext
    {
        public DbSet<PhotoCollectionModel> PhotoCollections { get; set; }

        public PhotoCollectionContext(DbContextOptions<PhotoCollectionContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        
    }
}
