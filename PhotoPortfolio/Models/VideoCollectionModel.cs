using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PhotoPortfolio.Models;
using System.ComponentModel.DataAnnotations;

namespace PhotoPortfolio.Models
{
    public class VideoCollectionModel
    {
        //First Video in dictionary will be face of collection 

        
        public string VideoNames { get; private set; } = "";
        public string Name { get; set; }
        public int Lenth { get; private set; } = 0;

        [Key]
        public int Id { get; set; }
        public VideoCollectionModel(string name)
        {
            Name = name;
        }
        public VideoCollectionModel(string name, string faceName)
        {
            Name = name;
            VideoNames = faceName;
        }
        public void AddVideo(string name)
        {
            if (Lenth == 0)
            {
                VideoNames = name;
                Lenth++;
                return;
            }
            VideoNames += "," + name;
            Lenth++;
        }
        public string GetVideoName(int number)
        {
            return VideoNames.Split(",").ToArray()[number];
        }
        // 
        public void DeleteVideo(int number)
        {
            if (number >= Lenth) return;

            this.VideoNames = DataOperations.DeleteElementFromCsvString(this.VideoNames, number);
            this.Lenth--;
        }
    }

    public class VideoCollectionContext : DbContext
    {
        public DbSet<VideoCollectionModel> VideoCollections { get; set; }

        public VideoCollectionContext(DbContextOptions<VideoCollectionContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

    }
}
