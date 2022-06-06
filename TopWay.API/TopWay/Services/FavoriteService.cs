using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Repositories;
using TopWay.API.TopWay.Domain.Services;
using TopWay.API.TopWay.Domain.Services.Communication;

namespace TopWay.API.TopWay.Services;

public class FavoriteService: IFavoriteService
{
    private readonly IFavoriteRepository _favoriteRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IScalerRepository _scalerRepository;
    private readonly IClimbingGymRepository _climbingGymRepository;

    public FavoriteService(IFavoriteRepository favoriteRepository, IUnitOfWork unitOfWork,
        IScalerRepository scalerRepository, IClimbingGymRepository climbingGymRepository)
    {
        _favoriteRepository = favoriteRepository;
        _unitOfWork = unitOfWork;
        _scalerRepository = scalerRepository;
        _climbingGymRepository = climbingGymRepository;
    }

    public async Task<IEnumerable<Favorite>> ListAsync()
    {
        return await _favoriteRepository.ListAsync();
    }

    public async Task<IEnumerable<Favorite>> FindByScalerIdAsync(int scalerId)
    {
        return await _favoriteRepository.FindByScalerIdAsync(scalerId);
    }

    public async Task<Favorite> FindByClimbingGymIdAndScalerIdAsync(int climbingGymId, int scalerId)
    {
        return await _favoriteRepository.FindByClimbingGymIdAndScalerIdAsync(climbingGymId, scalerId);
    }

    public async Task<Favorite> FindByIdAsync(int id)
    {
        return await _favoriteRepository.FindByIdAsync(id);
    }

    public async Task<FavoriteResponse> AddAsync(Favorite favorite, int climbingGymId, int scalerId)
    {
        var existingScaler = await _scalerRepository.FindByIdAsync(scalerId);
        var existingClimbingGym = await _climbingGymRepository.FindByIdAsync(climbingGymId);
        var existingFavorite = await _favoriteRepository.FindByClimbingGymIdAndScalerIdAsync(climbingGymId, scalerId);
        if (existingFavorite != null)
        {
            return new FavoriteResponse("Favorite already exists");
        }
        if (existingScaler == null)
        {
            return new FavoriteResponse("Scaler not found");
        }
        if (existingClimbingGym == null)
        {
            return new FavoriteResponse("Climbing gym not found");
        }
        favorite.Scaler = existingScaler;
        favorite.ClimbingGym = existingClimbingGym;
        favorite.ScalerId = scalerId;
        favorite.ClimbingGymId = climbingGymId;

        try
        {
            favorite.ClimbingGym = existingClimbingGym;
            favorite.Scaler = existingScaler;
            await _favoriteRepository.AddAsync(favorite);
            await _unitOfWork.CompleteAsync();

            return new FavoriteResponse(favorite);
        }
        catch (Exception ex)
        {
            return new FavoriteResponse($"An error occurred when saving the favorite: {ex.Message}");
        }
    }

    public async Task<FavoriteResponse> UpdateAsync(Favorite favorite, int climbingGymId, int scalerId)
    {
        var existingFavorite = await _favoriteRepository.FindByClimbingGymIdAndScalerIdAsync(climbingGymId, scalerId);
        if (existingFavorite == null)
        {
            return new FavoriteResponse("Favorite not found");
        }
        existingFavorite.ClimbingGymId = climbingGymId;
        existingFavorite.ScalerId = scalerId;
        try
        {
            _favoriteRepository.Update(existingFavorite);
            await _unitOfWork.CompleteAsync();

            return new FavoriteResponse(existingFavorite);
        }
        catch (Exception ex)
        {
            return new FavoriteResponse($"An error occurred when updating the favorite: {ex.Message}");
        }
    }

    public async Task<FavoriteResponse> Delete(int climbingGymId, int scalerId)
    {
        var existingFavorite = await _favoriteRepository.FindByClimbingGymIdAndScalerIdAsync(climbingGymId, scalerId);
        if (existingFavorite == null)
        {
            return new FavoriteResponse("Favorite not found");
        }

        try
        {
            _favoriteRepository.Delete(existingFavorite);
            await _unitOfWork.CompleteAsync();

            return new FavoriteResponse(existingFavorite);
        }
        catch (Exception ex)
        {
            return new FavoriteResponse($"An error occurred when deleting the favorite: {ex.Message}");
        }
    }
}
