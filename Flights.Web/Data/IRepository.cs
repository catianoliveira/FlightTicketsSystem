using Flights.Web.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flights.Web.Data
{
    public interface IRepository
    {
        void AddAirplane(Airplane airplane);

        bool AirplaneExists(int id);

        Airplane GetAirplane(int id);

        IEnumerable<Airplane> GetAirplanes();

        void RemoveAirplanes(Airplane airplane);

        Task<bool> SaveAllAsync();

        void UpdateAirplane(Airplane airplane);
    }
}