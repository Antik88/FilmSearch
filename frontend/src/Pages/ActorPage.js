import { useContext, useEffect, useState } from "react";
import { Button, Col, Container, Image, Nav, Row, Spinner } from "react-bootstrap";
import { useParams } from "react-router-dom";
import { getActorById } from "../Http/actorApi";
import { FILM_ROUTE } from "../Utils/consts";
import UpdActorModal from "../Components/Modals/ActorModals/UpdActorModal";
import { Context } from "..";

const ActorPage = () => {
    const { id } = useParams();
    const { user } = useContext(Context);
    const [actor, setActor] = useState();
    const [loading, setLoading] = useState(true);

    const [actorUpdVisible, setActorUpdVisible] = useState(false)

    useEffect(() => {
        getActorById(id)
            .then((data) => {
                setActor(data);
            })
            .finally(() => setLoading(false));
    }, [id]);

    if (loading || !actor) {
        return <Spinner animation={"grow"} />;
    }

    return (
        <Container>
            <Row className="mt-2">
                <Col md={4}>
                    <Image
                        src={
                            process.env.REACT_APP_API_URL +
                            "StaticContent/getImg?FileName=" +
                            actor.imageName
                        }
                        alt={actor.firstName + " " + actor.lastName}
                        fluid
                    />
                </Col>
                <Col md={7}>
                    <h1>
                        {actor.firstName} {actor.lastName}
                    </h1>
                    <p>Birth Date: {actor.birthDate}</p>
                    <h2>Films:</h2>
                    {actor.films.map((film) => (
                        <Nav.Link
                            key={film.id}
                            href={FILM_ROUTE + '/' + film.id}
                        >
                            {film.title}
                        </Nav.Link>
                    ))}
                </Col>
            </Row>
            {localStorage.getItem('role') && localStorage.getItem('role').includes('Admin') && (
                <Row className="mt-2">
                    <Col>
                        <Button variant="warning" className="ms-2" onClick={() => setActorUpdVisible(true)}>
                            Edit
                        </Button>
                    </Col>
                </Row>
            )}
            <UpdActorModal id={id} show={actorUpdVisible} onHide={() => setActorUpdVisible(false)} />
        </Container>
    );
};

export default ActorPage;