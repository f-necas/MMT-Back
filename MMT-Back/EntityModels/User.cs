using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;

namespace MMT_Back.EntityModels
{
    public class User 
    {

        public int Id {  get; set; }

        [Required]
        public string UserName {  get; set;}

        [Required]
        public string Password { get; set; }

        public User()
        {
            SentFriendRequests = new List<Friend>();
            ReceievedFriendRequests = new List<Friend>();
        }

        //public byte[] ProfilePicture { get; set; }

        public virtual ICollection<Friend> SentFriendRequests { get; set; }

        public virtual ICollection<Friend> ReceievedFriendRequests { get; set; }

        [NotMapped]
        public virtual ICollection<Friend> Friends
        {
            get
            {
                var friends = SentFriendRequests.Where(x => x.Approved).ToList();
                friends.AddRange(ReceievedFriendRequests.Where(x => x.Approved));
                return friends;
            }
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedDateTime;

    }
}
