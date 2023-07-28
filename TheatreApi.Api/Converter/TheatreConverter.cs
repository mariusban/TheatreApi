using Model = TheatreApi.DataAccess.Models;

namespace TheatreApi.Api.Converter
{
    public interface ITheatreConverter
    {
        Model.Theatre ToModel(Dto.Theatre theatre);
        Dto.Theatre ToDto(Model.Theatre model);
    }

    public class TheatreConverter : ITheatreConverter
    {
        private readonly IPlayConverter _playConverter;

        public TheatreConverter(IPlayConverter playConverter)
        {
            _playConverter = playConverter;
        }

        public Model.Theatre ToModel(Dto.Theatre theatre)
        {
            var listPlaysTemp = new List<Model.Play>();
            if (theatre != null)
            {
                if (theatre.Plays != null)
                {
                    theatre.Plays.ForEach(play => listPlaysTemp.Add(_playConverter.ToModel(play)));
                }

                Model.Theatre theatreModel = new Model.Theatre()
                {
                    Id = theatre.Id,
                    Name = theatre.Name,
                    Plays = listPlaysTemp
                };

                return theatreModel;
            }
            return null;
        }


        public Dto.Theatre ToDto(Model.Theatre model)
        {
            var listPlaysTemp = new List<Dto.Play>();
            if (model != null)
            {
                if (model.Plays != null)
                {
                    model.Plays.ForEach(play => listPlaysTemp.Add(_playConverter.ToDto(play)));
                }

                Dto.Theatre theatre = new Dto.Theatre()
                {
                    Id = model.Id,
                    Name = model.Name,
                    Plays = listPlaysTemp
                };

                return theatre;
            }
            return null;
        }
    }
}
