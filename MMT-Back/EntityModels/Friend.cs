using MMT_Back.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MMT_Back.EntityModels
{
    public class Friend
    {
        public int Id {  get; set; }
        public int RequestedById { get; set; }
      
        public int RequestedToId { get; set; }
        public virtual User RequestedBy { get; set; }
        public virtual User RequestedTo { get; set; }

        public DateTime? RequestTime { get; set; }

        public DateTime? BecameFriendsTime { get; set; }

        public FriendRequestFlag FriendRequestFlag { get; set; }

        [NotMapped]
        public bool Approved => FriendRequestFlag == FriendRequestFlag.Approved;

        public static void AddFriendRequest(User user, int friendUser)
        {
            var friendRequest = new Friend()
            {
                RequestedBy = user,
                RequestedToId = friendUser,
                RequestTime = DateTime.Now,
                FriendRequestFlag = FriendRequestFlag.None
            };
            user.SentFriendRequests.Add(friendRequest);
        }
        public void AcceptFriendRequest()
        {
            this.BecameFriendsTime = DateTime.Now;
            this.FriendRequestFlag = FriendRequestFlag.Approved;
        }
    }

}
