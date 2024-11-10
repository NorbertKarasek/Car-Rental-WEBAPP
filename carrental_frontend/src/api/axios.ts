import axios from 'axios';

// Create an instance of axios with a base URL
const api = axios.create({
    baseURL: process.env.REACT_APP_API_URL,
});

// Add an interceptor to automatically add a token to headers
api.interceptors.request.use(
    (config) => {
        // Get the token from local storage
        const token = localStorage.getItem('token');
        if (token) {
            // If token exists, add it to the Authorization header
            config.headers!['Authorization'] = `Bearer ${token}`;
        }
        return config;
    },
    (error) => {
        // Handle request error
        console.error('Request error:', error);
        return Promise.reject(error);
    }
);

// Add an interceptor to handle responses
api.interceptors.response.use(
    (response) => response, // Return the response if successful
    (error) => {
        if (error.response) {
            // Handle specific status codes
            if (error.response.status === 401) {
                // If the response status is 401 (Unauthorized), remove the token from local storage
                localStorage.removeItem('token');
                // Redirect the user to the login page
                window.location.href = '/login';
            } else {
                // Log other response errors
                console.error('Response error:', error.response);
            }
        } else {
            // Log errors without a response (e.g., network errors)
            console.error('Network error:', error);
        }
        return Promise.reject(error); // Handle response error
    }
);

export default api;