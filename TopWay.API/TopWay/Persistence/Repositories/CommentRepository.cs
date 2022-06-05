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

    public async Task<IEnumerable<Comment>> ListAsync()
    {
        return await _context.Comments.ToListAsync();
    }

    public async Task<IEnumerable<Comment>> FindByClimbingGymIdAsync(int climbingGymId)
    {
        return await _context.Comments
            .Where(c => c.ClimbingGymId == climbingGymId)
            .ToListAsync();
    }

    public async Task<Comment> FindByClimbingGymIdAndScalerIdAsync(int climbingGymId, int scalerId)
    {
        return (await _context.Comments
            .FirstOrDefaultAsync(c => c.ClimbingGymId == climbingGymId && c.ScalerId == scalerId))!;
    }

    public async Task<Comment> FindByIdAsync(int id)
    {
        return (await _context.Comments.FindAsync(id))!;
    }

    public async Task AddAsync(Comment comment)
    {
        await _context.Comments.AddAsync(comment);
    }

    public void UpdateAsync(Comment comment)
    {
        _context.Comments.Update(comment);
    }


    public void Delete(Comment comment)
    {
        _context.Comments.Remove(comment);
    }
}