using AutoMapper;
using Tour.Infrastructure.Data;
using Tour.Infrastructure.Repositories;

using Tour.Application.Interfaces;
using Tour.Domain.Entities;
using Tour.Application.Dto;
using Tour.Infrastructure.Common;

namespace Tour.Infrastructure.Services
{
    public class TourService : Service<Tours, TourDto>, ITourService
    {
        private readonly IRepository<Tours> _repository;
        private readonly IMapper _mapper;
        TourDatabaseContext _context;
        private readonly IUnitOfWork _unitOfWork;
        public TourService(IRepository<Tours> repository, IMapper mapper,IUnitOfWork unitOfWork, TourDatabaseContext context)
            : base(repository, mapper, unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _context = context;
            _unitOfWork = unitOfWork;
        }

        //public async Task<IEnumerable<object>> GetJoin()
        //{
        //    var res = await _context.Tour.Include(t => t.Transport)
        //              .Include(tc => tc.ToursCities).ThenInclude(c => c.City)
        //              .Include(ts => ts.ToursSights).ThenInclude(s => s.Sight)
        //              .Select((t) => new
        //              {
        //                  Id = t.Id,
        //                  Name = t.Name,
        //                  Price = t.Price,
        //                  MaxTourists = t.MaxTourists,
        //                  StartDate = t.StartDate,
        //                  EndDate = t.EndDate,
        //                  Transport = new Transport()
        //                  {
        //                      Id = t.Id,
        //                      TransportName = t.Transport.TransportName
        //                  },
        //                  Sight = t.ToursSights,
        //                  City = t.ToursCities

        //              })
        //              .ToListAsync();
        //    return res;
        //}

        //public async Task<Tours> GetJoinById(string id)
        //{
        //    var res = await _context.Tour.Include(t => t.Transport)
        //               .Include(tc => tc.ToursCities).ThenInclude(c => c.City)
        //               .Include(ts => ts.ToursSights).ThenInclude(s => s.Sight)
        //               .FirstOrDefaultAsync(x => x.Id == id);
        //    return res;
        //}

        //public async Task AddAsyncJoin(TourDto vm)
        //{
        //    var id = Guid.NewGuid().ToString();
        //    var tour = new Tour()
        //    {
        //        Id = id,
        //        Name = vm.Name,
        //        Price = vm.Price,
        //        MaxTourists = vm.MaxTourists,
        //        StartDate = vm.StartDate,
        //        EndDate = vm.EndDate,
        //        TransportId = vm.TransportId,
        //    };

        //    foreach (var itemCity in vm.CityId)
        //    {

        //        tour.ToursCities.Add(new ToursCities()
        //        {
        //            Tour = tour,
        //            CityId = itemCity
        //        });
        //    }

        //    foreach (var itemSight in vm.SightId)
        //    {

        //        tour.ToursSights.Add(new ToursSight()
        //        {
        //            Tour = tour,
        //            SightId = itemSight
        //        });
        //    }

        //    _context.Tour.Add(tour);
        //}

        //public async Task UpdateAsyncJoin(string id, TourDto vm)
        //{
        //    var tour = await _context.Tour.Include(x => x.ToursCities)
        //                .ThenInclude(y => y.City).FirstOrDefaultAsync(x => x.Id == id);

        //    // city
        //    var existingIds = tour.ToursCities.Select(x => x.CityId).ToList(); // 1 2
        //    var selectedIds = vm.CityId.ToList(); // 1 3
        //    var toAdd = selectedIds.Except(existingIds).ToList(); //3
        //    var toRemove = existingIds.Except(selectedIds).ToList(); // 2
        //    tour.ToursCities = tour.ToursCities.Where(x => !toRemove.Contains(x.CityId)).ToList();

        //    // sight
        //    var existingIdSight = tour.ToursSights.Select(s => s.SightId).ToList();
        //    var selectedIdSight = vm.SightId.ToList();
        //    var toAddSight = selectedIdSight.Except(existingIdSight).ToList();
        //    var toRemoveSight = existingIdSight.Except(selectedIdSight).ToList();
        //    tour.ToursSights = tour.ToursSights.Where(s => !toRemoveSight.Contains(s.SightId)).ToList();

        //    foreach (var item in toAdd)
        //    {
        //        tour.ToursCities.Add(new ToursCities()
        //        {
        //            CityId = item
        //        });
        //    }
        //    foreach (var itemSight in vm.SightId)
        //    {

        //        tour.ToursSights.Add(new ToursSight()
        //        {
        //            SightId = itemSight
        //        });
        //    }


        //    await _repository.UpdateAsync(tour);
        //}
    }
}
