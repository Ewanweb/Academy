import { Routes, Route } from 'react-router-dom';
import MainLayout from './layouts/MainLayout';
import Home from './pages/Home';
import Courses from './pages/Courses';
import CourseDetail from './pages/CourseDetail';
import CourseCategory from './pages/CourseCategory';
import About from './pages/About';
import Contact from './pages/Contact';
import BlogList from './pages/BlogList';
import BlogDetail from './pages/BlogDetail';
import FAQ from './pages/FAQ';
import Login from './pages/Login';
import Register from './pages/Register';
import Profile from './pages/Profile';
import AdminDashboard from './pages/AdminDashboard';
import { AuthProvider } from './services/AuthContext';
import ProtectedRoute from './routes/ProtectedRoute';

const App = () => (
  <AuthProvider>
    <MainLayout>
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/courses" element={<Courses />} />
        <Route path="/courses/category/:id" element={<CourseCategory />} />
        <Route path="/courses/:slug" element={<CourseDetail />} />
        <Route path="/about" element={<About />} />
        <Route path="/contact" element={<Contact />} />
        <Route path="/blog" element={<BlogList />} />
        <Route path="/blog/:slug" element={<BlogDetail />} />
        <Route path="/faq" element={<FAQ />} />
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />
        <Route path="/profile" element={<ProtectedRoute element={<Profile />} />} />
        <Route path="/admin" element={<ProtectedRoute role="Admin" element={<AdminDashboard />} />} />
      </Routes>
    </MainLayout>
  </AuthProvider>
);

export default App;
