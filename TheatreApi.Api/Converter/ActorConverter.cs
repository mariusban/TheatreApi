using Model = TheatreApi.DataAccess.Models;

namespace TheatreApi.Api.Converter
{
    public interface IActorConverter
    {
        Model.Actor ToModel(Dto.Actor actor);
        Dto.Actor ToDto(Model.Actor model);
    }


    public class ActorConverter : IActorConverter
    {
        public Dto.Actor ToDto(Model.Actor model)
        {
            Dto.Actor actor = new Dto.Actor()
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email
            };

            return actor;
        }

        public Model.Actor ToModel(Dto.Actor actor)
        {
            Model.Actor model = new Model.Actor()
            {
                Id = actor.Id,
                FirstName = actor.FirstName,
                LastName = actor.LastName,
                Email = actor.Email
            };

            return model;
        }
    }
}
