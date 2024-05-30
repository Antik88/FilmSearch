import { useState } from "react";
import { Button, Col, Container, Row } from "react-bootstrap";
import CreateActorModal from "../Components/Modals/ActorModals/CreateActorModal";
import DeleteActorModal from "../Components/Modals/ActorModals/DeleteActorModal";
import CreateGenreModal from "../Components/Modals/GenreModal/CreateGenreModal";
import DeleteGenteModal from "../Components/Modals/GenreModal/DeleteGenreModal";
import UpdateGenreModal from "../Components/Modals/GenreModal/UpdateGenreModal";
import { observer } from "mobx-react-lite";
import CreateFilmModal from "../Components/Modals/FilmModals/CreateFilmModal";
import DeleteFilmModal from "../Components/Modals/FilmModals/DeleteFilmModal";

const AdminPage = () => {
    const [createFilmVisible, setCreateFilmVisible] = useState(false)
    const [delteFilmVisible, setDeleteFilmVisible] = useState(false)

    const [actorCreateVisible, setActorCreateVisible] = useState(false)
    const [actorDelteVisible, setActorDeleteVisible] = useState(false)

    const [genreCreateVisible, setGenreCreateVisible] = useState(false)
    const [genreDelteVisible, setGenreDeleteVisible] = useState(false)
    const [genreUpdateVisible, setGenreUpdateVisible] = useState(false)

    return (
        <Container>
            <Row className="mt-2">
                <Col md={5}>
                    <h1>Genres</h1>
                    <Button variant="success" onClick={() => setGenreCreateVisible(true)}>
                        Create
                    </Button>
                    <Button variant="danger" className="ms-2" onClick={() => setGenreDeleteVisible(true)}>
                        Detelte
                    </Button>
                    <Button variant="warning" className="ms-2" onClick={() => setGenreUpdateVisible(true)}>
                        Edit
                    </Button>
                </Col>
            </Row>
            <Row className="mt-2">
                <Col md={5}>
                    <h1>Actor</h1>
                    <Button variant="success" onClick={() => setActorCreateVisible(true)}>
                        Create
                    </Button>
                    <Button variant="danger" className="ms-2" onClick={() => setActorDeleteVisible(true)}>
                        Detelte
                    </Button>
                </Col>
            </Row>
            <Row className="mt-2">
                <Col md={5}>
                    <h1>Film</h1>
                    <Button variant="success" onClick={() => setCreateFilmVisible(true)}>
                        Create
                    </Button>
                    <Button variant="danger" className="ms-2" onClick={() => setDeleteFilmVisible(true)}>
                        Detelte
                    </Button>
                </Col>
            </Row>
            <CreateActorModal show={actorCreateVisible} onHide={() => setActorCreateVisible(false)} />
            <DeleteActorModal show={actorDelteVisible} onHide={() => setActorDeleteVisible(false)} />

            <CreateGenreModal show={genreCreateVisible} onHide={() => setGenreCreateVisible(false)} />
            <DeleteGenteModal show={genreDelteVisible} onHide={() => setGenreDeleteVisible(false)} />
            <UpdateGenreModal show={genreUpdateVisible} onHide={() => setGenreUpdateVisible(false)} />

            <CreateFilmModal show={createFilmVisible} onHide={() => setCreateFilmVisible(false)} />
            <DeleteFilmModal show={delteFilmVisible} onHide={() => setDeleteFilmVisible(false)} />
        </Container>
    );
}

export default observer(AdminPage)