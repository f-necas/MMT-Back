using System.ComponentModel.DataAnnotations;

namespace MMT_Back.EntityModels
{
    public class Invitation
    {
        [Required]
        public int Id {  get; set; }

        [Required]
        public int UserEventId { get; set; }
        public UserEvent UserEvent {  get; set; }
        [Required]
        public string StatusCode {  get; set; }
        [Required]
        public int InvitedUserId { get; set; }
       
        public User InvitedUser {  get; set; }
    }
}
