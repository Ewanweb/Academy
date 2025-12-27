import { useState } from 'react';
import { Link } from 'react-router-dom';
import StoryViewer from './StoryViewer';

const StoryBar = ({ stories = [] }) => {
  const [active, setActive] = useState(null);
  if (!stories.length) return null;

  return (
    <div className="glass rounded-2xl p-4 text-white">
      <div className="flex items-center justify-between mb-3">
        <h3 className="font-semibold text-lg">استوری‌ها</h3>
        {active && (
          <button onClick={() => setActive(null)} className="text-sm text-secondary">
            بستن
          </button>
        )}
      </div>
      <div className="flex gap-4 overflow-x-auto pb-2">
        {stories.map((story) => (
          <button
            key={story.id}
            onClick={() => setActive(story.id)}
            className="flex-shrink-0 flex flex-col items-center gap-2 group"
            aria-label={story.title}
          >
            <span className="p-1 rounded-full bg-gradient-to-tr from-secondary via-accent to-secondary group-hover:scale-105 transition-transform">
              <img
                src={story.imageUrl || 'https://placehold.co/96x96'}
                alt={story.title}
                className="h-16 w-16 rounded-full border-2 border-slate-900 object-cover"
                loading="lazy"
              />
            </span>
            <span className="text-xs text-slate-200 line-clamp-1 w-20 text-center">{story.title}</span>
          </button>
        ))}
      </div>
      {active && <StoryViewer stories={stories} activeId={active} onClose={() => setActive(null)} />}
    </div>
  );
};

export default StoryBar;
