namespace TheatreApi.DataAccess.Models
{
    public class Play
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset Date { get; set; }
        public List<Actor> Actors { get; set; }
    }
}
