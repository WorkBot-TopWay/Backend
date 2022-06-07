using Microsoft.EntityFrameworkCore;
using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Repositories;
using TopWay.API.TopWay.Persistence.Contexts;

namespace TopWay.API.TopWay.Persistence.Repositories;

public class CommentRepository: BaseRepository, ICommentRepository
{
    public CommentRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Comments>> ListAsync()
    {
        return await _context.Comments.ToListAsync();
    }

    public async Task<IEnumerable<Comments>> FindByClimbingGymIdAsync(int climbingGymId)
    {
        return await _context.Comments
            .Where(c => c.ClimbingGymId == climbingGymId)
            .ToListAsync();
    }

    public async Task<Comments> FindByClimbingGymIdAndScalerIdAsync(int climbingGymId, int scalerId)
    {
        return (await _context.Comments
            .FirstOrDefaultAsync(c => c.ClimbingGymId == climbingGymId && c.ScalerId == scalerId))!;
    }

    public async Task<Comments> FindByIdAsync(int id)
    {
        return (await _context.Comments.FindAsync(id))!;
    }

    public async Task AddAsync(Comments comments)
    {
        await _context.Comments.AddAsync(comments);
    }

    public void UpdateAsync(Comments comments)
    {
        _context.Comments.Update(comments);
    }


    public void Delete(Comments comments)
    {
        _context.Comments.Remove(comments);
    }
}