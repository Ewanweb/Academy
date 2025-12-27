import { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import api from '../services/api';
import Seo from '../components/Seo';

const BlogList = () => {
  const [posts, setPosts] = useState([]);

  useEffect(() => {
    api.get('/blog').then((res) => setPosts(res.data.data.items || []));
  }, []);

  return (
    <div className="container-wide py-10">
      <Seo title="بلاگ" description="آخرین مقالات آموزشی آکادمی پردیس توس در حوزه وب و بک‌اند." />
      <h1 className="text-3xl font-bold mb-6">مقالات بلاگ</h1>
      <div className="grid gap-4 md:grid-cols-3">
        {posts.map((post) => (
          <Link key={post.id} to={`/blog/${post.slug}`} className="bg-white rounded-xl border border-gray-100 p-4 shadow-sm block">
            <div className="text-xs text-gray-500 mb-2">{new Date(post.publishedAt).toLocaleDateString('fa-IR')}</div>
            <h3 className="font-semibold text-lg mb-2">{post.title}</h3>
            <p className="text-gray-700 text-sm leading-7">{post.summary}</p>
          </Link>
        ))}
      </div>
    </div>
  );
};

export default BlogList;
