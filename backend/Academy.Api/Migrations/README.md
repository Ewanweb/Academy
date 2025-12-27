برای ایجاد مایگریشن و به‌روزرسانی دیتابیس SQL Server:

```bash
cd backend/Academy.Api
dotnet ef migrations add InitialCreate
dotnet ef database update
```

فایل‌های مایگریشن پس از اجرای دستورات بالا داخل همین پوشه ساخته می‌شوند.
