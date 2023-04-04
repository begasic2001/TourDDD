using AutoMapper;
using Tour.Infrastructure.Data;
using Tour.Infrastructure.Repositories;

using Tour.Application.Interfaces;
using Tour.Domain.Entities;
using Tour.Application.Dto;
using Tour.Infrastructure.Common;

namespace Tour.Infrastructure.Services
{
    public class SightService : Service<Sight, SightDto>, ISightService
    {
        private readonly IRepository<Sight> _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public SightService(IRepository<Sight> repository, IMapper mapper , IUnitOfWork unitOfWork) : base(repository, mapper, unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
    }
}
