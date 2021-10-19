using MMT_Back.EntityModels;

namespace MMT_Back.Models
{
    public class NewEventRequestDTO
    {
        public UserEvent eventItem { get; set; }
        public IEnumerable<int> users {  get; set; }
    }
}
