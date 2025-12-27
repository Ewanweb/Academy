/** @type {import('tailwindcss').Config} */
export default {
  content: ['./index.html', './src/**/*.{js,jsx,ts,tsx}'],
  theme: {
    extend: {
      fontFamily: {
        vazir: ['"Vazirmatn"', 'sans-serif'],
      },
      colors: {
        primary: '#0f172a',
        secondary: '#38bdf8',
        accent: '#a855f7',
      },
    },
  },
  plugins: [],
};
