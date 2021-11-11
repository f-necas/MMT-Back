using MMT_Back.Enum;

namespace MMT_Back.DTO
{
    public class FriendshipInteraction
    {
        public int userId { get; set; }

        public FriendRequestFlag action { get; set; }
    }
}
