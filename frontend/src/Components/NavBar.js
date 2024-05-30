import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import { NavLink } from 'react-router-dom';
import { ADMIN_ROUTE, MAINPAGE_ROUTE, LOGIN_ROUTE, ACTORLIST_ROUTE } from '../Utils/consts';
import { observer } from 'mobx-react-lite';
import { useContext } from 'react';
import { Context } from '..';
import { Button } from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';

const NavBar = observer(() => {
    const { user } = useContext(Context)
    const navigate = useNavigate()

    const logout = () => {
        localStorage.clear()
        user.setUser({})
        user.setIsAuth(false)
    }

    return (
        <Navbar bg="dark" data-bs-theme="dark">
            <Container>
                <Nav>
                    <NavLink
                        style={{ fontSize: "19px", color: "wheat", textDecoration: "none" }}
                        to={MAINPAGE_ROUTE}
                    >
                        FilmSearcher
                    </NavLink>
                </Nav>
                <Nav className='ml-auto'>
                    <Button
                        variant='outlined-primary'
                        style={{ color: "grey", textDecoration: "none" }}
                        onClick={() => navigate(ACTORLIST_ROUTE)}
                    >
                        Actors
                    </Button>
                    {user.isAuth ?
                        <Nav className="ml-auto">
                            {localStorage.getItem('role') && localStorage.getItem('role').includes('Admin') ? (
                                <Button
                                    className="me-2"
                                    style={{ color: "grey", textDecoration: "none" }}
                                    onClick={() => navigate(ADMIN_ROUTE)}
                                    variant='outlined-primary'
                                >
                                    AdminPage
                                </Button>
                            ) : null}
                            <Button
                                variant='outlined-primary'
                                style={{ color: "grey", textDecoration: "none" }}
                                onClick={() => logout()}
                            >
                                logout
                            </Button>
                        </Nav>
                        :
                        <Nav className="ml-auto">
                            <Button
                                variant='outlined-primary'
                                style={{ color: "grey", textDecoration: "none" }}
                                onClick={() => navigate(LOGIN_ROUTE)}
                            >
                                login
                            </Button>
                        </Nav>
                    }
                </Nav>
            </Container>
        </Navbar >
    );
});

export default NavBar;
