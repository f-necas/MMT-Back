using System.ComponentModel.DataAnnotations;
using NetTopologySuite.Geometries;

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

        public Point? Coordinate { get; set; }

    }
}
