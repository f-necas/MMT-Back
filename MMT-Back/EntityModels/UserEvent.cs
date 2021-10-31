using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace MMT_Back.EntityModels
{
    public class UserEvent
    {
        [Required]
        public int Id {  get; set; }
        [Required]
        public string EventName {  get; set; }
        [Required]
        public int RequesterUserId {  get; set; }
        [JsonIgnore]
        public User RequesterUser {  get; set; }
        [Required]
        public DateTime EventDate {  get; set; }
        [Required]
        public int EventPlaceId { get; set; }
        public Place EventPlace {  get; set; }
    }
}
