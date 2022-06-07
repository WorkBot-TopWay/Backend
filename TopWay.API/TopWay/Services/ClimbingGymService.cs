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
    
    public async Task<IEnumerable<ClimbingGyms>> ListAsync()
    {
        return await _climbingGymRepository.ListAsync();
    }

    public async Task<ClimbingGyms> FindByIdAsync(int id)
    {
        return await _climbingGymRepository.FindByIdAsync(id);
    }

    public async Task<ClimbingGyms> FindByNameAsync(string name)
    {
        return await _climbingGymRepository.FindByNameAsync(name);
    }

    public async Task<ClimbingGymResponse> SaveAsync(ClimbingGyms climbingGyms)
    {
        climbingGyms.type = "ClimbingGym";
        try
        {
            await _climbingGymRepository.AddAsync(climbingGyms);
            await _unitOfWork.CompleteAsync();

            return new ClimbingGymResponse(climbingGyms);
        }
        catch (Exception ex)
        {
            return new ClimbingGymResponse($"An error occurred when saving the climbing gym: {ex.Message}");
        }
    }

    public async Task<ClimbingGymResponse> UpdateAsync(int id, ClimbingGyms climbingGyms)
    {
        var existingClimbingGym =await _climbingGymRepository.FindByIdAsync(id);
        if (existingClimbingGym == null)
            return new ClimbingGymResponse("Climbing gym not found.");
        
        existingClimbingGym.Name = climbingGyms.Name;
        existingClimbingGym.Password = climbingGyms.Password;
        existingClimbingGym.Email = climbingGyms.Email;
        existingClimbingGym.City = climbingGyms.City;
        existingClimbingGym.District = climbingGyms.District;
        existingClimbingGym.Address = climbingGyms.Address;
        existingClimbingGym.Phone = climbingGyms.Phone;
        existingClimbingGym.LogoUrl = climbingGyms.LogoUrl;
        
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