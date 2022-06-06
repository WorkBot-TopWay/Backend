using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TopWay.API.Shared.Extensions;
using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Services;
using TopWay.API.TopWay.Resources;

namespace TopWay.API.TopWay.Controllers;

[Route("/api/v1/[controller]")]
public class FavoriteController: ControllerBase
{
    private readonly IFavoriteService _favoriteService;
    private readonly IMapper _mapper;
    
    public FavoriteController(IFavoriteService favoriteService, IMapper mapper)
    {
        _favoriteService = favoriteService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IEnumerable<FavoriteResource>> GetAllAsync()
    {
        var favorite = await _favoriteService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Favorite>, IEnumerable<FavoriteResource>>(favorite);
        return resources;
    }
    
    [HttpGet("findByScalerId/{id}")]
    public async Task<IEnumerable<FavoriteResource>> FindByScalerIdAsync(int id)
    {
        var favorite = await _favoriteService.FindByScalerIdAsync(id);
        var resources = _mapper.Map<IEnumerable<Favorite>, IEnumerable<FavoriteResource>>(favorite);
        return resources;
    }
    
    [HttpGet("FindByClimbingGymIdAndScalerId/")]
    public async Task<IActionResult> FindByClimbingGymIdAndScalerIdAsync(int climbingGymId, int scalerId)
    {
        var favorite = await _favoriteService.FindByClimbingGymIdAndScalerIdAsync(climbingGymId, scalerId);
        if (favorite == null)
        {
            return NotFound();
        }
        var resource = _mapper.Map<Favorite, FavoriteResource>(favorite);
        return Ok(resource);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveFavoriteResource resource, int climbingGymId,
        int scalerId)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        var favorite = _mapper.Map<SaveFavoriteResource, Favorite>(resource);
        var result = await _favoriteService.AddAsync(favorite, climbingGymId, scalerId);
        if (!result.Success)
            return BadRequest(result.Message);
        var favoriteResource = _mapper.Map<Favorite, FavoriteResource>(result.Resource);
        return Ok(favoriteResource);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveFavoriteResource resource, int climbingGymId,
        int scalerId)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        var favorite = _mapper.Map<SaveFavoriteResource, Favorite>(resource);
        var result = await _favoriteService.UpdateAsync(favorite, climbingGymId, scalerId);
        if (!result.Success)
            return BadRequest(result.Message);
        var favoriteResource = _mapper.Map<Favorite, FavoriteResource>(result.Resource);
        return Ok(favoriteResource);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int climbingGymId, int scalerId)
    {
        var result = await _favoriteService.Delete(climbingGymId, scalerId);
        if (!result.Success)
        {
            return BadRequest(result.Message);
        }
        var favoriteResource = _mapper.Map<Favorite, FavoriteResource>(result.Resource);
        return Ok(favoriteResource);
    }
}