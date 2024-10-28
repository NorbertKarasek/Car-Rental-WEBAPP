import axios from 'axios';

const api = axios.create({
    baseURL: process.env.REACT_APP_API_URL,
});

// Add an interceptor to automatically add a token to headers
api.interceptors.request.use(
    (config) => {
        const token = localStorage.getItem('token');
        if (token) {
            config.headers!['Authorization'] = `Bearer ${token}`;
        }
        return config;
    },
    (error) => Promise.reject(error)
);

// Logout user if token is invalid
api.interceptors.response.use(
    (response) => response,
    (error) => {
        if (error.response && error.response.status === 401) {
            // Log out the user if the token is invalid
            localStorage.removeItem('token');
            // You can redirect the user to the login page
            window.location.href = '/login';
        }
        return Promise.reject(error);
    }
);


export default api;
