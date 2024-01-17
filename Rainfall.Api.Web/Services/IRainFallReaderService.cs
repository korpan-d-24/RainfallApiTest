using Rainfall.Api.Models;

namespace Rainfall.Api.Web.Services;

public interface IRainFallReaderService
{
    Task<RainfallReadingResponse> GetRainfallReadings(string stationId, int count = 10);
}