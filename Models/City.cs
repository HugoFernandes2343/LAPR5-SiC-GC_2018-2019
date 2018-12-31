using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiC.Models
{
    public class City
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CityId { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public City(string Name, double Latitude, double Longitude)
        {
            this.Name = Name;
            this.Latitude = Latitude;
            this.Longitude = Longitude;
        }

        public City()
        {

        }
    }
}