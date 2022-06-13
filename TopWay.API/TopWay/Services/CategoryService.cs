using TopWay.API.Shared.Domain.Repositories;
using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Repositories;
using TopWay.API.TopWay.Domain.Services;
using TopWay.API.TopWay.Domain.Services.Communication;

namespace TopWay.API.TopWay.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
    {
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Categories>> ListAsync()
    {
        return await _categoryRepository.ListAsync();
    }

    public async Task<Categories> FindByIdAsync(int id)
    {
        return await _categoryRepository.FindByIdAsync(id);
    }

    public async Task<Categories> FindByNameAsync(string name)
    {
        return await _categoryRepository.FindByNameAsync(name);
    }

    public async Task<CategoryResponse> SaveAsync(Categories categories)
    {
        try
        {
            await _categoryRepository.AddAsync(categories);
            await _unitOfWork.CompleteAsync();

            return new CategoryResponse(categories);
        }
        catch (Exception ex)
        {
            // Do some logging stuff
            return new CategoryResponse($"An error occurred when saving the category: {ex.Message}");
        }
    }

    public async Task<CategoryResponse> UpdateAsync(int id, Categories climbingGym)
    {
        var existingCategory = await _categoryRepository.FindByIdAsync(id);
        if (existingCategory == null)
            return new CategoryResponse("Category not found.");
        
        existingCategory.Name = climbingGym.Name;
        try
        {
            _categoryRepository.Update(existingCategory);
            await _unitOfWork.CompleteAsync();
            return new CategoryResponse(existingCategory);
        }
        catch (Exception ex)
        {
            // Do some logging stuff
            return new CategoryResponse($"An error occurred when updating the category: {ex.Message}");
        }
    }

        public async Task<CategoryResponse> DeleteAsync(int id)
    {
        var existingCategory = await _categoryRepository.FindByIdAsync(id);
        if (existingCategory == null)
            return new CategoryResponse("Category not found.");
        
        try
        {
            _categoryRepository.Delete(existingCategory);
            await _unitOfWork.CompleteAsync();
            return new CategoryResponse(existingCategory);
        }
        catch (Exception ex)
        {
            // Do some logging stuff
            return new CategoryResponse($"An error occurred when deleting the category: {ex.Message}");
        }
    }
}