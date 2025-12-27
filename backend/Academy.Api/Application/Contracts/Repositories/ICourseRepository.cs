using Academy.Api.Models;

namespace Academy.Api.Application.Contracts.Repositories;

public interface ICourseRepository
{
    Task<(IEnumerable<Course> Items, int Total)> SearchAsync(string? q, Guid? categoryId, string? level, string? mode, decimal? minPrice, decimal? maxPrice, int page, int pageSize);
    Task<Course?> GetBySlugAsync(string slug);
    Task<IEnumerable<CourseCategory>> GetCategoriesAsync();
    Task<IEnumerable<Course>> GetAllAdminAsync();
    Task<Course?> FindByIdAsync(Guid id);
    Task<Course> CreateAsync(Course course);
    Task UpdateAsync(Course course);
    Task DeleteAsync(Course course);
}
