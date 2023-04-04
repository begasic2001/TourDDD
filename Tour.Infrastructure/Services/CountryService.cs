using AutoMapper;
using Tour.Application.Dto;
using Tour.Application.Interfaces;
using Tour.Domain.Entities;
using Tour.Infrastructure.Common;
using Tour.Infrastructure.Repositories;

namespace Tour.Infrastructure.Services
{
    public class CountryService : Service<Country, CountryDto>, ICountryService
    {
        private readonly IRepository<Country> _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public CountryService(IRepository<Country> repository, IMapper mapper, IUnitOfWork unitOfWork) : base(repository, mapper, unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
    }
}
