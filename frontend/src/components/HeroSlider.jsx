import { useEffect, useMemo, useRef, useState } from 'react';
import { Link } from 'react-router-dom';

const HeroSlider = ({ slides = [] }) => {
  const safeSlides = useMemo(() => slides.filter((s) => s.isActive !== false), [slides]);
  const [active, setActive] = useState(0);
  const touchStart = useRef(0);

  useEffect(() => {
    if (!safeSlides.length) return undefined;
    const id = setInterval(() => setActive((prev) => (prev + 1) % safeSlides.length), 6000);
    return () => clearInterval(id);
  }, [safeSlides.length]);

  if (!safeSlides.length) return null;
  const current = safeSlides[active];

  const handleTouchStart = (e) => {
    touchStart.current = e.touches[0].clientX;
  };
  const handleTouchEnd = (e) => {
    const diff = e.changedTouches[0].clientX - touchStart.current;
    if (diff > 60) setActive((prev) => (prev - 1 + safeSlides.length) % safeSlides.length);
    if (diff < -60) setActive((prev) => (prev + 1) % safeSlides.length);
  };

  return (
    <div className="relative overflow-hidden rounded-3xl border border-white/10 bg-slate-900 shadow-2xl shadow-slate-900/50">
      <div className="h-80 md:h-[420px]" onTouchStart={handleTouchStart} onTouchEnd={handleTouchEnd}>
        <img
          src={current.imageUrl || 'https://placehold.co/1200x500'}
          alt={current.title}
          className="h-full w-full object-cover"
          loading="lazy"
        />
        <div className="absolute inset-0 bg-gradient-to-r from-slate-950/80 via-slate-900/50 to-transparent" />
        <div className="absolute inset-0 p-6 md:p-10 flex flex-col gap-4 justify-center text-white">
          <div className="px-3 py-1 rounded-full bg-white/10 w-fit text-sm text-secondary border border-white/10 backdrop-blur">
            آموزش تخصصی
          </div>
          <h2 className="text-3xl md:text-4xl font-extrabold leading-tight drop-shadow">{current.title}</h2>
          <p className="max-w-2xl text-sm md:text-base text-slate-200 leading-7">{current.subtitle}</p>
          {current.buttonLink && (
            <Link
              to={current.buttonLink}
              className="w-fit px-6 py-3 rounded-full bg-secondary text-slate-900 font-semibold shadow-lg shadow-secondary/30 hover:shadow-secondary/50 transition-transform hover:-translate-y-1"
            >
              {current.buttonText || 'بیشتر بدانید'}
            </Link>
          )}
        </div>
      </div>
      <div className="absolute bottom-4 right-4 flex gap-2">
        {safeSlides.map((_, idx) => (
          <button
            key={idx}
            onClick={() => setActive(idx)}
            className={`h-2.5 rounded-full transition-all ${idx === active ? 'w-8 bg-secondary' : 'w-2 bg-white/40'}`}
            aria-label={`slide-${idx}`}
          />
        ))}
      </div>
    </div>
  );
};

export default HeroSlider;
