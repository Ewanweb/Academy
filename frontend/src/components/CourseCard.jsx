import { Link } from 'react-router-dom';

const CourseCard = ({ course }) => (
  <div className="rounded-xl bg-slate-900 border border-slate-700 shadow-lg shadow-slate-900/40 overflow-hidden flex flex-col">
    <div className="p-4 flex-1">
      <div className="text-xs text-secondary mb-2">{course.category?.name}</div>
      <h3 className="text-lg font-semibold mb-2 text-white">{course.title}</h3>
      <p className="text-sm text-slate-300">{course.description}</p>
    </div>
    <div className="px-4 pb-4 flex items-center justify-between text-sm text-slate-300">
      <span>{course.mode} • {course.level}</span>
      <span className="font-bold text-secondary">{course.price?.toLocaleString()} تومان</span>
    </div>
    <div className="px-4 pb-4">
      <Link to={`/courses/${course.slug}`} className="block w-full text-center bg-secondary text-slate-900 rounded-lg py-2 font-semibold">
        مشاهده جزئیات
      </Link>
    </div>
  </div>
);

export default CourseCard;
