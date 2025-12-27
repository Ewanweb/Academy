using Academy.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Academy.Api.Data;

public class AcademyDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
{
    public AcademyDbContext(DbContextOptions<AcademyDbContext> options) : base(options)
    {
    }

    public DbSet<CourseCategory> CourseCategories => Set<CourseCategory>();
    public DbSet<Instructor> Instructors => Set<Instructor>();
    public DbSet<Course> Courses => Set<Course>();
    public DbSet<EnrollmentRequest> EnrollmentRequests => Set<EnrollmentRequest>();
    public DbSet<BlogPost> BlogPosts => Set<BlogPost>();
    public DbSet<HomeSlide> HomeSlides => Set<HomeSlide>();
    public DbSet<Testimonial> Testimonials => Set<Testimonial>();
    public DbSet<FAQ> FAQs => Set<FAQ>();
    public DbSet<StoryHighlight> StoryHighlights => Set<StoryHighlight>();
    public DbSet<AttendanceSession> AttendanceSessions => Set<AttendanceSession>();
    public DbSet<AttendanceRecord> AttendanceRecords => Set<AttendanceRecord>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Course>()
            .HasOne(c => c.Category)
            .WithMany(c => c.Courses)
            .HasForeignKey(c => c.CategoryId);

        builder.Entity<Course>()
            .HasOne(c => c.Instructor)
            .WithMany(i => i.Courses)
            .HasForeignKey(c => c.InstructorId);

        builder.Entity<EnrollmentRequest>()
            .HasOne(e => e.Course)
            .WithMany(c => c.EnrollmentRequests)
            .HasForeignKey(e => e.CourseId);

        builder.Entity<EnrollmentRequest>()
            .HasOne<AppUser>(e => e.User)
            .WithMany()
            .HasForeignKey(e => e.UserId);

        builder.Entity<AttendanceSession>()
            .HasOne(a => a.Course)
            .WithMany()
            .HasForeignKey(a => a.CourseId);

        builder.Entity<AttendanceRecord>()
            .HasOne(a => a.Session)
            .WithMany(s => s.Records!)
            .HasForeignKey(a => a.AttendanceSessionId);

