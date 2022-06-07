using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Services.Communication;

namespace TopWay.API.TopWay.Domain.Services;

public interface IFeatureService
{
    Task<Features> FindByIdAsync(int id);
    Task<FeatureResponse> AddAsync(Features features, int id);
    Task<FeatureResponse> Update(Features features, int id);
    Task<FeatureResponse> Delete(int id);
}