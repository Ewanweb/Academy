# آکادمی پردیس توس

پروژه فول‌استک شامل بک‌اند ASP.NET Core (.NET 8) و فرانت‌اند React (Vite) برای وب‌سایت آموزشگاه فنی‌حرفه‌ای پردیس توس.

## ساختار پوشه‌ها
- `backend/Academy.Api`: پروژه وب‌API با EF Core، احراز هویت JWT، Swagger و دیتابیس SQL Server.
- `frontend`: اپلیکیشن React با React Router، Axios و TailwindCSS (RTL).

## راه‌اندازی بک‌اند
1. نصب پیش‌نیازها: `.NET 8 SDK` و SQL Server در دسترس.
2. تنظیم اتصال دیتابیس در `backend/Academy.Api/appsettings.json` (رشته اتصال `DefaultConnection`).
3. اگر دامنه اختصاصی دارید مقدار `SiteSettings:BaseUrl` را برای خروجی نقشه سایت (SEO) تنظیم کنید.
3. اجرای دستورات:
   ```bash
   cd backend/Academy.Api
   dotnet restore
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   dotnet run
   ```
4. دسترسی‌ها:
   - Swagger: `http://localhost:5000/swagger`
   - کاربر ادمین پیش‌فرض: `admin@pardistous.ir` با رمز `Admin@12345`

## راه‌اندازی فرانت‌اند
1. پیش‌نیاز: Node.js 18+ و npm.
2. فایل `.env` را از `.env.example` بسازید و مقدار `VITE_API_URL` را روی آدرس API تنظیم کنید (پیش‌فرض `http://localhost:5000/api/v1`).
3. نصب و اجرا:
   ```bash
   cd frontend
   npm install
   npm run dev
   ```
4. اپ پیش‌فرض روی پورت `5173` در دسترس است.

## ویژگی‌های اصلی
- احراز هویت JWT و نقش‌های Admin/Student
- CRUD برای دوره‌ها، دسته‌بندی‌ها، مدرس‌ها، مقالات، اسلایدها، استوری‌های صفحه اصلی، نظرات و سوالات متداول
- مدیریت درخواست‌های ثبت‌نام (ادمین) و ثبت درخواست توسط کاربر
- رابط کاربری فارسی، راست‌به‌چپ و ریسپانسیو با فونت Vazirmatn + اسلایدر هرو و استوری شبیه اینستاگرام
- بهینه‌سازی متاتگ‌های سئو در صفحات اصلی فرانت‌اند
- ساختار کلین‌آرک (Repositories + Services) و خروجی `sitemap.xml` برای SEO
- ساختار پاسخ استاندارد `{ data, messageType, message, errors? }`

## تست سریع API با curl
نمونه ورود:
```bash
curl -X POST http://localhost:5000/api/v1/auth/login \\
  -H \"Content-Type: application/json\" \\
  -d '{\"email\":\"admin@pardistous.ir\",\"password\":\"Admin@12345\"}'
```
