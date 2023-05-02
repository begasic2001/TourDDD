using AutoMapper;
using Tour.Infrastructure.Data;
using Tour.Infrastructure.Repositories;

using Tour.Application.Interfaces;
using Tour.Domain.Entities;
using Tour.Application.Dto;
using Tour.Infrastructure.Common;
using Tour.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Tour.Infrastructure.Services
{
    public class TourService : Service<Tours, TourDto>, ITourService
    {
        private readonly IRepository<Tours> _repository;
        private readonly IMapper _mapper;
        TourDatabaseContext _context;
        private readonly IUnitOfWork _unitOfWork;
        public static int PAGE_SIZE { get; set; } = 5;
        public TourService(IRepository<Tours> repository, IMapper mapper,IUnitOfWork unitOfWork, TourDatabaseContext context)
            : base(repository, mapper, unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _context = context;
            _unitOfWork = unitOfWork;
        }

        public SearchVM Search(string? search, double? from, double? to, string? sortBy, int page = 1)
        {
            var allProducts = _context.Tour.Include(t => t.City).Include(t=> t.Transport).Include(t=>t.Sight).AsQueryable();

            #region Filtering
            if (!string.IsNullOrEmpty(search))
            {
                allProducts = allProducts.Where(t => t.Name.Contains(search));
            }
            if (from.HasValue)
            {
                allProducts = allProducts.Where(t => t.Price >= from);
            }
            if (to.HasValue)
            {
                allProducts = allProducts.Where(t => t.Price <= to);
            }
            #endregion


            #region Sorting
            //Default sort by Name (TenHh)
            allProducts = allProducts.OrderBy(hh => hh.Name);

            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "tenhh_desc": allProducts = allProducts.OrderByDescending(hh => hh.Name); break;
                    case "gia_asc": allProducts = allProducts.OrderBy(hh => hh.Price); break;
                    case "gia_desc": allProducts = allProducts.OrderByDescending(hh => hh.Price); break;
                }
            }
            #endregion

          
            
            var result = PaginatedList<Tours>.Create(allProducts, page, PAGE_SIZE);
            var listSearch = result.Select(t => new TourVM
            {
                name = t.Name,
                TransportName = t.Transport.TransportName,
                SightName = t.Sight.SightName,
                CityName = t.City.CityName,
                Price = t.Price
            });
            
            var total = result.TotalPage;
            Console.WriteLine(total);
            //return result.Select(t => new SearchVM
            //{
            //    Results = listSearch,
            //   TotalPage = total
            //}).ToList();
            var finalResult = new SearchVM()
            {
                Results = listSearch,
                TotalPage = total
            };
            return finalResult;
        
    }
    }
}
