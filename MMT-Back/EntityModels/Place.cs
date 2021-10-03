using NetTopologySuite.Geometries;
using System.ComponentModel.DataAnnotations;

namespace MMT_Back.EntityModels
{
    public class Place
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        //[Required]
        //public string Code { get; set; }
        [Required]
        public string Address { get; set; }

        // public Geometry Coordinate { get; set; }

    }
}
