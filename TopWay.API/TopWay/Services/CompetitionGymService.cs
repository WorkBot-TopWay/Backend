using TopWay.API.Shared.Domain.Repositories;
using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Repositories;
using TopWay.API.TopWay.Domain.Services;
using TopWay.API.TopWay.Domain.Services.Communication;

namespace TopWay.API.TopWay.Services;

public class CompetitionGymService : ICompetitionGymService
{
    private readonly ICompetitionGymRepository _competitionGymRepository;
    private readonly IClimbingGymRepository _climbingGymRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CompetitionGymService(ICompetitionGymRepository competitionGymRepository,
        IUnitOfWork unitOfWork, IClimbingGymRepository climbingGymRepository)
    {
        _competitionGymRepository = competitionGymRepository;
        _unitOfWork = unitOfWork;
        _climbingGymRepository = climbingGymRepository;
    }

    public async Task<IEnumerable<CompetitionGyms>> ListAsync()
    {
        return await _competitionGymRepository.ListAsync();
    }

    public async Task<IEnumerable<CompetitionGyms>> FindByClimbingGymIdAsync(int climbingGymId)
    {
        return await _competitionGymRepository.FindByClimbingGymIdAsync(climbingGymId);
    }

    public async Task<CompetitionGyms> FindByIdAsync(int id)
    {
        return await _competitionGymRepository.FindByIdAsync(id);
    }

    public async Task<CompetitionGymResponse> SaveAsync(CompetitionGyms competitionGyms, int climbingGymId)
    {
        var existingClimbingGym = await _climbingGymRepository.FindByIdAsync(climbingGymId);
        if (existingClimbingGym == null)
        {
            return new CompetitionGymResponse("Climbing gym not found.");
        }
        competitionGyms.Date = DateTime.Now;
        competitionGyms.ClimbingGyms = existingClimbingGym;
        competitionGyms.ClimberGymId = existingClimbingGym.Id;
        try
        {
            await _competitionGymRepository.AddAsync(competitionGyms);
            await _unitOfWork.CompleteAsync();

            return new CompetitionGymResponse(competitionGyms);
        }
        catch (Exception ex)
        {
            // Do some logging stuff
            return new CompetitionGymResponse($"An error occurred when saving the competition gym: {ex.Message}");
        }
    }

    public async Task<CompetitionGymResponse> UpdateAsync(int id, CompetitionGyms competitionGyms)
    {
        var existingCompetitionGym = await _competitionGymRepository.FindByIdAsync(id);

        if (existingCompetitionGym == null)
            return new CompetitionGymResponse("Competition gym not found.");

        existingCompetitionGym.Name = competitionGyms.Name;
        existingCompetitionGym.type = competitionGyms.type;
        existingCompetitionGym.Price = competitionGyms.Price;
        try
        {
            _competitionGymRepository.Update(existingCompetitionGym);
            await _unitOfWork.CompleteAsync();

            return new CompetitionGymResponse(existingCompetitionGym);
        }
        catch (Exception ex)
        {
            // Do some logging stuff
            return new CompetitionGymResponse($"An error occurred when updating the competition gym: {ex.Message}");
        }
    }

    public async Task<CompetitionGymResponse> DeleteAsync(int id)
    {
        var existingCompetitionGym = await _competitionGymRepository.FindByIdAsync(id);

        if (existingCompetitionGym == null)
            return new CompetitionGymResponse("Competition gym not found.");

        try
        {
            _competitionGymRepository.Delete(existingCompetitionGym);
            await _unitOfWork.CompleteAsync();

            return new CompetitionGymResponse(existingCompetitionGym);
        }
        catch (Exception ex)
        {
            // Do some logging stuff
            return new CompetitionGymResponse($"An error occurred when deleting the competition gym: {ex.Message}");
        }
    }
}