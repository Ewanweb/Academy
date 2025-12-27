import { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import api from '../services/api';
import CourseCard from '../components/CourseCard';
import HeroSlider from '../components/HeroSlider';
import StoryBar from '../components/StoryBar';
import Seo from '../components/Seo';
import CourseSlider from '../components/CourseSlider';

const Home = () => {
  const [data, setData] = useState({ slides: [], testimonials: [], faqs: [], topCourses: [], latestPosts: [], stories: [] });
  const [categories, setCategories] = useState([]);
  const [coursesByCategory, setCoursesByCategory] = useState({});

  useEffect(() => {
    api.get('/public/home').then((res) => {
      const payload = res.data.data;
      setData(payload);
      setCategories(payload.categories || []);
    });
    api.get('/courses/categories').then((res) => setCategories((prev) => prev.length ? prev : res.data.data || []));
    api.get('/courses', { params: { pageSize: 50 } }).then((res) => {
      const items = res.data.data.items || res.data.data || [];
      const grouped = items.reduce((acc, course) => {
        const key = course.category?.name || 'سایر';
        acc[key] = acc[key] ? [...acc[key], course] : [course];
        return acc;
      }, {});
      setCoursesByCategory(grouped);
    });
  }, []);

  return (
    <div>
      <Seo title="صفحه اصلی" description="آکادمی پردیس توس؛ دوره‌های تخصصی وب، موبایل و بک‌اند با پشتیبانی مربیان صنعت." />
      <section className="bg-gradient-to-l from-slate-900/60 to-slate-800/40 border-b border-slate-800">
        <div className="container-wide grid md:grid-cols-2 gap-10 py-14 items-center">
          <div>
            <span className="text-secondary text-sm font-semibold">آموزش‌های پروژه‌محور</span>
            <h1 className="text-3xl md:text-4xl font-bold leading-relaxed mb-4 text-white">
              آکادمی پردیس توس؛ <br /> مسیر یادگیری فناوری به زبان ساده
            </h1>
            <p className="text-slate-200 mb-6">
              دوره‌های تخصصی برنامه‌نویسی وب، موبایل و بک‌اند با تمرکز بر استخدام و کار عملی.
            </p>
            <div className="flex flex-wrap gap-3">
              <Link to="/courses" className="px-5 py-3 rounded-lg bg-secondary text-slate-900 font-semibold shadow">
                مشاهده دوره‌ها
              </Link>
              <Link to="/contact" className="px-5 py-3 rounded-lg border border-secondary text-secondary">
                مشاوره رایگان
              </Link>
            </div>
          </div>
          <div className="rounded-2xl bg-slate-900 border border-slate-800 shadow-lg shadow-slate-900/40 p-6">
            <h3 className="text-lg font-semibold mb-3 text-white">چرا آکادمی ما؟</h3>
            <ul className="space-y-2 text-slate-200 text-sm leading-7">
              <li>• مربیان باتجربه صنعت</li>
              <li>• پروژه‌های واقعی در طول دوره</li>
              <li>• پشتیبانی و منتورینگ شخصی</li>
              <li>• تضمین ارائه رزومه و آماده‌سازی مصاحبه</li>
            </ul>
          </div>
        </div>
      </section>

      <section className="container-wide py-6">
        <HeroSlider slides={data.slides} />
      </section>

      <section className="container-wide py-6">
        <StoryBar stories={data.stories} />
      </section>

      <section className="container-wide py-12 space-y-8">
        <div className="flex items-center justify-between">
          <h2 className="section-title">دوره‌های محبوب</h2>
          <Link to="/courses" className="text-secondary">مشاهده همه</Link>
        </div>
        <div className="grid gap-6 md:grid-cols-2 lg:grid-cols-3">
          {data.topCourses.map((course) => <CourseCard key={course.id} course={course} />)}
        </div>
        <div className="space-y-6">
          {Object.entries(coursesByCategory).map(([name, list]) => (
            <CourseSlider key={name} title={`دوره‌های ${name}`} courses={list} />
          ))}
        </div>
      </section>

      <section className="bg-slate-900/60 py-12 border-y border-slate-800">
        <div className="container-wide">
          <h2 className="text-2xl font-bold mb-6 text-white">نظرات هنرجویان</h2>
          <div className="grid gap-4 md:grid-cols-2">
            {data.testimonials.map((t) => (
              <div key={t.id} className="rounded-xl border border-slate-800 bg-slate-900 p-4 shadow-lg shadow-slate-900/40">
                <div className="flex items-center justify-between mb-2">
                  <span className="font-semibold text-white">{t.studentName}</span>
                  <span className="text-secondary">{'★'.repeat(t.rating)}</span>
                </div>
                <p className="text-slate-200 text-sm leading-7">{t.message}</p>
              </div>
            ))}
          </div>
        </div>
      </section>

      <section className="container-wide py-12 space-y-8">
        <div>
          <div className="flex items-center justify-between mb-4">
            <h2 className="text-2xl font-bold">آخرین مقالات</h2>
            <Link to="/blog" className="text-primary">همه مقالات</Link>
          </div>
          <div className="grid gap-4 md:grid-cols-3">
            {data.latestPosts.map((post) => (
              <Link key={post.id} to={`/blog/${post.slug}`} className="rounded-xl bg-white border border-gray-100 p-4 shadow-sm block">
                <div className="text-xs text-gray-500 mb-2">{new Date(post.publishedAt).toLocaleDateString('fa-IR')}</div>
                <h3 className="font-semibold mb-2">{post.title}</h3>
                <p className="text-gray-600 text-sm leading-7">{post.summary}</p>
              </Link>
            ))}
          </div>
        </div>

        <div>
          <div className="flex items-center justify-between mb-4">
            <h2 className="text-2xl font-bold">سوالات متداول</h2>
            <Link to="/faq" className="text-primary">مشاهده همه</Link>
          </div>
          <div className="grid gap-4 md:grid-cols-2">
            {data.faqs.slice(0, 4).map((faq) => (
              <div key={faq.id} className="rounded-xl bg-white border border-gray-100 p-4 shadow-sm">
                <h3 className="font-semibold mb-2">{faq.question}</h3>
                <p className="text-gray-600 text-sm leading-7">{faq.answer}</p>
              </div>
            ))}
          </div>
        </div>
      </section>
    </div>
  );
};

export default Home;
