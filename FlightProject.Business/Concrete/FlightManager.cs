using AutoMapper;
using FlightProject.Business.Abstract;
using FlightProject.Core.Utilities.Results;
using FlightProject.DataAccess.Abstract;
using FlightProject.Entities.Concrete;
using FlightProject.Entities.Dtos;
using FlightServiceReference;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Authentication;
using System.Security.Claims;

namespace FlightProject.Business.Concrete
{
    public class FlightManager : IFlightService
    {
        private readonly IAirSearch _airSearch;
        private readonly IFlightDal _flightDal;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;

        public FlightManager(IAirSearch airSearch, IFlightDal flightDal, IMapper mapper, IHttpContextAccessor contextAccessor)
        {
            _airSearch = airSearch;
            _flightDal = flightDal;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
        }

        public async Task<IResult> AddAsync(FlightAddDto flightAddDto)
        {
            //Burada IHttpContextAccessor arayüzünden jwt içindeki userId'yi alıyorum ve rezervasyon eklerken bu userId'yi kullanıyorum.
            var userId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                throw new AuthenticationException("Kullanıcı Bulunamadı!");
            }

            var flight = _mapper.Map<Flight>(flightAddDto);
            flight.UserId = Guid.Parse(userId);
            flight.IsActive = true;

            var res = await _flightDal.AddAsync(flight);
            if (res)
            {
                await _flightDal.SaveAsync();
                return new SuccessResult("Başarılı!");
            }
            return new ErrorResult("Hata Meydana Geldi.!");
        }

        public IDataResult<List<FlightResponseDto>> GetAllFlights()
        {
            var flights = _flightDal.GetAll().Include(f => f.User).Include(f => f.PassengerInformation).ToList();
            if (flights.Count > 0)
            {
                var flightsDto = _mapper.Map<List<FlightResponseDto>>(flights);
                return new SuccessDataResult<List<FlightResponseDto>>(flightsDto);
            }
            return new ErrorDataResult<List<FlightResponseDto>>("Veri Bulunamadı!");
        }

        public async Task<IResult> DeleteById(string id)
        {
            var currentFlight = await _flightDal.GetByIdAsync(id);
            if (currentFlight is null)
            {
                return new ErrorResult("Rezervasyon Bulunamadı!");
            }
            currentFlight.IsActive = false;
            await _flightDal.SaveAsync();

            return new SuccessResult("Rezervasyon İptal Edildi.");

        }

        public async Task<IDataResult<SearchResult>> Search(SearchRequest search)
        {
            var result = await _airSearch.AvailabilitySearchAsync(search);
            if (result.HasError)
            {
                //Todo throw custom Exception
                throw new Exception("Uçuşları Listelerken Hata Meydana Geldi!");
            }
            return new SuccessDataResult<SearchResult>(result);
        }

        public IDataResult<List<FlightResponseDto>> GetAllCurrentUserFlight()
        {
            var userId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                throw new AuthenticationException("Kullanıcı Bulunamadı!");
            }

            var flights = _flightDal.GetAll()
                .Include(f => f.User)
                .Include(f => f.PassengerInformation)
                .Where(f => f.UserId == Guid.Parse(userId))
                .OrderByDescending(f => f.CreatedDate)
                .ToList();
            if (flights.Count > 0)
            {
                var flightsDto = _mapper.Map<List<FlightResponseDto>>(flights);
                return new SuccessDataResult<List<FlightResponseDto>>(flightsDto);
            }
            return new ErrorDataResult<List<FlightResponseDto>>("Veri Bulunamadı!");
        }
    }
}
