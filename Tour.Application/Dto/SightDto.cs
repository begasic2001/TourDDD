

namespace Tour.Application.Dto
{
    public class SightDto : Entity
    {

        public string SightName { get; set; }

        public double SightForMoney { get; set; }
        public string Picture { get; set; }
        public string? CityId { get; set; }
    }
}
