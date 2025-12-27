using System.Text;
using Academy.Api.Application.Contracts.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Academy.Api.Controllers;

[ApiController]
[Route("sitemap.xml")]
public class SitemapController : ControllerBase
{
    private readonly ICourseRepository _courses;
    private readonly IContentRepository _content;
    private readonly IConfiguration _config;

    public SitemapController(ICourseRepository courses, IContentRepository content, IConfiguration config)
    {
        _courses = courses;
        _content = content;
        _config = config;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Get()
    {
        var baseUrl = _config.GetSection("SiteSettings")["BaseUrl"] ?? "http://localhost:5000";
        var courseItems = (await _courses.SearchAsync(null, null, null, null, null, null, 1, 1000)).Items;
        var blogItems = (await _content.GetBlogPostsAsync(1, 1000)).Items;

        var sb = new StringBuilder();
        sb.AppendLine(@"<?xml version=""1.0"" encoding=""UTF-8""?>");
        sb.AppendLine(@"<urlset xmlns=""http://www.sitemaps.org/schemas/sitemap/0.9"">");
        void AddUrl(string loc) => sb.AppendLine($@"  <url><loc>{baseUrl}{loc}</loc></url>");

        AddUrl("/");
        AddUrl("/courses");
        AddUrl("/blog");
        AddUrl("/faq");
        AddUrl("/contact");

        foreach (var c in courseItems)
            AddUrl($"/courses/{c.Slug}");
        foreach (var b in blogItems)
            AddUrl($"/blog/{b.Slug}");

        sb.AppendLine("</urlset>");
        return Content(sb.ToString(), "application/xml", Encoding.UTF8);
    }
}
