import React, { createContext, useContext, useEffect, useState } from 'react';
import { api } from './api';

const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
  const [user, setUser] = useState(() => {
    const token = localStorage.getItem('token');
    const role = localStorage.getItem('role');
    const fullName = localStorage.getItem('fullName');
    return token ? { token, role, fullName } : null;
  });

  const login = async (email, password) => {
    const { data } = await api.post('/auth/login', { email, password });
    const payload = data.data;
    localStorage.setItem('token', payload.token);
    localStorage.setItem('role', payload.role);
    localStorage.setItem('fullName', payload.fullName);
    setUser({ token: payload.token, role: payload.role, fullName: payload.fullName });
  };

  const register = async (fullName, email, password) => {
    const { data } = await api.post('/auth/register', { fullName, email, password });
    const payload = data.data;
    localStorage.setItem('token', payload.token);
    localStorage.setItem('role', payload.role);
    localStorage.setItem('fullName', payload.fullName);
    setUser({ token: payload.token, role: payload.role, fullName: payload.fullName });
  };

  const logout = () => {
    localStorage.clear();
    setUser(null);
  };

  useEffect(() => {
    // No-op placeholder for potential future profile refresh
  }, []);

  return (
    <AuthContext.Provider value={{ user, login, logout, register }}>
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => useContext(AuthContext);
