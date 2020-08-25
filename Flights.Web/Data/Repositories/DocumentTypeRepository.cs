using Flights.Web.Data;
using Flights.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace FlightTicketsSystem.Web.Data.Repositories
{
    public class DocumentTypeRepository : GenericRepository<DocumentType>, IDocumentTypeRepository
    {
        private readonly DataContext _context;

        public DocumentTypeRepository(DataContext context) : base(context)
        {
            _context = context;
        }


        public IEnumerable<SelectListItem> GetComboDocumentTypes()
        {
            var list = _context.DocumentTypes.Select(c => new SelectListItem
            {
                Text = c.Type,
                Value = c.Id.ToString()

            }).OrderBy(l => l.Text).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Select a document type...)",
                Value = "0"
            });

            return list;

        }
    }
}
