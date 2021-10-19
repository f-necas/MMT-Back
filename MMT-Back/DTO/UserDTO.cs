using System.ComponentModel.DataAnnotations;

namespace MMT_Back.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }
    }
}
