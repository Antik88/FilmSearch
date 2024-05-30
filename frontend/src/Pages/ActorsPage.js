import React, { useContext, useEffect, useState } from "react";
import { Col, Container, Row, Spinner } from "react-bootstrap";
import { Context } from "..";
import { observer } from "mobx-react-lite";
import { getAllActors } from "../Http/actorApi";
import ActorCard from "../Components/ActorCard";

const ActorsPage = () => {
    const { films } = useContext(Context)

    const [loading, setLoading] = useState(true);

    useEffect(() => {
        getAllActors().then(data => films.setActors(data)).finally(() => setLoading(false))
    }, [])

    if (loading || !films.actors) {
        return <Spinner animation={'grow'} />
    }

    return (
        <Container>
            <Row>
                <h1>Actors</h1>
                {films.actors.map((actor) => (
                    <Col key={actor.id} xs={12} sm={6} md={4} lg={3}>
                        <ActorCard actor={actor} />
                    </Col>
                ))}
            </Row>
        </Container>
    )
}

export default observer(ActorsPage)