        SeedData(builder);
    }

    private void SeedData(ModelBuilder builder)
    {
        var adminRoleId = Guid.Parse("9e0ab933-5eca-4636-99f7-3b23910e05cd");
        var studentRoleId = Guid.Parse("a1d5de2d-848b-4b2b-9d52-a83a1fd4d720");
        var adminId = Guid.Parse("a96986e7-933d-4fa9-9e1f-e305f428f4b2");

        var adminRole = new IdentityRole<Guid>
        {
            Id = adminRoleId,
            Name = "Admin",
            NormalizedName = "ADMIN"
        };

        var studentRole = new IdentityRole<Guid>
        {
            Id = studentRoleId,
            Name = "Student",
            NormalizedName = "STUDENT"
        };

        var adminUser = new AppUser
        {
            Id = adminId,
            UserName = "admin",
            NormalizedUserName = "ADMIN",
            Email = "admin@pardistous.ir",
            NormalizedEmail = "ADMIN@PARDISTOUS.IR",
            EmailConfirmed = true,
            PhoneNumber = "+989000000000",
            PhoneNumberConfirmed = true,
            FullName = "ادمین آموزشگاه",
            Role = "Admin",
            SecurityStamp = Guid.NewGuid().ToString("D"),
            ConcurrencyStamp = Guid.NewGuid().ToString("D")
        };

        var hasher = new PasswordHasher<AppUser>();
        adminUser.PasswordHash = hasher.HashPassword(adminUser, "Admin@12345");

        builder.Entity<IdentityRole<Guid>>().HasData(adminRole, studentRole);
        builder.Entity<AppUser>().HasData(adminUser);
        builder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
        {
            RoleId = adminRoleId,
            UserId = adminId
        });

        var webCategoryId = Guid.Parse("1f5af6d0-5a23-4a12-8cd1-1e64bf0bc924");
        var mobileCategoryId = Guid.Parse("35fa0b51-3060-4e87-8f29-5f9ecde624d7");

        builder.Entity<CourseCategory>().HasData(
            new CourseCategory
            {
                Id = webCategoryId,
                Name = "برنامه‌نویسی وب",
                Description = "یادگیری طراحی و پیاده‌سازی وب‌سایت‌های مدرن.",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsActive = true
            },
            new CourseCategory
            {
                Id = mobileCategoryId,
                Name = "موبایل",
                Description = "توسعه اپلیکیشن‌های موبایل اندروید و iOS.",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsActive = true
            }
        );

        var instructorWebId = Guid.Parse("9a02ec1e-6006-4a45-b6a5-6c2851c60c1a");
        var instructorMobileId = Guid.Parse("0c4da3c7-2587-46af-92a7-fb4785905a95");

        builder.Entity<Instructor>().HasData(
            new Instructor
            {
                Id = instructorWebId,
                FullName = "نرگس مرادی",
                Bio = "توسعه‌دهنده ارشد وب با بیش از ۸ سال تجربه در React و .NET.",
                AvatarUrl = "https://placehold.co/200x200",
                Expertise = "Full-stack",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsActive = true
            },
            new Instructor
            {
                Id = instructorMobileId,
                FullName = "امیرحسین بهرامی",
                Bio = "برنامه‌نویس موبایل با تخصص در Kotlin و Flutter.",
                AvatarUrl = "https://placehold.co/200x200",
                Expertise = "Mobile",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsActive = true
            }
        );

        var webCourseId = Guid.Parse("301ed221-8e74-495b-952c-9d7c5683a1b8");
        var apiCourseId = Guid.Parse("3d49e392-8de5-4e14-baaa-8aba7b81aad2");

        builder.Entity<Course>().HasData(
            new Course
            {
                Id = webCourseId,
                Title = "طراحی وب مدرن با React و Tailwind",
                Slug = "modern-react-tailwind",
                Description = "یادگیری ساخت وب‌سایت‌های مدرن و واکنش‌گرا.",
                Level = "متوسط",
                Mode = "حضوری",
                Price = 4500000,
                DurationWeeks = 10,
                Prerequisites = "آشنایی مقدماتی با HTML/CSS",
                Syllabus = "React Basics, Hooks, Routing, TailwindCSS, Deployment",
                CategoryId = webCategoryId,
                InstructorId = instructorWebId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsActive = true
            },
            new Course
            {
                Id = apiCourseId,
                Title = "توسعه API با ASP.NET Core و EF Core",
                Slug = "aspnetcore-api",
                Description = "پیاده‌سازی بک‌اند حرفه‌ای با امنیت JWT و EF Core.",
                Level = "پیشرفته",
                Mode = "آنلاین",
                Price = 5200000,
                DurationWeeks = 12,
                Prerequisites = "آشنایی با C# و اصول OOP",
                Syllabus = "Web API, EF Core, JWT, Clean Architecture, Deploy",
                CategoryId = webCategoryId,
                InstructorId = instructorWebId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsActive = true
            }
        );

        builder.Entity<BlogPost>().HasData(
            new BlogPost
            {
                Id = Guid.Parse("48cf0dbf-e744-4c5a-9aae-0e7f9974fb21"),
                Title = "چرا باید ASP.NET Core یاد بگیریم؟",
                Slug = "why-aspnetcore",
                Summary = "نگاهی به مزایای ASP.NET Core برای ساخت API های مدرن.",
                Content = "ASP.NET Core یک فریم‌ورک متن‌باز و سریع برای ساخت سرویس‌های امن است.",
                Author = "تیم محتوای آکادمی",
                PublishedAt = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsActive = true
            },
            new BlogPost
            {
                Id = Guid.Parse("8559f2fd-0898-45e2-810b-bda3e7c2fcc1"),
                Title = "راهنمای شروع React برای مبتدی‌ها",
                Slug = "react-for-beginners",
                Summary = "قدم‌های اولیه برای ورود به دنیای توسعه فرانت‌اند.",
                Content = "در این مقاله با مفاهیم JSX، کامپوننت‌ها و مدیریت حالت آشنا می‌شوید.",
                Author = "تیم محتوای آکادمی",
                PublishedAt = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsActive = true
            },
            new BlogPost
            {
                Id = Guid.Parse("913b7270-9d80-44da-8983-3ecb34f94c5f"),
                Title = "۵ نکته برای یادگیری سریع‌تر",
                Slug = "learning-fast",
                Summary = "تجربه‌های مدرسین برای یادگیری عمیق‌تر.",
                Content = "تمرین مستمر و انجام پروژه‌های عملی مهم‌ترین فاکتور پیشرفت است.",
                Author = "تیم محتوای آکادمی",
                PublishedAt = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsActive = true
            }
        );

        builder.Entity<HomeSlide>().HasData(
            new HomeSlide
            {
                Id = Guid.Parse("e5656987-0b48-4a62-a65e-f07a38f0fa1c"),
                Title = "آکادمی پردیس توس",
                Subtitle = "مسیر حرفه‌ای شدن در فناوری را اینجا شروع کن.",
                ImageUrl = "https://placehold.co/1200x500",
                ButtonText = "مشاهده دوره‌ها",
                ButtonLink = "/courses",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsActive = true
            }
        );

        builder.Entity<StoryHighlight>().HasData(
            new StoryHighlight
            {
                Id = Guid.Parse("9d0ce07f-e4b6-4a1f-8c2f-4e6fdc256e9e"),
                Title = "وبینار رایگان React",
                ImageUrl = "https://placehold.co/120x120",
                Link = "/blog/react-for-beginners",
                ExpiresAt = DateTime.UtcNow.AddMonths(1),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsActive = true
            },
            new StoryHighlight
            {
                Id = Guid.Parse("f4299a0c-8a07-42fb-9b4a-e5f92b8d2d6f"),
                Title = "تخفیف تابستانه",
                ImageUrl = "https://placehold.co/120x120",
                Link = "/courses",
                ExpiresAt = DateTime.UtcNow.AddMonths(1),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsActive = true
            },
            new StoryHighlight
            {
                Id = Guid.Parse("3bf71331-b17a-4cf4-8f80-3ca16a4cdb15"),
                Title = "پروژه‌های هنرجویان",
                ImageUrl = "https://placehold.co/120x120",
                Link = "/blog/why-aspnetcore",
                ExpiresAt = DateTime.UtcNow.AddMonths(1),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsActive = true
            }
        );

        builder.Entity<Testimonial>().HasData(
            new Testimonial
            {
                Id = Guid.Parse("2dd22bc2-2306-4bc4-83af-ff911138f97c"),
                StudentName = "سارا نادری",
                Message = "دوره‌ها خیلی کاربردی بودند و مربی‌ها پشتیبانی عالی داشتند.",
                Rating = 5,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsActive = true
            }
        );

        builder.Entity<FAQ>().HasData(
            new FAQ
            {
                Id = Guid.Parse("3e8a98d5-390a-4cfa-bf7c-2e9602a2cb78"),
                Question = "چطور می‌توانم ثبت‌نام کنم؟",
                Answer = "بعد از انتخاب دوره، فرم درخواست ثبت‌نام را تکمیل کنید تا با شما تماس بگیریم.",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsActive = true
            },
            new FAQ
            {
                Id = Guid.Parse("5b7d4a8f-fb11-4fc0-99ff-e0266044a648"),
                Question = "آیا دوره‌ها آنلاین هم برگزار می‌شوند؟",
                Answer = "بله، اکثر دوره‌ها به صورت آنلاین و حضوری ارائه می‌شوند.",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsActive = true
            }
        );
    }
}
