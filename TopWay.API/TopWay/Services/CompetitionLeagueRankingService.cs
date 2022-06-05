using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Repositories;
using TopWay.API.TopWay.Domain.Services;
using TopWay.API.TopWay.Domain.Services.Communication;

namespace TopWay.API.TopWay.Services;

public class CompetitionLeagueRankingService:ICompetitionLeagueRankingService
{
    private readonly ICompetitionLeagueRankingRepository _competitionLeagueRankingRepository;
    private readonly ICompetitionLeagueRepository _competitionLeagueRepository;
    private readonly IClimbersLeagueRepository _climbersLeagueRepository;
    private readonly ILeagueRepository _leagueRepository;
    private readonly IScalerRepository _scalerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CompetitionLeagueRankingService(ICompetitionLeagueRankingRepository competitionLeagueRankingRepository
        , ICompetitionLeagueRepository competitionLeagueRepository, IScalerRepository scalerRepository, IUnitOfWork unitOfWork
        , IClimbersLeagueRepository climbersLeagueRepository, ILeagueRepository leagueRepository)
    {
        _competitionLeagueRankingRepository = competitionLeagueRankingRepository;
        _competitionLeagueRepository = competitionLeagueRepository;
        _scalerRepository = scalerRepository;
        _unitOfWork = unitOfWork;
        _climbersLeagueRepository = climbersLeagueRepository;
        _leagueRepository = leagueRepository;
    }

    public async Task<IEnumerable<CompetitionLeagueRanking>> ListAsync()
    {
        return await _competitionLeagueRankingRepository.ListAsync();
    }

    public async Task<IEnumerable<Scaler>> FindScalerByCompetitionIdAsync(int competitionId)
    {
        return await _competitionLeagueRankingRepository.FindScalerByCompetitionIdAsync(competitionId);
    }

    public async Task<IEnumerable<CompetitionLeagueRanking>> FindByCompetitionLeagueIdAsync(int competitionLeagueId)
    {
        return await _competitionLeagueRankingRepository.FindByCompetitionLeagueIdAsync(competitionLeagueId);
    }

    public async Task<CompetitionLeagueRanking> FindByCompetitionLeagueIdAndScalerIdAsync(int competitionLeagueId, int scalerId)
    {
        return await _competitionLeagueRankingRepository.FindByCompetitionLeagueIdAndScalerIdAsync(competitionLeagueId, scalerId);
    }

    public async Task<CompetitionLeagueRanking> FindByIdAsync(int id)
    {
        return await _competitionLeagueRankingRepository.FindByIdAsync(id);
    }

    public async Task<CompetitionLeagueRankingResponse> AddAsync(CompetitionLeagueRanking competitionLeagueRanking, int competitionLeagueId, int scalerId)
    {
        var existingCompetitionLeagueRanking = await _competitionLeagueRankingRepository.FindByCompetitionLeagueIdAndScalerIdAsync(competitionLeagueId, scalerId);
        var existingScaler = await _scalerRepository.FindByIdAsync(scalerId);
        var existingCompetitionLeague = await _competitionLeagueRepository.FindByIdAsync(competitionLeagueId);
        var existingLeague = await _leagueRepository.GetById(existingCompetitionLeague.LeagueId);
        var existingClimbersLeague = await _climbersLeagueRepository.FindByClimbingGymIdAndScalerIdAndLeagueId(existingLeague.ClimbingGymId, scalerId, existingCompetitionLeague.LeagueId);
        if (existingClimbersLeague == null && existingLeague.ScalerId != scalerId)
        {
             return new CompetitionLeagueRankingResponse("The climber is not registered in the league.");    
        }
        if (existingCompetitionLeagueRanking != null)
        {
            return new CompetitionLeagueRankingResponse("CompetitionLeagueRanking already exists");
        }
        if (existingScaler == null)
        {
            return new CompetitionLeagueRankingResponse("Scaler does not exist");
        }
        if (existingCompetitionLeague == null)
        {
            return new CompetitionLeagueRankingResponse("CompetitionLeague does not exist");
        }
        competitionLeagueRanking.CompetitionLeague = existingCompetitionLeague;
        competitionLeagueRanking.Scaler = existingScaler;
        competitionLeagueRanking.ScalerId = scalerId;
        competitionLeagueRanking.CompetitionLeagueId = competitionLeagueId;
        try
        {
            await _competitionLeagueRankingRepository.AddAsync(competitionLeagueRanking);
            await _unitOfWork.CompleteAsync();
            return new CompetitionLeagueRankingResponse(competitionLeagueRanking);
        }
        catch (Exception ex)
        {
            return new CompetitionLeagueRankingResponse($"An error occurred when saving the CompetitionLeagueRanking: {ex.Message}");
        }
    }

    public async Task<CompetitionLeagueRankingResponse> Delete(int competitionLeagueId, int scalerId)
    {
        var existingCompetitionLeagueRanking = await _competitionLeagueRankingRepository.FindByCompetitionLeagueIdAndScalerIdAsync(competitionLeagueId, scalerId);
        var existingScaler = await _scalerRepository.FindByIdAsync(scalerId);
        var existingCompetitionLeague = await _competitionLeagueRepository.FindByIdAsync(competitionLeagueId);
        if (existingCompetitionLeagueRanking == null)
        {
            return new CompetitionLeagueRankingResponse("CompetitionLeagueRanking not found");
        }
        if (existingScaler == null)
        {
            return new CompetitionLeagueRankingResponse("Scaler does not exist");
        }
        if (existingCompetitionLeague == null)
        {
            return new CompetitionLeagueRankingResponse("CompetitionLeague does not exist");
        }
        try
        {
             _competitionLeagueRankingRepository.Delete(existingCompetitionLeagueRanking);
            await _unitOfWork.CompleteAsync();
            return new CompetitionLeagueRankingResponse(existingCompetitionLeagueRanking);
        }
        catch (Exception ex)
        {
            return new CompetitionLeagueRankingResponse($"An error occurred when deleting the CompetitionLeagueRanking: {ex.Message}");
        }
    }
}