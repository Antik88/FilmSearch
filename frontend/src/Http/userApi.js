import { $authHost, $host } from ".";
import { jwtDecode } from "jwt-decode";
import Cookies from "js-cookie";

export const registration = async (name, password) => {
    const response = await $host.post('Auth/register', { name, password })
    setUserData(response.data)
    return jwtDecode(response.data)
}

export const login = async (name, password) => {
    const response = await $host.post('Auth/login', { name, password })
    setUserData(response.data)
    return jwtDecode(response.data)
}

export const check = async () => {
    const response = await $authHost.post('Auth/check')
    Cookies.set('token', response.data);
    return response
}

const setUserData = (token) => {
    localStorage.setItem('token', token);
    localStorage.setItem('userId', jwtDecode(token)['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier']);
    localStorage.setItem('role', jwtDecode(token)['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']);
}