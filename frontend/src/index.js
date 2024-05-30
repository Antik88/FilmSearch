import React, { createContext } from 'react';
import ReactDOM from 'react-dom/client';
import App from './App';
import UserStore from './Store/UserStore';
import FilmStore from './Store/FilmStore';

const root = ReactDOM.createRoot(document.getElementById('root'));

export const Context = createContext(null);

root.render(
  <React.StrictMode>
    <Context.Provider value={{
      user: new UserStore(),
      films: new FilmStore(),
    }}>
      <App />
    </Context.Provider>
  </React.StrictMode>
);
