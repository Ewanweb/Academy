const Footer = () => (
  <footer className="bg-slate-950 text-slate-200 mt-10 border-t border-slate-800">
    <div className="container-wide py-10 grid gap-6 md:grid-cols-3 text-sm">
      <div className="glass rounded-2xl p-5">
        <h4 className="text-lg font-semibold mb-3 text-secondary">آکادمی پردیس توس</h4>
        <p className="text-slate-300 leading-7">آموزشگاه تخصصی فناوری با دوره‌های به‌روز و پروژه‌محور.</p>
      </div>
      <div className="glass rounded-2xl p-5">
        <h4 className="text-lg font-semibold mb-3 text-secondary">تماس با ما</h4>
        <p className="text-slate-300">تلفن: ۰۲۱-۱۲۳۴۵۶۷۸</p>
        <p className="text-slate-300">ایمیل: info@pardistous.ir</p>
        <p className="text-slate-300">آدرس: مشهد، بلوار معلم، پلاک ۱۲</p>
      </div>
      <div className="glass rounded-2xl p-5">
        <h4 className="text-lg font-semibold mb-3 text-secondary">شبکه‌های اجتماعی</h4>
        <div className="flex gap-3 text-slate-300">
          <a href="#" className="hover:text-white">اینستاگرام</a>
          <a href="#" className="hover:text-white">لینکدین</a>
          <a href="#" className="hover:text-white">تلگرام</a>
        </div>
      </div>
    </div>
    <div className="text-center py-4 border-t border-slate-800 text-xs text-slate-400">
      © {new Date().getFullYear()} آکادمی پردیس توس - تمامی حقوق محفوظ است.
    </div>
  </footer>
);

export default Footer;
