using Microsoft.EntityFrameworkCore;
using TheatreApi.DataAccess.Context;
using TheatreApi.DataAccess.Models;

namespace TheatreApi.DataAccess.Repositories
{
    public class TheatreRepository : ITheatreRepository
    {
        private readonly TheatreDBContext _context;

        public TheatreRepository(TheatreDBContext context)
        {
            _context = context;
        }

        public IEnumerable<Theatre> GetTheatres()
        {
            return _context.Theatre.Include(t => t.Plays).ThenInclude(p => p.Actors);
        }

        public IEnumerable<Play> GetPlays()
        {
            return _context.Play.Include(p => p.Actors);
        }

        public Theatre GetTheatre(int id)
        {
            return GetTheatres().FirstOrDefault(theatre => theatre.Id == id);
        }

        public Theatre CreateTheatre(Theatre Theatre)
        {
            _context.Theatre.Include(t => t.Plays).ThenInclude(p => p.Actors).ToList().Add(Theatre);
            _context.SaveChanges();

            return Theatre;
        }

        public Theatre UpdatePlay(IEnumerable<Theatre> listTheatres, int idTheatre, IEnumerable<Play> listPlays, int idPlay)
        {
            var theatreFound = new Theatre();

            if (listTheatres.Any(theatre => theatre.Id == idTheatre) && listPlays.Any(play => play.Id == idPlay))
            {
                listTheatres.First(theatre => theatre.Id == idTheatre).Plays.Add(listPlays.First(play => play.Id == idPlay));
                theatreFound = listTheatres.FirstOrDefault(theatre => theatre.Id == idTheatre);
            }

            return theatreFound;
        }

        public List<Play> GetPlays(int id)
        {
            var listPlays = new List<Play>();
            if (GetTheatres().Any(theatre => theatre.Id == id))
            {
                listPlays = GetTheatres().First(theatre => theatre.Id == id).Plays;
            }

            return listPlays;
        }

        public IEnumerable<Play> GetPlay(DateTimeOffset? startDate, DateTimeOffset? endDate, string name)// cant modify, too many conditions in the same foreach
        {
            List<Play> theatrePlays = new List<Play>();

            bool existDate = false;

            if (startDate != null && endDate != null)
                existDate = true;

            foreach (Theatre theatre in GetTheatres())
            {
                var filteredPlays = theatre.Plays
                    .Where(play =>
                        play.Actors.Where(actor =>
                            actor.FirstName.ToLower().Contains(name.ToLower())
                            || actor.LastName.ToLower().Contains(name.ToLower())).Any());

                if (existDate)
                {
                    filteredPlays.Where(play => play.Date >= startDate && play.Date <= endDate);
                }

                theatrePlays.AddRange(filteredPlays);
            }

            return theatrePlays;
        }

        public Decimal GetActorsAverage()
        {
            var playsCount = GetPlays().Count();
            var actorsCount = 0;

            foreach (Play play in GetPlays())
            {
                actorsCount += play.Actors.Count();
            }

            return ((decimal)actorsCount/(decimal)playsCount);
        }

        public Play CreatePlay(Play play)
        {
            GetPlays().ToList().Add(play);
            _context.SaveChanges();

            return play;
        }
    }
}
