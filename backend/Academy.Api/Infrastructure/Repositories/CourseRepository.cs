using Academy.Api.Application.Contracts.Repositories;
using Academy.Api.Data;
using Academy.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Academy.Api.Infrastructure.Repositories;

public class CourseRepository : ICourseRepository
{
    private readonly AcademyDbContext _db;

    public CourseRepository(AcademyDbContext db)
    {
        _db = db;
    }

    public async Task<(IEnumerable<Course> Items, int Total)> SearchAsync(string? q, Guid? categoryId, string? level, string? mode, decimal? minPrice, decimal? maxPrice, int page, int pageSize)
    {
        var query = _db.Courses.Include(c => c.Category).Include(c => c.Instructor).AsQueryable();
        if (!string.IsNullOrWhiteSpace(q))
            query = query.Where(c => c.Title.Contains(q) || c.Description.Contains(q));
        if (categoryId.HasValue)
            query = query.Where(c => c.CategoryId == categoryId);
        if (!string.IsNullOrWhiteSpace(level))
            query = query.Where(c => c.Level == level);
        if (!string.IsNullOrWhiteSpace(mode))
            query = query.Where(c => c.Mode == mode);
        if (minPrice.HasValue)
            query = query.Where(c => c.Price >= minPrice.Value);
        if (maxPrice.HasValue)
            query = query.Where(c => c.Price <= maxPrice.Value);

        var total = await query.CountAsync();
        var items = await query.OrderByDescending(c => c.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return (items, total);
    }

    public async Task<Course?> GetBySlugAsync(string slug) =>
        await _db.Courses.Include(c => c.Category).Include(c => c.Instructor).FirstOrDefaultAsync(c => c.Slug == slug);

    public async Task<IEnumerable<CourseCategory>> GetCategoriesAsync() =>
        await _db.CourseCategories.Where(c => c.IsActive).OrderBy(c => c.Name).ToListAsync();

    public async Task<IEnumerable<Course>> GetAllAdminAsync() =>
        await _db.Courses.Include(c => c.Category).Include(c => c.Instructor).ToListAsync();

    public Task<Course?> FindByIdAsync(Guid id) => _db.Courses.Include(c => c.Category).Include(c => c.Instructor).FirstOrDefaultAsync(c => c.Id == id);

    public async Task<Course> CreateAsync(Course course)
    {
        _db.Courses.Add(course);
        await _db.SaveChangesAsync();
        return course;
    }

    public async Task UpdateAsync(Course course)
    {
        _db.Courses.Update(course);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(Course course)
    {
        _db.Courses.Remove(course);
        await _db.SaveChangesAsync();
    }
}
