using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Repositories;
using TopWay.API.TopWay.Domain.Services;
using TopWay.API.TopWay.Domain.Services.Communication;

namespace TopWay.API.TopWay.Services;

public class RequestService: IRequestService
{
    private readonly IRequestRepository _requestRepository;
    private readonly IScalerRepository _scalerRepository;
    private readonly ILeagueRepository _leagueRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RequestService(IRequestRepository requestRepository, IScalerRepository scalerRepository, ILeagueRepository leagueRepository, IUnitOfWork unitOfWork)
    {
        _requestRepository = requestRepository;
        _scalerRepository = scalerRepository;
        _leagueRepository = leagueRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Request>> GetAll()
    {
        return await _requestRepository.GetAll();
    }

    public async Task<IEnumerable<Request>> FindByScalerId(int scalerId)
    {
        return await _requestRepository.FindByScalerId(scalerId);
    }

    public async Task<Request> FindLeagueIdAndScapeId(int leagueId, int scalerId)
    {
        return await _requestRepository.FindLeagueIdAndScapeId(leagueId, scalerId);
    }

    public async Task<IEnumerable<Scaler>> FindRequestScalerByLeagueId(int leagueId)
    {
        return await _requestRepository.FindRequestScalerByLeagueId(leagueId);
    }

    public async Task<Request> FindByIdAsync(int id)
    {
        return await _requestRepository.FindByIdAsync(id);
    }

    public async Task<RequestResponse> AddAsync(Request request, int leagueId, int scalerId)
    {
        var league = await _leagueRepository.GetById(leagueId);
        var scaler = await _scalerRepository.FindByIdAsync(scalerId);
        var existingRequest = await _requestRepository.FindLeagueIdAndScapeId(leagueId, scalerId);
        if (existingRequest != null)
        {
            return new RequestResponse("Request already exists");
        }

        if (league == null)
            return new RequestResponse("League not found.");

        if (scaler == null)
            return new RequestResponse("Scaler not found.");

        request.Scaler = scaler;
        request.League = league;
        request.LeagueId = leagueId;
        request.ScalerId = scalerId;
        request.Status = "Pending";
        try
        {
            await _requestRepository.AddAsync(request);
            await _unitOfWork.CompleteAsync();
            return new RequestResponse(request);
        }
        catch (Exception ex)
        {
            return new RequestResponse($"An error occurred when saving the request: {ex.Message}");
        }
    }

    public async Task<RequestResponse> Delete(int leagueId, int scalerId)
    {
        var existingRequest = await _requestRepository.FindLeagueIdAndScapeId(leagueId, scalerId);
        if (existingRequest == null)
        {
            return new RequestResponse("Request not found.");
        }

        try
        {
            _requestRepository.Delete(existingRequest);
            await _unitOfWork.CompleteAsync();
            return new RequestResponse(existingRequest);
        }
        catch (Exception ex)
        {
            return new RequestResponse($"An error occurred when deleting the request: {ex.Message}");
        }
    }
}