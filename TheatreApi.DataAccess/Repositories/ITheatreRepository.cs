using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatreApi.DataAccess.Models;

namespace TheatreApi.DataAccess.Repositories
{
    public interface ITheatreRepository
    {
        IEnumerable<Theatre> GetTheatres();

        IEnumerable<Play> GetPlays();

        public Theatre GetTheatre(int id);

        public Theatre CreateTheatre(Theatre theatreModel);

        public Theatre UpdatePlay(IEnumerable<Theatre> listTheatres, int idTheatre, IEnumerable<Play> listPlays, int idPlay);

        public List<Play> GetPlays(int id);

        public IEnumerable<Play> GetPlay(DateTimeOffset? startDate, DateTimeOffset? endDate, string name);

        public Decimal GetActorsAverage();

        public Play CreatePlay(Play playModel);
    }
}
