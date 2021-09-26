using System.ComponentModel.DataAnnotations;

namespace MMT_Back.EntityModels
{
    public class UserProfile
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }

        public string Coordinate { get; set; }

    }
}
