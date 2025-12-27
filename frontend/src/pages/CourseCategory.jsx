import { useEffect, useState } from 'react';
import { useParams, Link } from 'react-router-dom';
import { api } from '../services/api';
import CourseCard from '../components/CourseCard';
import Seo from '../components/Seo';

const CourseCategory = () => {
  const { id } = useParams();
  const [courses, setCourses] = useState([]);
  const [category, setCategory] = useState(null);

  useEffect(() => {
    api.get('/courses/categories').then((res) => {
      const cat = res.data.data.find((c) => c.id === id);
      setCategory(cat);
    });
    api.get('/courses', { params: { categoryId: id, pageSize: 50 } }).then((res) => {
      setCourses(res.data.data.items || res.data.data || []);
    });
  }, [id]);

  return (
    <div className="container-wide py-10 space-y-6">
      <Seo title={`دوره‌های ${category?.name || ''}`} description={`لیست دوره‌ها در دسته ${category?.name || ''}`} />
      <div className="flex items-center justify-between">
        <div>
          <h1 className="section-title">{category?.name || 'دسته‌بندی دوره‌ها'}</h1>
          <p className="text-slate-300 mt-2">{category?.description}</p>
        </div>
        <Link to="/courses" className="text-secondary">بازگشت به همه دوره‌ها</Link>
      </div>
      <div className="grid gap-6 md:grid-cols-2 lg:grid-cols-3">
        {courses.map((course) => <CourseCard key={course.id} course={course} />)}
        {!courses.length && <div className="text-slate-400">دوره‌ای در این دسته یافت نشد.</div>}
      </div>
    </div>
  );
};

export default CourseCategory;
