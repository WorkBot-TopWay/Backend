using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Repositories;
using TopWay.API.TopWay.Domain.Services;
using TopWay.API.TopWay.Domain.Services.Communication;

namespace TopWay.API.TopWay.Services;

public class FeatureService:IFeatureService
{
    private readonly IFeatureRepository _featureRepository;
    private readonly IClimbingGymRepository _climbingGymRepository;
    private readonly IUnitOfWork _unitOfWork;


    public FeatureService(IFeatureRepository featureRepository, IUnitOfWork unitOfWork, IClimbingGymRepository climbingGymRepository)
    {
        _featureRepository = featureRepository;
        _unitOfWork = unitOfWork;
        _climbingGymRepository = climbingGymRepository;
    }

    public async Task<Features> FindByIdAsync(int id)
    {
        return await _featureRepository.FindByIdAsync(id);
    }

    public async Task<FeatureResponse> AddAsync(Features features, int id)
    {
        var existingClimbingGym = await _climbingGymRepository.FindByIdAsync(id);
        if (existingClimbingGym == null)
        {
            return new FeatureResponse("Climbing gym not found.");
        }
        features.Id = existingClimbingGym.Id;
        features.ClimbingGymsId = existingClimbingGym.Id;
        features.ClimbingGyms = existingClimbingGym;
        try
        {
            await _featureRepository.AddAsync(features);
            await _unitOfWork.CompleteAsync();

            return new FeatureResponse(features);
        }
        catch (Exception ex)
        {
            return new FeatureResponse($"An error occurred when saving the climbing gym: {ex.Message}");
        }
    }


    public async Task<FeatureResponse> Update(Features features, int id)
    {
        var existingFeature = await _featureRepository.FindByIdAsync(id);
        if (existingFeature == null)
        {
            return new FeatureResponse("Feature not found.");
        }
        existingFeature.Type_climb = features.Type_climb;
        existingFeature.Office_hours_start = features.Office_hours_start;
        existingFeature.Office_hours_end = features.Office_hours_end;
        existingFeature.Routes = features.Routes;
        existingFeature.Max_height = features.Max_height;
        existingFeature.Rock_type = features.Rock_type;
        existingFeature.Bolting = features.Bolting;
        existingFeature.price = features.price;
        try
        {
            _featureRepository.Update(existingFeature);
            await _unitOfWork.CompleteAsync();

            return new FeatureResponse(existingFeature);
        }
        catch (Exception ex)
        {
            return new FeatureResponse($"An error occurred when updating the climbing gym: {ex.Message}");
        }
    }

    public async Task<FeatureResponse> Delete(int id)
    {
        var existingFeature = await _featureRepository.FindByIdAsync(id);
        if (existingFeature == null)
        {
            return new FeatureResponse("Feature not found.");
            
        }
        try
        {
            _featureRepository.Delete(existingFeature);
            await _unitOfWork.CompleteAsync();

            return new FeatureResponse(existingFeature);
        }
        catch (Exception ex)
        {
            return new FeatureResponse($"An error occurred when deleting the climbing gym: {ex.Message}");
        }
    }
}