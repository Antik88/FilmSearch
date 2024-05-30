import { observer } from "mobx-react-lite";
import React, { useContext } from "react";
import { Context } from "..";
import { Col, Row } from 'react-bootstrap';
import FilmCard from "./FilmCard";

const FilmList = observer(() => {
    const { films } = useContext(Context)

    return (
        <Row className="d-flex">
            {films.films.length !== 0 ?
                films.films.map(film =>
                    <Col key={film.id} className="mb-2">
                        <FilmCard film={film} />
                    </Col>
                )
                :
                <Row className="md-9">
                    <p style={{ height: "88vh", width: "1100px" }}>Not found</p>
                </Row>
            }
        </Row>
    );
});

export default FilmList 
