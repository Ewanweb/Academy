import { useEffect, useState } from 'react';
import { useAuth } from '../services/AuthContext';
import api from '../services/api';
import { Link } from 'react-router-dom';
import Seo from '../components/Seo';

const Profile = () => {
  const { user } = useAuth();
  const [enrollments, setEnrollments] = useState([]);

  useEffect(() => {
    if (user) {
      api.get('/enrollments/me').then((res) => setEnrollments(res.data.data || []));
    }
  }, [user]);

  if (!user) {
    return (
      <div className="container-wide py-10">
        برای مشاهده پروفایل ابتدا وارد شوید. <Link to="/login" className="text-primary">ورود</Link>
      </div>
    );
  }

  return (
    <div className="container-wide py-10 space-y-6">
      <Seo title="پروفایل کاربری" description="مدیریت درخواست‌های ثبت‌نام و اطلاعات کاربری." />
      <div className="card">
        <h1 className="text-2xl font-bold mb-2 text-white">پروفایل کاربری</h1>
        <p className="text-slate-200">نام: {user.fullName}</p>
        <p className="text-slate-200">نقش: {user.role}</p>
      </div>
      <div className="card">
        <h2 className="text-xl font-semibold mb-3 text-white">دوره‌های من</h2>
        <div className="space-y-3 text-sm">
          {enrollments.map((e) => (
            <div key={e.id} className="p-3 rounded-lg border border-slate-700 flex items-center justify-between bg-slate-800/60">
              <div>
                <div className="font-semibold text-white">{e.course?.title}</div>
                <div className="text-slate-300">وضعیت درخواست: {e.status}</div>
                <div className="text-slate-400 text-xs">مدت دوره: {e.course?.durationWeeks} هفته • قیمت: {e.course?.price?.toLocaleString()} تومان</div>
              </div>
              <span className="text-slate-400 text-xs">{new Date(e.createdAt).toLocaleDateString('fa-IR')}</span>
            </div>
          ))}
          {!enrollments.length && <div className="text-slate-400">درخواستی ثبت نشده است.</div>}
        </div>
      </div>
    </div>
  );
};

export default Profile;
