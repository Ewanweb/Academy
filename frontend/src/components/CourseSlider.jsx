import { useMemo, useRef, useState } from 'react';
import CourseCard from './CourseCard';

const CourseSlider = ({ title, courses = [] }) => {
  const containerRef = useRef(null);
  const [scrollX, setScrollX] = useState(0);
  const maxScroll = useMemo(() => {
    const el = containerRef.current;
    if (!el) return 0;
    return el.scrollWidth - el.clientWidth;
  }, [courses.length]);

  const scrollBy = (dir) => {
    const el = containerRef.current;
    if (!el) return;
    const amount = dir === 'left' ? -320 : 320;
    el.scrollBy({ left: amount, behavior: 'smooth' });
    setScrollX(el.scrollLeft + amount);
  };

  if (!courses.length) return null;

  return (
    <section className="space-y-3">
      <div className="flex items-center justify-between">
        <h3 className="section-title text-lg md:text-xl">{title}</h3>
        <div className="flex gap-2">
          <button onClick={() => scrollBy('left')} className="px-3 py-1 rounded-full bg-white/10 hover:bg-white/20 text-sm">
            ◀
          </button>
          <button onClick={() => scrollBy('right')} className="px-3 py-1 rounded-full bg-secondary text-slate-900 text-sm font-semibold">
            ▶
          </button>
        </div>
      </div>
      <div ref={containerRef} className="flex gap-4 overflow-x-auto pb-2 snap-x snap-mandatory">
        {courses.map((course) => (
          <div key={course.id} className="min-w-[280px] snap-start">
            <CourseCard course={course} />
          </div>
        ))}
      </div>
      <div className="h-1 bg-slate-800 rounded">
        <div
          className="h-full bg-secondary rounded transition-all"
          style={{ width: maxScroll ? `${Math.min(scrollX / maxScroll, 1) * 100}%` : '0%' }}
        />
      </div>
    </section>
  );
};

export default CourseSlider;
