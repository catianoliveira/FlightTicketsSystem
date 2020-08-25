using Flights.Web.Data;
using Flights.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightTicketsSystem.Web.Data.Repositories
{
    public interface IDocumentTypeRepository : IGenericRepository<DocumentType>
    {
        IEnumerable<SelectListItem> GetComboDocumentTypes();
    }
}
