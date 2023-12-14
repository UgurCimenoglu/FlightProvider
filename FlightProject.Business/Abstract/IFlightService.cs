using FlightProject.Entities.Dtos;
using FlightServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightProject.Core.Utilities.Results;
using FlightProject.Entities.Concrete;

namespace FlightProject.Business.Abstract
{
    public interface IFlightService
    {
        Task<IDataResult<SearchResult>> Search(SearchRequest search);
        Task<IResult> AddAsync(FlightAddDto flightAddDto);
        IDataResult<List<FlightResponseDto>> GetAllFlights();
        IDataResult<List<FlightResponseDto>> GetAllCurrentUserFlight();
        Task<IResult> DeleteById(string id);
    }
}
