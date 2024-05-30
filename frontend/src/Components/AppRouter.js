import React, { useContext } from 'react';
import { Routes, Route, Navigate } from 'react-router-dom'
import { authRoutes, publicRoutes } from '../routes';
import { MAINPAGE_ROUTE } from '../Utils/consts';
import { Context } from '..';
import { observer } from 'mobx-react-lite';

const AppRouter = () => {
    const { user } = useContext(Context);

    return (
        <Routes>
            {user.isAuth && authRoutes.map(({ path, Component }) =>
                <Route key={path} path={path} element={<Component />} exact />
            )}
            {publicRoutes.map(({ path, Component }) =>
                <Route key={path} path={path} element={<Component />} exact />
            )}
            <Route path='*' element={<Navigate to={MAINPAGE_ROUTE} />} />
        </Routes>
    );
}

export default observer(AppRouter);
