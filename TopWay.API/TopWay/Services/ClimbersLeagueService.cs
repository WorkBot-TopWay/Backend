using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Repositories;
using TopWay.API.TopWay.Domain.Services;
using TopWay.API.TopWay.Domain.Services.Communication;

namespace TopWay.API.TopWay.Services;

public class ClimbersLeagueService: IClimbersLeagueService
{
    private readonly IClimbersLeagueRepository _climbersLeagueRepository;
    private readonly IScalerRepository _scalerRepository;
    private readonly ILeagueRepository _leagueRepository;
    private readonly IClimbingGymRepository _climbingGymRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ClimbersLeagueService(IClimbersLeagueRepository climbersLeagueRepository, IScalerRepository scalerRepository, 
        ILeagueRepository leagueRepository, IClimbingGymRepository climbingGymRepository, IUnitOfWork unitOfWork)
    {
        _climbersLeagueRepository = climbersLeagueRepository;
        _scalerRepository = scalerRepository;
        _leagueRepository = leagueRepository;
        _climbingGymRepository = climbingGymRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<ClimberLeagues>> GetAll()
    {
        return await _climbersLeagueRepository.GetAll();
    }

    public async Task<IEnumerable<Scaler>> FindScalersByLeagueAndClimbingGymId(int leagueId, int climbingGymId)
    {
        return await _climbersLeagueRepository.FindScalersByLeagueAndClimbingGymId(leagueId, climbingGymId);
    }

    public async Task<ClimberLeagues> FindByClimbingGymIdAndScalerIdAndLeagueId(int climbingGymId, int scalerId, int leagueId)
    {
        return await _climbersLeagueRepository.FindByClimbingGymIdAndScalerIdAndLeagueId(climbingGymId, scalerId, leagueId);
    }

    public async Task<ClimberLeagues> FindByIdAsync(int id)
    {
        return await _climbersLeagueRepository.FindByIdAsync(id);
    }

    public async Task<ClimbersLeagueResponse> AddAsync(ClimberLeagues climberLeagues, int climbingGymId, int scalerId, int leagueId)
    {
       var existingScalers = await _scalerRepository.FindByIdAsync(scalerId);
       var existingClimbingGym = await _climbingGymRepository.FindByIdAsync(climbingGymId);
       var existingLeague = await _leagueRepository.GetById(leagueId);
       var existingClimbersLeague = await _climbersLeagueRepository.FindByClimbingGymIdAndScalerIdAndLeagueId(climbingGymId, scalerId, leagueId);
       var existingScalerInLeague = await _leagueRepository.FindByClimbingGymIdAndScalarId(climbingGymId, scalerId);
       if (existingClimbersLeague != null)
       {
           return new ClimbersLeagueResponse("ClimbersLeague already exists");
       }
       if (existingScalerInLeague != null)
       {
           return new ClimbersLeagueResponse("Scaler already exists in league");
       }
       if (existingScalers == null)
       {
           return new ClimbersLeagueResponse("Scaler not found.");
       }
       if (existingClimbingGym == null) 
       {
              return new ClimbersLeagueResponse("Climbing gym not found.");
       }
       if (existingLeague == null)
       {
              return new ClimbersLeagueResponse("League not found.");
       }
       try
       {
           climberLeagues.ClimbingGyms = existingClimbingGym;
           climberLeagues.ClimbingGymId = existingClimbingGym.Id;
           climberLeagues.Scaler = existingScalers;
           climberLeagues.ScalerId = existingScalers.Id;
           climberLeagues.League = existingLeague;
           climberLeagues.LeagueId = existingLeague.Id;
           await _climbersLeagueRepository.AddAsync(climberLeagues);
           await _unitOfWork.CompleteAsync();
           return new ClimbersLeagueResponse(climberLeagues);
       }
       catch (Exception ex)
       {
           return new ClimbersLeagueResponse($"An error occurred when saving the climber: {ex.Message}");
       }
    }

    public async Task<ClimbersLeagueResponse> Delete(int climbingGymId, int scalerId, int leagueId)
    {
        var existingScalers = await _scalerRepository.FindByIdAsync(scalerId);
        var existingClimbingGym = await _climbingGymRepository.FindByIdAsync(climbingGymId);
        var existingLeague = await _leagueRepository.GetById(leagueId);
        if (existingScalers == null)
        {
            return new ClimbersLeagueResponse("Scaler not found.");
        }
        if (existingClimbingGym == null) 
        {
            return new ClimbersLeagueResponse("Climbing gym not found.");
        }
        if (existingLeague == null)
        {
            return new ClimbersLeagueResponse("League not found.");
        }
        try
        {
            var climbersLeague = await _climbersLeagueRepository.FindByClimbingGymIdAndScalerIdAndLeagueId(climbingGymId, scalerId, leagueId);
            _climbersLeagueRepository.Delete(climbersLeague);
            await _unitOfWork.CompleteAsync();
            return new ClimbersLeagueResponse(climbersLeague);
        }
        catch (Exception ex)
        {
            return new ClimbersLeagueResponse($"An error occurred when deleting the climber: {ex.Message}");
        }
    }
}