using AutoMapper;
using CarWorkshop.Application.CarWorkshop;
using CarWorkshop.Application.CarWorkshop.Commands.CreateCarWorkshop;
using CarWorkshop.Application.CarWorkshop.Commands.EditCarWorkshop;
using CarWorkshop.Application.CarWorkshop.Queries.GetAllCarWorkshops;
using CarWorkshop.Application.CarWorkshop.Queries.GetCarWorkshopByEncodedName;
using CarWorkshop.Domain.Entities;
using CarWorkshop.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarWorkshop.Controllers
{
    public class CarWorkshopController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public CarWorkshopController(IMediator mediator, IMapper mapper)
        {
            this._mediator = mediator;
            this._mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var carWorkshops = await _mediator.Send(new GetAllCarWorkshopsQuery());
            return View(carWorkshops); 
        }

        [Authorize(Roles ="Owner")]
        public IActionResult Create()
        {
            return View();
        }

        [Route("CarWorkshop/{encodedName}/Details")]
        public async Task<IActionResult> Details(string encodedName)
        {
            var dto = await _mediator.Send(new GetCarWorkshopByEncodedNameQuery(encodedName));
            return View(dto);
        }

        [Route("CarWorkshop/{encodedName}/Edit")]
        public async Task<IActionResult> Edit(string encodedName)
        {
            var dto = await _mediator.Send(new GetCarWorkshopByEncodedNameQuery(encodedName));

            if (!dto.IsEditable)
            {
                return RedirectToAction("NoAccess", "Home");
            }

            EditCarWorkshopCommand model = _mapper.Map<EditCarWorkshopCommand>(dto);

            return View(model);
        }

        [HttpPost]
        [Route("CarWorkshop/{encodedName}/Edit")]
        public async Task<IActionResult> Edit(string encodedName, EditCarWorkshopCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }
            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> Create(CreateCarWorkshopCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }

            await _mediator.Send(command);

            var notification = new Notification("success", $"Created carworkshop {command.Name}");
            TempData["Notification"] = notification;

            return RedirectToAction(nameof(Index));
        }
    }
}
