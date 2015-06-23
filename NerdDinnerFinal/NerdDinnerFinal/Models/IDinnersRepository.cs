using System.Linq;

namespace NerdDinnerFinal.Models
{
    public interface IDinnersRepository
    {
        IQueryable<Dinner> FindAllDinners();
        IQueryable<Dinner> FindUpcomingDinners();
        IQueryable<Dinner> FindByLocation(float latitude, float longitude);
        Dinner GetDinner(int id);

        void Add(Dinner dinner);
        void Delete(Dinner dinner);

        void Save();
    }
}