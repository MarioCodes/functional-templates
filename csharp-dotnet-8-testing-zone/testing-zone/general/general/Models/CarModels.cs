namespace general.Models
{
    public class CarResponse
    {
        public CarDto Car { get; set; }
    }

    public class CarDto
    {
        public int Id { get; set; }
        public string Car { get; set; }
        public string Car_Model { get; set; }
        public string Car_Color { get; set; }
        public int Car_Model_Year { get; set; }
        public string Car_Vin { get; set; }
        public string Price { get; set; }
        public bool Availability { get; set; }
    }
}
