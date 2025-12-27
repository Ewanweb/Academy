import { useEffect, useState } from 'react';
import CourseCard from '../components/CourseCard';
import api from '../services/api';
import Seo from '../components/Seo';

const Courses = () => {
  const [courses, setCourses] = useState([]);
  const [categories, setCategories] = useState([]);
  const [filters, setFilters] = useState({ q: '', categoryId: '', level: '', mode: '' });

  const load = async () => {
    const params = { ...filters };
    if (!params.categoryId) delete params.categoryId;
    const { data } = await api.get('/courses', { params });
    setCourses(data.data.items || data.data);
  };

  useEffect(() => {
    api.get('/courses/categories').then((res) => setCategories(res.data.data));
    load();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  const handleChange = (e) => setFilters({ ...filters, [e.target.name]: e.target.value });

  const applyFilters = () => load();

  return (
    <div className="container-wide py-10 space-y-6">
      <Seo title="دوره‌ها" description="لیست دوره‌های آکادمی پردیس توس با فیلتر سطح، نوع برگزاری و دسته‌بندی." />
      <div className="glass rounded-2xl p-4 mb-2 shadow-lg grid gap-4 md:grid-cols-4">
        <input name="q" value={filters.q} onChange={handleChange} placeholder="جستجو" className="input" />
        <select name="categoryId" value={filters.categoryId} onChange={handleChange} className="input">
          <option value="">دسته‌بندی</option>
          {categories.map((c) => <option key={c.id} value={c.id}>{c.name}</option>)}
        </select>
        <select name="level" value={filters.level} onChange={handleChange} className="input">
          <option value="">سطح</option>
          <option value="مقدماتی">مقدماتی</option>
          <option value="متوسط">متوسط</option>
          <option value="پیشرفته">پیشرفته</option>
        </select>
        <select name="mode" value={filters.mode} onChange={handleChange} className="input">
          <option value="">حضوری/آنلاین</option>
          <option value="حضوری">حضوری</option>
          <option value="آنلاین">آنلاین</option>
          <option value="هیبرید">هیبرید</option>
        </select>
        <button onClick={applyFilters} className="md:col-span-4 bg-secondary text-slate-900 rounded-lg py-2 font-semibold">اعمال فیلتر</button>
      </div>

      <div className="grid gap-6 md:grid-cols-2 lg:grid-cols-3">
        {courses.map((course) => (
          <div key={course.id} className="group transition-transform hover:-translate-y-1">
            <CourseCard course={course} />
          </div>
        ))}
      </div>
    </div>
  );
};

export default Courses;
