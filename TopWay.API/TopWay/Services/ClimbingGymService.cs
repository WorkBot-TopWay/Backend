using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Repositories;
using TopWay.API.TopWay.Domain.Services;
using TopWay.API.TopWay.Domain.Services.Communication;

namespace TopWay.API.TopWay.Services;

public class ClimbingGymService : IClimbingGymService
{
    private readonly IClimbingGymRepository _climbingGymRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public ClimbingGymService(IClimbingGymRepository climbingGymRepository, IUnitOfWork unitOfWork)
    {
        _climbingGymRepository = climbingGymRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<IEnumerable<ClimbingGym>> ListAsync()
    {
        return await _climbingGymRepository.ListAsync();
    }

    public async Task<ClimbingGym> FindByIdAsync(int id)
    {
        return await _climbingGymRepository.FindByIdAsync(id);
    }

    public async Task<ClimbingGym> FindByNameAsync(string name)
    {
        return await _climbingGymRepository.FindByNameAsync(name);
    }

    public async Task<ClimbingGymResponse> SaveAsync(ClimbingGym climbingGym)
    {
        climbingGym.type = "ClimbingGym";
        try
        {
            await _climbingGymRepository.AddAsync(climbingGym);
            await _unitOfWork.CompleteAsync();

            return new ClimbingGymResponse(climbingGym);
        }
        catch (Exception ex)
        {
            return new ClimbingGymResponse($"An error occurred when saving the climbing gym: {ex.Message}");
        }
    }

    public async Task<ClimbingGymResponse> UpdateAsync(int id, ClimbingGym climbingGym)
    {
        var existingClimbingGym =await _climbingGymRepository.FindByIdAsync(id);
        if (existingClimbingGym == null)
            return new ClimbingGymResponse("Climbing gym not found.");
        
        existingClimbingGym.Name = climbingGym.Name;
        existingClimbingGym.Password = climbingGym.Password;
        existingClimbingGym.Email = climbingGym.Email;
        existingClimbingGym.City = climbingGym.City;
        existingClimbingGym.District = climbingGym.District;
        existingClimbingGym.Address = climbingGym.Address;
        existingClimbingGym.Phone = climbingGym.Phone;
        existingClimbingGym.LogoUrl = climbingGym.LogoUrl;
        
        try
        {
            _climbingGymRepository.Update(existingClimbingGym);
            await _unitOfWork.CompleteAsync();
            return new ClimbingGymResponse(existingClimbingGym);     
        }
        catch (Exception ex)
        {
            return new ClimbingGymResponse($"An error occurred when updating the climbing gym: {ex.Message}");
        }
    }

    public async Task<ClimbingGymResponse> DeleteAsync(int id)
    {
        var existingScaler = await _climbingGymRepository.FindByIdAsync(id);
        if (existingScaler == null)
        {
            return new ClimbingGymResponse("Climbing gym not found.");
        }
        try
        {
            _climbingGymRepository.Delete(existingScaler);
            await _unitOfWork.CompleteAsync();

            return new ClimbingGymResponse(existingScaler);
        }
        catch (Exception ex)
        {
            return new ClimbingGymResponse($"An error occurred when deleting the climbing gym: {ex.Message}");
        }
    }
}