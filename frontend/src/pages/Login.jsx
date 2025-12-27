import { useState } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import { useAuth } from '../services/AuthContext';
import Seo from '../components/Seo';

const Login = () => {
  const { login } = useAuth();
  const navigate = useNavigate();
  const [form, setForm] = useState({ email: '', password: '' });
  const [error, setError] = useState('');

  const handleChange = (e) => setForm({ ...form, [e.target.name]: e.target.value });

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await login(form.email, form.password);
      navigate('/');
    } catch (err) {
      setError(err.response?.data?.message || 'خطا در ورود');
    }
  };

  return (
    <div className="container-wide py-10 flex justify-center">
      <Seo title="ورود" description="ورود به حساب کاربری آکادمی پردیس توس" />
      <form onSubmit={handleSubmit} className="bg-white rounded-xl shadow-sm border border-gray-100 p-6 w-full max-w-md space-y-3">
        <h1 className="text-2xl font-bold mb-2">ورود</h1>
        <input name="email" value={form.email} onChange={handleChange} placeholder="ایمیل" className="input" required />
        <input type="password" name="password" value={form.password} onChange={handleChange} placeholder="رمز عبور" className="input" required />
        <button type="submit" className="bg-primary text-white rounded-lg py-2 w-full">ورود</button>
        {error && <div className="text-red-600 text-sm">{error}</div>}
        <p className="text-sm text-gray-600">
          حساب کاربری ندارید؟ <Link to="/register" className="text-primary">ثبت‌نام</Link>
        </p>
      </form>
    </div>
  );
};

export default Login;
