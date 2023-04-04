using AutoMapper;
using Tour.Infrastructure.Data;
using Tour.Infrastructure.Repositories;

using Tour.Application.Interfaces;
using Tour.Domain.Entities;
using Tour.Application.Dto;
using Tour.Infrastructure.Common;

namespace Tour.Infrastructure.Services
{
    public class CityService : Service<City,CityDto> , ICityService
    {
        private readonly IRepository<City> _repository;
        private readonly IMapper _mapper;
        private readonly TourDatabaseContext _context;
        private readonly IUnitOfWork _unitOfWork;
        public CityService(IRepository<City> repository, IMapper mapper, TourDatabaseContext context,IUnitOfWork unitOfWork ) 
            : base(repository, mapper,unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _context = context;
            _unitOfWork = unitOfWork;
        }
    }
}
