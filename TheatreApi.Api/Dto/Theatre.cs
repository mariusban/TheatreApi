namespace TheatreApi.Api.Dto
{
    public class Theatre
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Play> Plays { get; set; }
    }
}
