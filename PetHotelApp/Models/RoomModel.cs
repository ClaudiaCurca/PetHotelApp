namespace PetHotelApp.Models
{
    public class RoomModel
    {
        public Guid IdRoom { get; set; }
        public int? Capacity { get; set; }
        public decimal? PricePerDay { get; set; }
        public byte[]? RoomType { get; set; }
    }
}
