import 'bootstrap/dist/css/bootstrap.min.css';
import '@smastrom/react-rating/style.css'
import "react-datepicker/dist/react-datepicker.css";
import { BrowserRouter } from 'react-router-dom';
import AppRouter from './Components/AppRouter';
import NavBar from './Components/NavBar';
import { observer } from 'mobx-react-lite';
import { useContext, useEffect, useState } from 'react';
import { Context } from '.';
import { check } from './Http/userApi';
import { Spinner } from 'react-bootstrap';

function App() {
  const { user } = useContext(Context)
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const token = localStorage.getItem('token');
    if (token) {
      check().then(data => {
        try {
          user.setUser(data)
          user.setIsAuth(true)
        }
        catch {
          console.log('Unauthorized')
        }
      }).finally(() => setLoading(false))
    }
    else{
      setLoading(false) 
    }
  }, [])

  if (loading) {
    return <Spinner animation={'grow'} />
  }

  return (
    <BrowserRouter>
      <NavBar />
      <AppRouter />
    </BrowserRouter>
  );
}

export default observer(App);
