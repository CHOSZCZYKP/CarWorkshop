﻿using AutoMapper;
using CarWorkshop.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Application.CarWorkshopService.Queries.GetCarWorkshopServices
{
    public class GetCarWorkshopServicesQueryHandler : IRequestHandler<GetCarWorkshopServicesQuery, IEnumerable<CarWorkshopServiceDto>>
    {
        private readonly ICarWorkshopServiceRepository _carWorkshopServiceRepository;
        private readonly IMapper _mapper;

        public GetCarWorkshopServicesQueryHandler(ICarWorkshopServiceRepository carWorkshopServiceRepository, IMapper mapper)
        {
            this._carWorkshopServiceRepository = carWorkshopServiceRepository;
            this._mapper = mapper;
        }

        public async Task<IEnumerable<CarWorkshopServiceDto>> Handle(GetCarWorkshopServicesQuery request, CancellationToken cancellationToken)
        {
            var result = await _carWorkshopServiceRepository.GetAllByEncodedName(request.EncodedName);
            var dtos = _mapper.Map<IEnumerable<CarWorkshopServiceDto>>(result);

            return dtos;
        }
    }
}
