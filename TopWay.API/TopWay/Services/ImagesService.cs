using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Repositories;
using TopWay.API.TopWay.Domain.Services;
using TopWay.API.TopWay.Domain.Services.Communication;

namespace TopWay.API.TopWay.Services;

public class ImagesService : IImagesService
{
    private readonly IImagesRepository _imagesRepository;
    private readonly IClimbingGymRepository _climbingGymRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ImagesService(IImagesRepository imagesRepository, IUnitOfWork unitOfWork, IClimbingGymRepository climbingGymRepository)
    {
        _imagesRepository = imagesRepository;
        _unitOfWork = unitOfWork;
        _climbingGymRepository = climbingGymRepository;
    }

    public async Task<IEnumerable<Images>> FindAllAsync()
    {
        return await _imagesRepository.ListAsync();
    }

    public async Task<IEnumerable<Images>> FindByClimbingGymIdAsync(int climbingGymId)
    {
        return await _imagesRepository.FindByClimbingGymIdAsync(climbingGymId);
    }

    public async Task<Images> FindByIdAsync(int id)
    {
        return await _imagesRepository.FindByIdAsync(id);
    }

    public async Task<ImagesResponse> SaveAsync(Images images, int climbingGymId)
    {
        var existingClimbingGym = await _climbingGymRepository.FindByIdAsync(climbingGymId);
        if (existingClimbingGym == null)
        {
            return new ImagesResponse("Climbing gym not found.");
        }
        try
        {
            existingClimbingGym.Images.Add(images);
            await _unitOfWork.CompleteAsync();
            return new ImagesResponse(images);
        }
        catch (Exception ex)
        {
            return new ImagesResponse($"An error occurred when saving the image: {ex.Message}");
        }
    }

    public async Task<ImagesResponse> DeleteAsync(int id)
    {
        var existingImage = await _imagesRepository.FindByIdAsync(id);
        if (existingImage == null)
        {
            return new ImagesResponse("Image not found.");
        }
        try
        {
            _imagesRepository.Delete(existingImage);
            await _unitOfWork.CompleteAsync();
            return new ImagesResponse(existingImage);
        }
        catch (Exception ex)
        {
            return new ImagesResponse($"An error occurred when deleting the image: {ex.Message}");
        }
    }
}