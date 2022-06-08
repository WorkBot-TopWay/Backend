﻿using TopWay.API.TopWay.Domain.Models;
namespace TopWay.API.TopWay.Domain.Repositories;

public interface INewsRepository
{
    Task<IEnumerable<News>> ListAsync();
    Task<IEnumerable<News>> FindByClimbingGymIdAsync(int climbingGymId);
    Task<News> FindByIdAsync(int id);
    Task AddAsync(News news);
    void UpdateAsync(News news);
    void Delete(News news);
}