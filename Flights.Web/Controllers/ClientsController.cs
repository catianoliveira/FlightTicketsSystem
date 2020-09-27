using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flights.Web.Helpers;
using FlightTicketsSystem.Web.Data.Entities;
using FlightTicketsSystem.Web.Data.Repositories;
using FlightTicketsSystem.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlightTicketsSystem.Web.Controllers
{
    public class ClientsController : Controller
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IHistoryRepository _historyRepository;
        private readonly IUserHelper _userHelper;

        public ClientsController(
            ITicketRepository ticketRepository,
            IHistoryRepository historyRepository,
            IUserHelper userHelper)
        {
            _ticketRepository = ticketRepository;
            _historyRepository = historyRepository;
            _userHelper = userHelper;
        }


        public async Task<IActionResult> GetHistory(int? id)
        {
            if (id == null)
            {
                return View();
            }

            var ticket = await _ticketRepository.GetByIdAsync(id.Value);

            if (ticket == null)
            {
                return View();
            }

            var user = _userHelper.GetUserByEmailAsync(User.Identity.Name);

            var view = new HistoryViewModel
            {
                TicketId = ticket.Id,
                UserId = user.Id
            };

            return View(view);
        }

        [HttpPost]
        public async Task<IActionResult> GetHistory(HistoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var history = new HistoryViewModel
                    {
                        TicketId = model.TicketId,
                        UserId = model.UserId
                    };

                    await _historyRepository.CreateAsync(history);

                    model.Ticket = await _ticketRepository.GetDetailsTicketAsync(model.TicketId);

                    var histories = await _historyRepository.GetHistoriesFromUserId(model.TicketId);

                    return RedirectToAction($"{nameof(TicketDetails)}/{model.TicketId}");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(model);
        }

        public async Task<IActionResult> TicketDetails(int? id)
        {
            if (id == null)
            {
                return View();
            }

            var ticket = await _ticketRepository.GetDetailsTicketAsync(id.Value);

            if (ticket == null)
            {
                return View();
            }

            return View(ticket);
        }
    }
}
