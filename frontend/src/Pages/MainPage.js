import { useContext, useEffect } from "react";
import { Col, Container, Row } from "react-bootstrap";
import { Context } from "..";
import { observer } from "mobx-react-lite";
import FilmList from "../Components/FilmList";
import { getAllFilms } from "../Http/filmApi";
import SearchBar from "../Components/SearchBar";

const MainPage = () => {
    const { films } = useContext(Context)

    useEffect(() => {
        getAllFilms().then(data => films.setFilms(data))
    }, [])


    return (
        <Container className='d-flex mt-4'>
            <Row>
                <Col md={5}>
                    <SearchBar />
                </Col>
                <Col md={7}>
                    <FilmList />
                </Col>
            </Row>
        </Container>
    );
}

export default observer(MainPage)
