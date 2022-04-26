namespace Measurement.Model
{
    public class Measurement
    {
        public Guid Id { get; set; }
        public float Value { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Type { get; set; }
        public string UserId { get; set; }
    }
}
