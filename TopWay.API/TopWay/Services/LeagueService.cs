using TopWay.API.Security.Domain.Repositories;
using TopWay.API.Shared.Domain.Repositories;
using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Repositories;
using TopWay.API.TopWay.Domain.Services;
using TopWay.API.TopWay.Domain.Services.Communication;

namespace TopWay.API.TopWay.Services;

public class LeagueService : ILeagueService
{
    private readonly ILeagueRepository _leagueRepository;
    private readonly IScalerRepository _scalerRepository;
    private readonly IClimbingGymRepository _climbingGymRepository;
    private readonly IUnitOfWork _unitOfWork;

    public LeagueService(ILeagueRepository leagueRepository, IScalerRepository scalerRepository,
        IClimbingGymRepository climbingGymRepository, IUnitOfWork unitOfWork)
    {
        _leagueRepository = leagueRepository;
        _scalerRepository = scalerRepository;
        _climbingGymRepository = climbingGymRepository;
        _unitOfWork = unitOfWork;
    }


    public async Task<IEnumerable<League>> GetAll()
    {
        return await _leagueRepository.GetAll();
    }

    public async Task<IEnumerable<League>> FindByClimbingGymId(int climbingGymId)
    {
        return await _leagueRepository.FindByClimbingGymId(climbingGymId);
    }


    public async Task<League> GetById(int id)
    {
        return await _leagueRepository.GetById(id);
    }

    public async Task<LeagueResponse> Add(League league, int climbingGymId, int scaleId)
    {
        var existingClimbingGym = await _climbingGymRepository.FindByIdAsync(climbingGymId);
        var existingScale = await _scalerRepository.FindByIdAsync(scaleId);
        if (existingClimbingGym == null)
        {
            return new LeagueResponse("Climbing gym not found.");
        }

        if (existingScale == null)
        {
            return new LeagueResponse("Scale not found.");
        }

        league.ClimbingGyms = existingClimbingGym;
        league.Scaler = existingScale;
        league.ScalerId = scaleId;
        league.ClimbingGymId = climbingGymId;
        league.AdminName = existingScale.FirstName + " " + existingScale.LastName;
        league.NumberParticipants = 1;
        try
        {
            await _leagueRepository.AddAsync(league);
            await _unitOfWork.CompleteAsync();
            return new LeagueResponse(league);
        }
        catch (Exception ex)
        {
            return new LeagueResponse($"An error occurred when saving the league: {ex.Message}");
        }
    }

    public async Task<LeagueResponse> Update(League league, int leagueId)
    {
        var existingLeague = await _leagueRepository.GetById(leagueId);
        if (existingLeague == null)
        {
            return new LeagueResponse("League not found.");
        }
        existingLeague.Name = league.Name;
        existingLeague.Description = league.Description;
        existingLeague.UrlLogo = league.UrlLogo;
        try
        {
            _leagueRepository.Update(existingLeague);
            await _unitOfWork.CompleteAsync();
            return new LeagueResponse(existingLeague);
        }
        catch (Exception ex)
        {
            return new LeagueResponse($"An error occurred when updating the league: {ex.Message}");
        }
    }

    public async Task<LeagueResponse> UpdateNumberParticipant(int leagueId, int scaleId)
    {
       var exitingLeague = await _leagueRepository.GetById(leagueId);
       var exitingScale = await _scalerRepository.FindByIdAsync(scaleId);
       if (exitingLeague == null)
       {
            return new LeagueResponse("League not found.");
       }
       if(exitingScale == null)
       {
            return new LeagueResponse("Scale not found.");
       }

       if (exitingLeague.ScalerId == scaleId)
       {
            return new LeagueResponse("This scale is already in this league.");
       }
        
       exitingLeague.NumberParticipants++;
       try
       {
            _leagueRepository.Update(exitingLeague);
            await _unitOfWork.CompleteAsync();
            return new LeagueResponse(exitingLeague);
       }
       catch (Exception ex) 
       {
            return new LeagueResponse($"An error occurred when updating the league: {ex.Message}");
       }
    }

    public async Task<LeagueResponse> DeleteParticipant(int leagueId, int scaleId)
    {
        var exitingLeague = await _leagueRepository.GetById(leagueId);
        var exitingScale = await _scalerRepository.FindByIdAsync(scaleId);
        if (exitingLeague == null)
        {
            return new LeagueResponse("League not found.");
        }
        if(exitingScale == null)
        {
            return new LeagueResponse("Scale not found.");
        }

        if (exitingLeague.ScalerId == scaleId)
        {
            return new LeagueResponse("You cannot delete the league because you are an administrator.");
        }
        exitingLeague.NumberParticipants--;
        try
        {
            _leagueRepository.Update(exitingLeague);
            await _unitOfWork.CompleteAsync();
            return new LeagueResponse(exitingLeague);
        }
        catch (Exception ex)
        {
            return new LeagueResponse($"An error occurred when updating the league: {ex.Message}");
        }
    }

    public async Task<LeagueResponse> Delete(int scaleId)
    {
        var existingLeague = await _leagueRepository.GetById(scaleId);
        if (existingLeague == null)
        {
            return new LeagueResponse("League not found.");
        }

        try
        {
            _leagueRepository.Delete(existingLeague);
            await _unitOfWork.CompleteAsync();
            return new LeagueResponse(existingLeague);
        }
        catch (Exception ex)
        {
            return new LeagueResponse($"An error occurred when deleting the league: {ex.Message}");
        }
    }
}