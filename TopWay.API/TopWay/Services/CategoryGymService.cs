using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Repositories;
using TopWay.API.TopWay.Domain.Services;
using TopWay.API.TopWay.Domain.Services.Communication;

namespace TopWay.API.TopWay.Services;

public class CategoryGymService : ICategoryGymService
{
    private readonly ICategoryGymRepository _categoryGymRepository;
    private readonly IClimbingGymRepository _climbingGymRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public CategoryGymService(ICategoryGymRepository categoryGymRepository, 
        IUnitOfWork unitOfWork, IClimbingGymRepository climbingGymRepository, 
        ICategoryRepository categoryRepository)
    {
        _categoryGymRepository = categoryGymRepository;
        _unitOfWork = unitOfWork;
        _climbingGymRepository = climbingGymRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<IEnumerable<CategoryGym>> GetAll()
    {
        return await _categoryGymRepository.GetAll();
    }

    public async Task<IEnumerable<ClimbingGym>> FindClimbingGymsByCategoryIdAsync(int categoryId)
    {
        return await _categoryGymRepository.FindClimbingGymsByCategoryIdAsync(categoryId);
    }

    public async Task<IEnumerable<Category>> FindCategoriesByGymIdAsync(int gymId)
    {
        return await _categoryGymRepository.FindCategoriesByGymIdAsync(gymId);
    }

    public async Task<CategoryGym> FindByCategoryIdAndClimbingGymIdAsync(int categoryId, int climbingGymId)
    {
        return await _categoryGymRepository.FindByCategoryIdAndClimbingGymIdAsync(categoryId, climbingGymId);
    }

    public async Task<CategoryGym> FindByIdAsync(int id)
    {
        return await _categoryGymRepository.FindByIdAsync(id);
    }

    public async Task<CategoryGymResponse> SaveAsync(CategoryGym categoryGym, int climbingGymId, int categoryId)
    {
        var existingCategory = await _categoryRepository.FindByIdAsync(categoryId);
        var existingClimbingGym = await _climbingGymRepository.FindByIdAsync(climbingGymId);
        var existingCategoryGym = await _categoryGymRepository.FindByCategoryIdAndClimbingGymIdAsync(categoryId, climbingGymId);
        if (existingCategoryGym != null)
        {
            return new CategoryGymResponse("CategoryGym already exists");
        }
        if (existingCategory == null)
        {
            return new CategoryGymResponse("Category not found.");
        }

        if (existingClimbingGym == null)
        {
            return new CategoryGymResponse("Climbing gym not found.");
        }

        categoryGym.CategoryId = existingCategory.Id;
        categoryGym.ClimbingGymId = existingClimbingGym.Id;
        categoryGym.CategoryName = existingCategory.Name;
        categoryGym.ClimbingGymName = existingClimbingGym.Name;
        categoryGym.Category = existingCategory;
        categoryGym.ClimbingGym = existingClimbingGym;
        try
        {
            await _categoryGymRepository.AddAsync(categoryGym);
            await _unitOfWork.CompleteAsync();
            return new CategoryGymResponse(categoryGym);
        }
        catch (Exception ex)
        {
            return new CategoryGymResponse($"An error occurred when saving the category: {ex.Message}");


        }
    }

    public async Task<CategoryGymResponse> DeleteAsync(int climbingGymId, int categoryId)
    {
        var existingCategory = await _categoryRepository.FindByIdAsync(categoryId);
        var existingClimbingGym = await _climbingGymRepository.FindByIdAsync(climbingGymId);
        if (existingCategory == null)
        {
            return new CategoryGymResponse("Category not found.");
        }

        if (existingClimbingGym == null)
        {
            return new CategoryGymResponse("Climbing gym not found.");
        }
        var categoryGym = await _categoryGymRepository.FindByCategoryIdAndClimbingGymIdAsync(categoryId, climbingGymId);
        try
        {
            _categoryGymRepository.Delete(categoryGym);
            await _unitOfWork.CompleteAsync();
            return new CategoryGymResponse(categoryGym);
        }
        catch (Exception ex)
        {
            return new CategoryGymResponse($"An error occurred when deleting the category: {ex.Message}");
        }
    }
}