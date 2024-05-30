import { Col, Container, Form, Row, Button } from "react-bootstrap";
import { login, registration } from "../Http/userApi";
import { useContext, useState } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import { LOGIN_ROUTE, MAINPAGE_ROUTE, REGISTRATION_ROUTE } from "../Utils/consts";
import { Context } from "..";

const AuthPage = () => {
    const navigate = useNavigate()

    const location = useLocation()
    const isLogin = location.pathname === LOGIN_ROUTE

    const { user } = useContext(Context)

    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');

    const handleAuthBtnClick = async () => {
        try {

            let data
            if (isLogin) {
                data = await login(username, password)
            } else {
                data = await registration(username, password)
            }
            user.setUser(data)
            user.setIsAuth(true)
            navigate(MAINPAGE_ROUTE)
        }
        catch (e) {
            console.log(e)
        }
    }

    return (
        <Container className="d-flex justify-content-center align-items-center" style={{ minHeight: '100vh' }}>
            <Row>
                <Col md={12} className="mx-auto">
                    <Form>
                        <Form.Group >
                            <Form.Control
                                id="formUsername"
                                className="mb-2"
                                placeholder="User name"
                                type="text"
                                value={username}
                                onChange={(e) => setUsername(e.target.value)}
                            />
                            <Form.Control
                                id="formPassword"
                                className="mb-2"
                                placeholder="Password"
                                type="password"
                                value={password}
                                onChange={(e) => setPassword(e.target.value)}
                            />
                        </Form.Group>

                        <div className="d-grid gap-2">
                            <Button variant="primary"
                                style={{ height: "40px" }}
                                onClick={handleAuthBtnClick}
                            >
                                {isLogin ?
                                    <p>
                                        Login
                                    </p>
                                    :
                                    <p>
                                        Registration
                                    </p>
                                }
                            </Button>
                            {isLogin ?
                                <Button variant="link" href={REGISTRATION_ROUTE}>
                                    Don't have an account? Sign up
                                </Button>
                                :
                                <Button variant="link" href={LOGIN_ROUTE}>
                                    Have an account? sign in
                                </Button>
                            }
                        </div>
                    </Form>
                </Col>
            </Row>
        </Container>
    );
}

export default AuthPage;