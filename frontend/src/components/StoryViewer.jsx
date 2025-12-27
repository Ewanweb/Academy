import { useEffect, useMemo, useRef, useState } from 'react';

const StoryViewer = ({ stories = [], activeId, onClose }) => {
  const [currentId, setCurrentId] = useState(activeId);
  const [progress, setProgress] = useState(0);
  const timerRef = useRef(null);
  const story = useMemo(() => stories.find((s) => s.id === currentId), [stories, currentId]);

  useEffect(() => {
    setCurrentId(activeId);
    setProgress(0);
  }, [activeId]);

  useEffect(() => {
    if (!story) return;
    timerRef.current = setInterval(() => {
      setProgress((p) => {
        if (p >= 100) {
          goNext();
          return 0;
        }
        return p + 2;
      });
    }, 100);
    return () => clearInterval(timerRef.current);
  }, [story]);

  const goNext = () => {
    const idx = stories.findIndex((s) => s.id === currentId);
    if (idx === -1) return;
    if (idx < stories.length - 1) setCurrentId(stories[idx + 1].id);
    else onClose();
    setProgress(0);
  };

  const goPrev = () => {
    const idx = stories.findIndex((s) => s.id === currentId);
    if (idx > 0) setCurrentId(stories[idx - 1].id);
    setProgress(0);
  };

  if (!story) return null;

  return (
    <div className="fixed inset-0 z-50 bg-black/80 backdrop-blur flex items-center justify-center">
      <div className="w-full max-w-xl mx-auto px-4">
        <div className="flex items-center gap-2 mb-3">
          {stories.map((s) => (
            <div key={s.id} className="flex-1 h-1 bg-white/20 rounded">
              <div
                className={`h-full rounded ${s.id === currentId ? 'bg-secondary' : 'bg-white/40'}`}
                style={{ width: s.id === currentId ? `${progress}%` : s.id === currentId ? '100%' : '0%' }}
              />
            </div>
          ))}
        </div>
        <div className="relative rounded-2xl overflow-hidden border border-white/10 bg-slate-900 shadow-xl shadow-slate-900/60">
          <img src={story.imageUrl || 'https://placehold.co/800x600'} alt={story.title} className="w-full h-96 object-cover" />
          <div className="absolute inset-0 flex items-center justify-between px-3">
            <button onClick={goPrev} className="h-full w-1/4" aria-label="prev" />
            <button onClick={goNext} className="h-full w-1/4" aria-label="next" />
          </div>
          <div className="absolute bottom-0 left-0 right-0 p-4 bg-gradient-to-t from-black/60 to-transparent text-white">
            <div className="font-semibold">{story.title}</div>
          </div>
        </div>
        <div className="mt-3 flex justify-between text-sm text-slate-200">
          <button onClick={onClose} className="px-3 py-1 rounded-lg border border-slate-700 hover:border-secondary">
            بستن
          </button>
          <div className="flex gap-2">
            <button onClick={goPrev} className="px-3 py-1 rounded-lg bg-white/10 hover:bg-white/20">
              قبلی
            </button>
            <button onClick={goNext} className="px-3 py-1 rounded-lg bg-secondary text-slate-900 font-semibold">
              بعدی
            </button>
          </div>
        </div>
      </div>
    </div>
  );
};

export default StoryViewer;
