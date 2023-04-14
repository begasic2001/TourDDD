namespace Tour.Api.Models
{
    public class AddToCartModel
    {
        public string UserId { get; set; }
        public string TourId { get; set;}
        public int Amount { get; set;}
    }
}
