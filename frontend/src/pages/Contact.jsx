import { useState } from 'react';
import Seo from '../components/Seo';

const Contact = () => {
  const [form, setForm] = useState({ name: '', email: '', message: '' });
  const [submitted, setSubmitted] = useState(false);

  const handleChange = (e) => setForm({ ...form, [e.target.name]: e.target.value });
  const handleSubmit = (e) => {
    e.preventDefault();
    setSubmitted(true);
  };

  return (
    <div className="container-wide py-10 grid gap-6 md:grid-cols-2">
      <Seo title="تماس با ما" description="راه‌های ارتباطی با آکادمی پردیس توس برای مشاوره و ثبت‌نام." />
      <div>
        <h1 className="text-3xl font-bold mb-4">تماس با ما</h1>
        <p className="text-gray-700 mb-4">برای مشاوره دوره‌ها یا همکاری با ما تماس بگیرید.</p>
        <ul className="text-gray-700 space-y-2">
          <li>تلفن: ۰۲۱-۱۲۳۴۵۶۷۸</li>
          <li>ایمیل: info@pardistous.ir</li>
          <li>آدرس: مشهد، بلوار معلم، پلاک ۱۲</li>
        </ul>
      </div>
      <form onSubmit={handleSubmit} className="bg-white border border-gray-100 rounded-2xl shadow-sm p-6 space-y-3">
        <input name="name" value={form.name} onChange={handleChange} placeholder="نام و نام خانوادگی" className="input" required />
        <input name="email" value={form.email} onChange={handleChange} placeholder="ایمیل" className="input" required />
        <textarea name="message" value={form.message} onChange={handleChange} placeholder="پیام شما" className="input h-32" required />
        <button type="submit" className="bg-primary text-white rounded-lg py-2 w-full">ارسال پیام</button>
        {submitted && <div className="text-green-600 text-sm">پیام شما ثبت شد. به زودی پاسخ می‌دهیم.</div>}
      </form>
    </div>
  );
};

export default Contact;
