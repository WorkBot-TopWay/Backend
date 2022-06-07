using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Repositories;
using TopWay.API.TopWay.Domain.Services;
using TopWay.API.TopWay.Domain.Services.Communication;

namespace TopWay.API.TopWay.Services;

public class CompetitionReservationClimberService : ICompetitionReservationClimberService
{
    private readonly ICompetitionReservationClimberRepository _competitionReservationClimberRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IScalerRepository _scalerRepository;
    private readonly ICompetitionGymRepository _competitionGymRepository;


    public CompetitionReservationClimberService(ICompetitionReservationClimberRepository competitionReservationClimberRepository, 
        IUnitOfWork unitOfWork, IScalerRepository scalerRepository, ICompetitionGymRepository competitionGymRepository)
    {
        _competitionReservationClimberRepository = competitionReservationClimberRepository;
        _unitOfWork = unitOfWork;
        _scalerRepository = scalerRepository;
        _competitionGymRepository = competitionGymRepository;
    }

    public async Task<IEnumerable<CompetitionReservationClimber>> ListAsync()
    {
        return await _competitionReservationClimberRepository.ListAsync();
    }

    public async Task<IEnumerable<Scaler>> FindScalerByCompetitionIdAsync(int competitionId)
    {
        return await _competitionReservationClimberRepository.FindScalerByCompetitionIdAsync(competitionId);
    }

    public async Task<IEnumerable<CompetitionReservationClimber>> FindByCompetitionIdAsync(int competitionId)
    {
        return await _competitionReservationClimberRepository.FindByCompetitionIdAsync(competitionId);
    }


    public async Task<CompetitionReservationClimber> FindByCompetitionIdAndScalerIdAsync(int competitionId, int scalerId)
    {
        return await _competitionReservationClimberRepository.FindByCompetitionIdAndScalerIdAsync(competitionId, scalerId);
    }

    public async Task<CompetitionReservationClimber> FindByIdAsync(int id)
    {
        return await _competitionReservationClimberRepository.FindByIdAsync(id);
    }

    public async Task<CompetitionReservationClimberResponse> AddAsync(CompetitionReservationClimber competitionReservationClimber, int competitionId, int scalerId)
    {
        var existingCompetitionReservationClimber = await _competitionReservationClimberRepository.FindByCompetitionIdAndScalerIdAsync(competitionId, scalerId);
        var existingCompetitionGym = await _competitionGymRepository.FindByIdAsync(competitionId);
        var existingScaler = await _scalerRepository.FindByIdAsync(scalerId);
        if (existingCompetitionReservationClimber != null)
        {
            return new CompetitionReservationClimberResponse("CompetitionReservationClimber already exists.");
        }
        if (existingCompetitionGym == null)
        {
            return new CompetitionReservationClimberResponse("Competition does not exist.");
        }
        if (existingScaler == null)
        {
            return new CompetitionReservationClimberResponse("Scaler does not exist.");
        }
        competitionReservationClimber.Scaler = existingScaler;
        competitionReservationClimber.CompetitionGyms = existingCompetitionGym;
        competitionReservationClimber.ScalerId = scalerId;
        competitionReservationClimber.CompetitionGymId = competitionId;
        try
        {
            await _competitionReservationClimberRepository.AddAsync(competitionReservationClimber);
            await _unitOfWork.CompleteAsync();

            return new CompetitionReservationClimberResponse(competitionReservationClimber);
        }
        catch (Exception ex)
        {
            return new CompetitionReservationClimberResponse($"An error occurred when saving the CompetitionReservationClimber: {ex.Message}");
        }
        
    }


    public async Task<CompetitionReservationClimberResponse> Delete( int competitionId, int scalerId)
    {
        var existingCompetitionReservationClimber = await _competitionReservationClimberRepository.FindByCompetitionIdAndScalerIdAsync(competitionId, scalerId);
        if (existingCompetitionReservationClimber == null)
        {
            return new CompetitionReservationClimberResponse("CompetitionReservationClimber not found.");
        }
        try
        {
            _competitionReservationClimberRepository.Delete(existingCompetitionReservationClimber);
            await _unitOfWork.CompleteAsync();

            return new CompetitionReservationClimberResponse(existingCompetitionReservationClimber);
        }
        catch (Exception ex)
        {
            return new CompetitionReservationClimberResponse($"An error occurred when deleting the CompetitionReservationClimber: {ex.Message}");
        }
    }
}