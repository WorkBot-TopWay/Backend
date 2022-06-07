using TopWay.API.TopWay.Domain.Models;

namespace TopWay.API.TopWay.Domain.Repositories;

public interface IFeatureRepository
{
    Task<Features> FindByIdAsync(int id);
    Task AddAsync(Features features);
    void Update(Features features);
    void Delete(Features features);
}