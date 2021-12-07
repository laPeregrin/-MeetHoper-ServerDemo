namespace Common.Models.DTOs
{
    public class Geoposition
    {
        public float Langitude { get; set; }
        public float Latitude { get; set; }

        public Geoposition(float langitude, float latitude)
        {
            Langitude = langitude;
            Latitude = latitude;
        }
    }
}
