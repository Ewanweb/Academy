import { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import api from '../services/api';
import Seo from '../components/Seo';

const BlogDetail = () => {
  const { slug } = useParams();
  const [post, setPost] = useState(null);

  useEffect(() => {
    api.get(`/blog/${slug}`).then((res) => setPost(res.data.data));
  }, [slug]);

  if (!post) return <div className="container-wide py-10">در حال بارگذاری...</div>;

  return (
    <div className="container-wide py-10">
      <Seo title={post.title} description={post.summary || post.content?.slice(0, 160)} />
      <div className="bg-white border border-gray-100 rounded-2xl shadow-sm p-6">
        <div className="text-xs text-gray-500 mb-2">{new Date(post.publishedAt).toLocaleDateString('fa-IR')}</div>
        <h1 className="text-3xl font-bold mb-4">{post.title}</h1>
        <p className="text-gray-700 leading-8 whitespace-pre-line">{post.content}</p>
      </div>
    </div>
  );
};

export default BlogDetail;
