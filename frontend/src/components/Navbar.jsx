import { Link, NavLink } from 'react-router-dom';
import { useAuth } from '../services/AuthContext';

const navItems = [
  { to: '/', label: 'خانه' },
  { to: '/courses', label: 'دوره‌ها' },
  { to: '/blog', label: 'بلاگ' },
  { to: '/faq', label: 'سوالات متداول' },
  { to: '/contact', label: 'تماس با ما' },
];

const Navbar = () => {
  const { user, logout } = useAuth();
  return (
    <header className="bg-slate-950/70 backdrop-blur-xl border-b border-slate-800 shadow-lg shadow-slate-900/60 sticky top-0 z-50">
      <div className="container-wide flex items-center justify-between py-4">
        <Link to="/" className="text-xl font-extrabold text-secondary">
          آکادمی پردیس توس
        </Link>
        <nav className="flex items-center gap-4 text-sm font-medium text-slate-200">
          {navItems.map((item) => (
            <NavLink
              key={item.to}
              to={item.to}
              className={({ isActive }) =>
                `px-3 py-1 rounded-full transition-all duration-200 ${isActive ? 'bg-white/10 text-secondary shadow-md shadow-secondary/20' : 'text-slate-300 hover:text-secondary hover:bg-white/5'}`
              }
            >
              {item.label}
            </NavLink>
          ))}
        </nav>
        <div className="flex items-center gap-3 text-sm">
          {user ? (
            <>
              <NavLink to="/profile" className="text-slate-200 hover:text-secondary">
                {user.fullName || 'پروفایل'}
              </NavLink>
              {user.role === 'Admin' && (
                <NavLink to="/admin" className="px-3 py-1 rounded-full bg-secondary text-slate-900 font-semibold shadow hover:shadow-secondary/40">
                  پنل ادمین
                </NavLink>
              )}
              <button onClick={logout} className="px-3 py-1 rounded-full border border-slate-700 text-slate-200 hover:border-secondary hover:text-secondary transition-colors">
                خروج
              </button>
            </>
          ) : (
            <>
              <NavLink to="/login" className="text-slate-200 hover:text-secondary">
                ورود
              </NavLink>
              <NavLink to="/register" className="px-3 py-1 rounded-full bg-secondary text-slate-900 font-semibold shadow hover:shadow-secondary/40">
                ثبت‌نام
              </NavLink>
            </>
          )}
        </div>
      </div>
    </header>
  );
};

export default Navbar;
