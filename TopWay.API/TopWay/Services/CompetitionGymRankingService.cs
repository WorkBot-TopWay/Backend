﻿using System.Runtime.CompilerServices;
using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Repositories;
using TopWay.API.TopWay.Domain.Services;
using TopWay.API.TopWay.Domain.Services.Communication;
using Ubiety.Dns.Core;

namespace TopWay.API.TopWay.Services;

public class CompetitionGymRankingService: ICompetitionGymRankingService
{
    private readonly ICompetitionGymRankingRepository _competitionGymRankingRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IScalerRepository _scalerRepository;
    private readonly ICompetitionGymRepository _competitionGymRepository;

    public CompetitionGymRankingService(ICompetitionGymRankingRepository competitionGymRankingRepository
        , IUnitOfWork unitOfWork, IScalerRepository scalerRepository, ICompetitionGymRepository competitionGymRepository)
    {
        _competitionGymRankingRepository = competitionGymRankingRepository;
        _unitOfWork = unitOfWork;
        _scalerRepository = scalerRepository;
        _competitionGymRepository = competitionGymRepository;
    }

    public async Task<IEnumerable<CompetitionGymRanking>> ListAsync()
    {
        return await _competitionGymRankingRepository.ListAsync();
    }

    public async Task<IEnumerable<Scaler>> FindScalerByCompetitionIdAsync(int competitionId)
    {
        return await _competitionGymRankingRepository.FindScalerByCompetitionIdAsync(competitionId);
    }

    public async Task<CompetitionGymRanking> FindByCompetitionIdAndScalerIdAsync(int competitionId, int scalerId)
    {
        return await _competitionGymRankingRepository.FindByCompetitionIdAndScalerIdAsync(competitionId, scalerId);
    }

    public async Task<CompetitionGymRanking> FindByIdAsync(int id)
    {
        return await _competitionGymRankingRepository.FindByIdAsync(id);
    }

    public async Task<CompetitionGymRankingResponse> AddAsync(CompetitionGymRanking competitionGymRanking, int competitionId, int scalerId)
    {
        var existingCompetitionGymRanking = await _competitionGymRankingRepository.FindByCompetitionIdAndScalerIdAsync(competitionId, scalerId);
        var existingScaler = await _scalerRepository.FindByIdAsync(scalerId);
        var existingCompetitionGym = await _competitionGymRepository.FindByIdAsync(competitionId);
        if (existingCompetitionGymRanking != null)
        {
            return new CompetitionGymRankingResponse("CompetitionGymRanking already exists");
        }
        if (existingScaler == null)
        {
            return new CompetitionGymRankingResponse("Scaler does not exist");
        }
        if (existingCompetitionGym == null)
        {
            return new CompetitionGymRankingResponse("Competition does not exist");
        }
        competitionGymRanking.ScalerId = scalerId;
        competitionGymRanking.CompetitionGymId = competitionId;
        competitionGymRanking.Scaler = existingScaler;
        competitionGymRanking.CompetitionGym = existingCompetitionGym;
        try
        {
            await _competitionGymRankingRepository.AddAsync(competitionGymRanking);
            await _unitOfWork.CompleteAsync();
            return new CompetitionGymRankingResponse(competitionGymRanking);
        }
        catch (Exception ex)
        {
            return new CompetitionGymRankingResponse($"An error occurred when saving the competitionGymRanking: {ex.Message}");
        }

    }

    public async Task<CompetitionGymRankingResponse> Delete(int competitionId, int scalerId)
    {
        var existingCompetitionGymRanking = await _competitionGymRankingRepository.FindByCompetitionIdAndScalerIdAsync(competitionId, scalerId);
        if (existingCompetitionGymRanking == null)
        {
            return new CompetitionGymRankingResponse("CompetitionGymRanking not found");
        }
        try
        {
            _competitionGymRankingRepository.Delete(existingCompetitionGymRanking);
            await _unitOfWork.CompleteAsync();
            return new CompetitionGymRankingResponse(existingCompetitionGymRanking);
        }
        catch (Exception ex)
        {
            return new CompetitionGymRankingResponse($"An error occurred when deleting the competitionGymRanking: {ex.Message}");
        }
    }
}