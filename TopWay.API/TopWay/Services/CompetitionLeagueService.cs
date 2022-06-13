using TopWay.API.Shared.Domain.Repositories;
using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Repositories;
using TopWay.API.TopWay.Domain.Services;
using TopWay.API.TopWay.Domain.Services.Communication;

namespace TopWay.API.TopWay.Services;

public class CompetitionLeagueService : ICompetitionLeagueService
{
    private readonly ICompetitionLeagueRepository _competitionLeagueRepository;
    private readonly ILeagueRepository _leagueRepository; 
    private readonly IUnitOfWork _unitOfWork;
    
    public CompetitionLeagueService(ICompetitionLeagueRepository competitionLeagueRepository
        , IUnitOfWork unitOfWork, ILeagueRepository leagueRepository)
    {
        _competitionLeagueRepository = competitionLeagueRepository;
        _unitOfWork = unitOfWork;
        _leagueRepository = leagueRepository;
    }
    
    
    public async Task<IEnumerable<CompetitionLeague>> ListAsync()
    {
        return await _competitionLeagueRepository.ListAsync();
    }

    public async Task<IEnumerable<CompetitionLeague>> FindByLeagueIdAsync(int leagueId)
    {
        return await _competitionLeagueRepository.FindByLeagueIdAsync(leagueId);
    }

    public async Task<CompetitionLeague> FindByIdAsync(int id)
    {
        return await _competitionLeagueRepository.FindByIdAsync(id);
    }

    public async Task<CompetitionLeagueResponse> AddAsync(CompetitionLeague competitionLeague, int leagueId)
    {
        var existingLeague = await _leagueRepository.GetById(leagueId);
        if (existingLeague == null)
            return new CompetitionLeagueResponse("League not found.");
        
        competitionLeague.League = existingLeague;
        competitionLeague.LeagueId = existingLeague.Id;
        competitionLeague.Date = DateTime.Now;
        try
        {
            await _competitionLeagueRepository.AddAsync(competitionLeague);
            await _unitOfWork.CompleteAsync();

            return new CompetitionLeagueResponse(competitionLeague);
        }
        catch (Exception ex)
        {
            return new CompetitionLeagueResponse($"An error occurred when saving the competition: {ex.Message}");
        }
        
    }
    
    public async Task<CompetitionLeagueResponse> Update(CompetitionLeague competitionLeague, int id)
    {
        var existingCompetitionLeague = await _competitionLeagueRepository.FindByIdAsync(id);

        if (existingCompetitionLeague == null)
            return new CompetitionLeagueResponse("Competition not found.");

        existingCompetitionLeague.Name = competitionLeague.Name;
        existingCompetitionLeague.Date = competitionLeague.Date;
        existingCompetitionLeague.type = competitionLeague.type;

        try
        {
            _competitionLeagueRepository.Update(existingCompetitionLeague);
            await _unitOfWork.CompleteAsync();

            return new CompetitionLeagueResponse(existingCompetitionLeague);
        }
        catch (Exception ex)
        {
            return new CompetitionLeagueResponse($"An error occurred when updating the competition: {ex.Message}");
        }
    }

    public async Task<CompetitionLeagueResponse> Delete(int id)
    {
        var existingCompetitionLeague = await _competitionLeagueRepository.FindByIdAsync(id);

        if (existingCompetitionLeague == null)
            return new CompetitionLeagueResponse("Competition not found.");

        try
        {
            _competitionLeagueRepository.Delete(existingCompetitionLeague);
            await _unitOfWork.CompleteAsync();

            return new CompetitionLeagueResponse(existingCompetitionLeague);
        }
        catch (Exception ex)
        {
            return new CompetitionLeagueResponse($"An error occurred when deleting the competition: {ex.Message}");
        }
    }
}