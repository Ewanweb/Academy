import { useEffect, useState } from 'react';
import { api } from '../services/api';
import Seo from '../components/Seo';

const FAQ = () => {
  const [faqs, setFaqs] = useState([]);

  useEffect(() => {
    api.get('/public/home').then((res) => setFaqs(res.data.data.faqs || []));
  }, []);

  return (
    <div className="container-wide py-10">
      <Seo title="سوالات متداول" description="پرسش‌ها و پاسخ‌های پرتکرار درباره ثبت‌نام و دوره‌های آکادمی." />
      <h1 className="text-3xl font-bold mb-6">سوالات متداول</h1>
      <div className="space-y-4">
        {faqs.map((faq) => (
          <details key={faq.id} className="bg-white border border-gray-100 rounded-xl p-4 shadow-sm">
            <summary className="cursor-pointer font-semibold">{faq.question}</summary>
            <p className="text-gray-700 mt-2 leading-7">{faq.answer}</p>
          </details>
        ))}
      </div>
    </div>
  );
};

export default FAQ;
