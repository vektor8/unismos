import axios, { AxiosInstance } from 'axios';

export const Axios: AxiosInstance = axios.create({
    baseURL: "https://localhost:7005/api/",
});

Axios.interceptors.request.use(
    async config => {
        const token = localStorage.getItem("access_token");
        config.headers = {
            'Authorization': `Bearer ${token}`,
        }
        return config;
    },
    error => {
        Promise.reject(error)
    });

