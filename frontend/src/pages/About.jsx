import Seo from '../components/Seo';

const About = () => (
  <div className="container-wide py-10 grid gap-6 md:grid-cols-2">
    <Seo title="درباره ما" description="با آکادمی پردیس توس، مسیر یادگیری و استخدام در حوزه فناوری را بشناسید." />
    <div>
      <h1 className="text-3xl font-bold mb-4">درباره آکادمی پردیس توس</h1>
      <p className="text-gray-700 leading-8">
        آکادمی پردیس توس با تمرکز بر آموزش‌های کاربردی و پروژه‌محور، به شما کمک می‌کند تا به یک متخصص آماده کار در حوزه
        فناوری تبدیل شوید. تیم ما متشکل از مربیان باتجربه صنعت است که مسیر یادگیری را کوتاه‌تر و جذاب‌تر می‌کنند.
      </p>
      <p className="text-gray-700 leading-8 mt-4">
        ما دوره‌های وب، موبایل و بک‌اند را با استانداردهای بازار کار و همراه با منتورینگ ارائه می‌کنیم. هدف ما ایجاد
        محیطی دوستانه، مدرن و کارآمد برای رشد مهارت‌های شماست.
      </p>
    </div>
    <div className="bg-white border border-gray-100 rounded-2xl shadow-sm p-6">
      <h3 className="text-xl font-semibold mb-3">ویژگی‌های کلیدی</h3>
      <ul className="space-y-3 text-gray-700">
        <li>• برنامه درسی به‌روز و هماهنگ با نیاز بازار</li>
        <li>• دسترسی به منابع و ضبط جلسات</li>
        <li>• فرصت شبکه‌سازی با متخصصان و هم‌کلاسی‌ها</li>
        <li>• کارگاه‌های رزومه‌نویسی و مصاحبه شغلی</li>
      </ul>
    </div>
  </div>
);

export default About;
