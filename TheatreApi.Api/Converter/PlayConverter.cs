using Model = TheatreApi.DataAccess.Models;

namespace TheatreApi.Api.Converter
{
    public interface IPlayConverter
    {
        Model.Play ToModel(Dto.Play play);
        Dto.Play ToDto(Model.Play model);
    }


    public class PlayConverter : IPlayConverter
    {
        private readonly IActorConverter _actorConverter;

        public PlayConverter(IActorConverter actorConverter)
        {
            _actorConverter = actorConverter;
        }

        public Model.Play ToModel(Dto.Play play)
        {
            var listActorsTemp = new List<Model.Actor>();

            if (play != null)
            {
                if (play.Actors != null)
                {
                    play.Actors.ForEach(actor => listActorsTemp.Add(_actorConverter.ToModel(actor)));
                }

                Model.Play playModel = new Model.Play()
                {
                    Id = play.Id,
                    Name = play.Name,
                    Date = play.Date,
                    Actors = listActorsTemp
                };

                return playModel;
            }
            return null;
        }

        public Dto.Play ToDto(Model.Play model)
        {
            var listActorsTemp = new List<Dto.Actor>();

            if (model != null)
            {
                if (model.Actors != null)
                {
                    model.Actors.ForEach(actor => listActorsTemp.Add(_actorConverter.ToDto(actor)));
                }

                Dto.Play play = new Dto.Play()
                {
                    Id = model.Id,
                    Name = model.Name,
                    Date = model.Date,
                    Actors = listActorsTemp
                };

                return play;
            }
            return null;
        }
    }
}
