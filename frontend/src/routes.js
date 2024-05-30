import MainPage from './Pages/MainPage';
import AdminPage from './Pages/AdminPage'
import AuthPage from './Pages/AuthPage'
import FilmPage from './Pages/FilmPage'
import ActorPage from './Pages/ActorPage'
import ActorsPage from './Pages/ActorsPage';

import {
    ADMIN_ROUTE,
    LOGIN_ROUTE,
    REGISTRATION_ROUTE,
    FILM_ROUTE, 
    MAINPAGE_ROUTE,
    ACTOR_ROUTE,
    ACTORLIST_ROUTE
} from './Utils/consts';

export const authRoutes = [
    {
        path: ADMIN_ROUTE,
        Component: AdminPage 
    },
]

export const publicRoutes = [
    {
        path: MAINPAGE_ROUTE,
        Component: MainPage 
    },
    {
        path: ACTOR_ROUTE + '/:id',
        Component: ActorPage  
    },
    {
        path: ACTORLIST_ROUTE,
        Component: ActorsPage  
    },
    {
        path: LOGIN_ROUTE,
        Component: AuthPage 
    },
    {
        path: REGISTRATION_ROUTE,
        Component: AuthPage
    },
    {
        path: FILM_ROUTE + '/:id',
        Component: FilmPage 
    },
]
