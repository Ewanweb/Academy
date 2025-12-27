import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../services/AuthContext';
import { api } from '../services/api';
import Seo from '../components/Seo';

const AdminDashboard = () => {
  const { user } = useAuth();
  const navigate = useNavigate();
  const [stats, setStats] = useState({ courses: 0, enrollments: 0, users: 0 });
  const [enrollments, setEnrollments] = useState([]);
  const [attendanceForm, setAttendanceForm] = useState({ courseId: '', title: '', sessionDate: '' });
  const [attendanceRecords, setAttendanceRecords] = useState([]);

  useEffect(() => {
    if (!user || user.role !== 'Admin') {
      navigate('/');
      return;
    }
    Promise.all([
      api.get('/admin/courses'),
      api.get('/admin/enrollments'),
      api.get('/admin/content/categories'),
    ]).then(([coursesRes, enrollRes]) => {
      setStats({
        courses: coursesRes.data.data.length,
        enrollments: enrollRes.data.data.length,
        users: '---',
      });
      setEnrollments(enrollRes.data.data);
    });
  }, [user, navigate]);

  const updateStatus = async (id, status) => {
    await api.patch(`/admin/enrollments/${id}/${status}`);
    const refreshed = await api.get('/admin/enrollments');
    setEnrollments(refreshed.data.data);
  };

  const createSession = async () => {
    if (!attendanceForm.courseId) return;
    await api.post('/admin/attendance/sessions', {
      courseId: attendanceForm.courseId,
      title: attendanceForm.title,
      sessionDate: attendanceForm.sessionDate || new Date().toISOString(),
    });
    const sessions = await api.get(`/admin/attendance/sessions/${attendanceForm.courseId}`);
    setAttendanceRecords(sessions.data.data || []);
  };

  return (
    <div className="container-wide py-10 space-y-6">
      <Seo title="داشبورد ادمین" description="مدیریت محتوا و درخواست‌های ثبت‌نام آکادمی پردیس توس." />
      <h1 className="text-3xl font-bold text-white">داشبورد ادمین</h1>
      <div className="grid gap-4 md:grid-cols-3">
        <div className="stat-card">
          <div className="text-slate-400 text-sm">تعداد دوره‌ها</div>
          <div className="text-2xl font-bold text-white">{stats.courses}</div>
        </div>
        <div className="stat-card">
          <div className="text-slate-400 text-sm">درخواست‌های ثبت‌نام</div>
          <div className="text-2xl font-bold text-white">{stats.enrollments}</div>
        </div>
        <div className="stat-card">
          <div className="text-slate-400 text-sm">کاربران</div>
          <div className="text-2xl font-bold text-white">{stats.users}</div>
        </div>
      </div>

      <div className="card">
        <h2 className="text-xl font-semibold mb-3 text-white">مدیریت درخواست‌های ثبت‌نام</h2>
        <div className="space-y-3 text-sm">
          {enrollments.map((e) => (
            <div key={e.id} className="p-3 border border-slate-700 rounded-lg flex flex-wrap items-center justify-between gap-3 bg-slate-800/60">
              <div>
                <div className="font-semibold text-white">{e.course?.title}</div>
                <div className="text-slate-300">کاربر: {e.user?.email}</div>
                <div className="text-slate-300">وضعیت فعلی: {e.status}</div>
              </div>
              <div className="flex gap-2">
                {['Pending', 'Approved', 'Rejected'].map((s) => (
                  <button key={s} onClick={() => updateStatus(e.id, s)} className="px-3 py-1 rounded-lg bg-slate-200 text-slate-900">
                    {s}
                  </button>
                ))}
              </div>
            </div>
          ))}
          {!enrollments.length && <div className="text-slate-400">درخواستی ثبت نشده است.</div>}
        </div>
      </div>

      <div className="card">
        <h2 className="text-xl font-semibold mb-3 text-white">حضور و غیاب کلاس</h2>
        <div className="grid md:grid-cols-3 gap-3 mb-3">
          <input className="input" placeholder="شناسه دوره" value={attendanceForm.courseId} onChange={(e) => setAttendanceForm({ ...attendanceForm, courseId: e.target.value })} />
          <input className="input" type="datetime-local" value={attendanceForm.sessionDate} onChange={(e) => setAttendanceForm({ ...attendanceForm, sessionDate: e.target.value })} />
          <input className="input" placeholder="عنوان جلسه" value={attendanceForm.title} onChange={(e) => setAttendanceForm({ ...attendanceForm, title: e.target.value })} />
        </div>
        <button onClick={createSession} className="bg-secondary text-slate-900 px-4 py-2 rounded-lg font-semibold">
          ایجاد جلسه حضور و غیاب
        </button>
        <div className="mt-4 space-y-2 text-sm">
          {attendanceRecords.map((s) => (
            <div key={s.id} className="p-3 rounded-lg border border-slate-700 bg-slate-800/60">
              <div className="font-semibold text-white">{s.title || 'جلسه'}</div>
              <div className="text-slate-300">{new Date(s.sessionDate).toLocaleString('fa-IR')}</div>
            </div>
          ))}
        </div>
      </div>
    </div>
  );
};

export default AdminDashboard;
