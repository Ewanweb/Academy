import { useState } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import { useAuth } from '../services/AuthContext';
import Seo from '../components/Seo';

const Register = () => {
  const { register } = useAuth();
  const navigate = useNavigate();
  const [form, setForm] = useState({ fullName: '', email: '', password: '' });
  const [error, setError] = useState('');

  const handleChange = (e) => setForm({ ...form, [e.target.name]: e.target.value });

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await register(form.fullName, form.email, form.password);
      navigate('/');
    } catch (err) {
      setError(err.response?.data?.message || 'خطا در ثبت‌نام');
    }
  };

  return (
    <div className="container-wide py-10 flex justify-center">
      <Seo title="ثبت‌نام" description="ایجاد حساب در آکادمی پردیس توس و دسترسی به دوره‌ها" />
      <form onSubmit={handleSubmit} className="bg-white rounded-xl shadow-sm border border-gray-100 p-6 w-full max-w-md space-y-3">
        <h1 className="text-2xl font-bold mb-2">ثبت‌نام</h1>
        <input name="fullName" value={form.fullName} onChange={handleChange} placeholder="نام و نام خانوادگی" className="input" required />
        <input name="email" value={form.email} onChange={handleChange} placeholder="ایمیل" className="input" required />
        <input type="password" name="password" value={form.password} onChange={handleChange} placeholder="رمز عبور" className="input" required />
        <button type="submit" className="bg-primary text-white rounded-lg py-2 w-full">ایجاد حساب</button>
        {error && <div className="text-red-600 text-sm">{error}</div>}
        <p className="text-sm text-gray-600">
          قبلا ثبت‌نام کرده‌اید؟ <Link to="/login" className="text-primary">ورود</Link>
        </p>
      </form>
    </div>
  );
};

export default Register;
