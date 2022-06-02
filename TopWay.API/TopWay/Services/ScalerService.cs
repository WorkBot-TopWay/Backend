using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Repositories;
using TopWay.API.TopWay.Domain.Services;
using TopWay.API.TopWay.Domain.Services.Communication;

namespace TopWay.API.TopWay.Services;

public class ScalerService : IScalerService
{
    private readonly IScalerRepository _scalerRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public ScalerService(IScalerRepository scalerRepository, IUnitOfWork unitOfWork)
    {
        _scalerRepository = scalerRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<IEnumerable<Scaler>> ListAsync()
    {
        return await _scalerRepository.ListAsync();
    }
    
    public async Task<Scaler> FindByIdAsync(int id)
    {
        return await _scalerRepository.FindByIdAsync(id);
    }
    public async Task<ScalerResponse> SaveAsync(Scaler scaler)
    {
        scaler.Type= "Scaler";
        try
        {
            await _scalerRepository.AddAsync(scaler);
            await _unitOfWork.CompleteAsync();

            return new ScalerResponse(scaler);
        }
        catch (Exception ex)
        {
            return new ScalerResponse($"An error occurred when saving the scaler: {ex.Message}");
        }
    }

    public async Task<ScalerResponse> UpdateAsync(int id, Scaler scaler)
    {
        var existingScaler = await  _scalerRepository.FindByIdAsync(id);
        if (existingScaler == null)
            return new ScalerResponse("Scaler not found.");
        
        existingScaler.FirstName = scaler.FirstName;
        existingScaler.LastName = scaler.LastName;
        existingScaler.Email = scaler.Email;
        existingScaler.Phone = scaler.Phone;
        existingScaler.Address = scaler.Address;
        existingScaler.City = scaler.City;
        existingScaler.District = scaler.District;
        existingScaler.Password = scaler.Password;
        existingScaler.UrlPhoto = scaler.UrlPhoto;
        
        try
        {
             _scalerRepository.Update(existingScaler);
             await _unitOfWork.CompleteAsync();

            return new ScalerResponse(existingScaler);
        }
        catch (Exception ex)
        {
            return new ScalerResponse($"An error occurred when updating the scaler: {ex.Message}");
        }
        
    }

    public async Task<ScalerResponse> DeleteAsync(int id)
    {
        var existingScaler = await _scalerRepository.FindByIdAsync(id);
        if (existingScaler == null)
        {
            return new ScalerResponse("Scaler not found.");
        }
        try
        {
            _scalerRepository.Delete(existingScaler);
            await _unitOfWork.CompleteAsync();

            return new ScalerResponse(existingScaler);
        }
        catch (Exception ex)
        {
            return new ScalerResponse($"An error occurred when deleting the scaler: {ex.Message}");
        }
    }
}