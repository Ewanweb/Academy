import { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import api from '../services/api';
import { useAuth } from '../services/AuthContext';
import Seo from '../components/Seo';

const CourseDetail = () => {
  const { slug } = useParams();
  const navigate = useNavigate();
  const { user } = useAuth();
  const [course, setCourse] = useState(null);
  const [note, setNote] = useState('');
  const [message, setMessage] = useState('');

  useEffect(() => {
    api.get(`/courses/${slug}`).then((res) => setCourse(res.data.data));
  }, [slug]);

  const requestEnroll = async () => {
    if (!user) {
      navigate('/login');
      return;
    }
    const { data } = await api.post('/enrollments', { courseId: course.id, note });
    setMessage(data.message);
  };

  if (!course) return <div className="container-wide py-10">در حال بارگذاری...</div>;

  return (
    <div className="container-wide py-10 grid gap-6 md:grid-cols-3">
      <Seo title={course.title} description={course.description?.slice(0, 150)} />
      <div className="md:col-span-2 bg-white border border-gray-100 rounded-xl p-6 shadow-sm">
        <h1 className="text-2xl font-bold mb-2">{course.title}</h1>
        <p className="text-gray-700 leading-8 mb-4">{course.description}</p>
        <div className="flex flex-wrap gap-3 text-sm text-gray-600 mb-4">
          <span className="px-3 py-1 rounded-full bg-primary/10 text-primary">{course.level}</span>
          <span className="px-3 py-1 rounded-full bg-secondary/10 text-secondary">{course.mode}</span>
          <span className="px-3 py-1 rounded-full bg-gray-100">مدت دوره: {course.durationWeeks} هفته</span>
        </div>
        <h3 className="text-lg font-semibold mb-2">پیش‌نیازها</h3>
        <p className="text-gray-700 mb-4">{course.prerequisites}</p>
        <h3 className="text-lg font-semibold mb-2">سرفصل</h3>
        <p className="text-gray-700 whitespace-pre-line leading-7">{course.syllabus}</p>
      </div>
      <div className="bg-white border border-gray-100 rounded-xl p-6 shadow-sm h-fit">
        <div className="text-gray-700 text-sm mb-2">دسته‌بندی: {course.category?.name}</div>
        <div className="text-gray-700 text-sm mb-2">مدرس: {course.instructor?.fullName}</div>
        <div className="text-2xl font-bold text-primary mb-4">{course.price?.toLocaleString()} تومان</div>
        <textarea
          className="input h-24"
          placeholder="توضیحات ثبت‌نام (اختیاری)"
          value={note}
          onChange={(e) => setNote(e.target.value)}
        />
        <button onClick={requestEnroll} className="w-full bg-primary text-white rounded-lg py-2 mt-3">
          درخواست ثبت‌نام
        </button>
        {message && <div className="text-green-600 text-sm mt-3">{message}</div>}
      </div>
    </div>
  );
};

export default CourseDetail;
