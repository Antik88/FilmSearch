import { Button, Col, Container, Image, Row, Spinner } from "react-bootstrap";
import { useParams } from "react-router-dom";
import { getFilmById } from "../Http/filmApi";
import noImage from '../Assets/noimage.jpg'
import { useContext, useEffect, useState } from "react";
import ReviewCard from "../Components/ReviewCard";
import ActorCard from "../Components/ActorCard";
import FilmInfo from "../Components/FilmInfo";
import { Context } from "..";
import CreateReviewModal from "../Components/Modals/ReviewModals/CreateReviewModal";
import EditFilmModal from "../Components/Modals/FilmModals/EditFilmModal";

const FilmPage = () => {
    const { id } = useParams()
    const { user } = useContext(Context)

    const [film, setFilm] = useState(null);
    const [loading, setLoading] = useState(true);

    const [reviewCreateVisible, setReviewCreateVisible] = useState(false)
    const [editFilmVisible, setEditFilmVisible] = useState(false)

    useEffect(() => {
        getFilmById(id)
            .then(data => {
                setFilm(data);
            })
            .finally(() => setLoading(false));
    }, [id]);

    function hasUserReviewed(reviews) {
        return reviews.some(review => review.userId === localStorage.getItem('userId'));
    }

    if (loading || !film) {
        return <Spinner animation={'grow'} />
    }

    return (
        <Container>
            <Row className="mt-2">
                <Col>
                    {film.imageName ? (
                        <Image
                            style={{
                                width: "300px",
                                height: "500px",
                                objectFit: "cover"
                            }}
                            src={
                                process.env.REACT_APP_API_URL +
                                "StaticContent/getImg?FileName=" +
                                film.imageName
                            }
                        />
                    ) : (
                        <Image
                            className="carousel-image"
                            src={noImage}
                            style={{
                                width: "100%",
                                height: "400px",
                                objectFit: "cover"
                            }}
                        />
                    )}
                    {localStorage.getItem('role') && localStorage.getItem('role').includes('Admin') && (
                        <Button variant="outline-primary" className="mt-2" onClick={() => setEditFilmVisible(true)}>
                            Edit
                        </Button>
                    )}
                </Col>
                <Col md={9}>
                    <FilmInfo film={film} />
                    <Row>
                        <h2>Actors</h2>
                        {film.actors.map((actor) => (
                            <Col key={actor.id} xs={12} sm={6} md={4} lg={3}>
                                <ActorCard actor={actor} />
                            </Col>
                        ))}
                    </Row>
                </Col>
            </Row>
            <Row>
                <Col md={4}>
                    <h2>Reviews</h2>
                    {user.isAuth && !hasUserReviewed(film.reviews, user.id) ? (
                        <Button
                            className="mb-2"
                            onClick={() => setReviewCreateVisible(true)}
                        >
                            Post review
                        </Button>
                    ) : user.isAuth ? (
                        <p>You have already left a review for this film.</p>
                    ) : (
                        <p>Please log in to leave a review.</p>
                    )}
                    {film.reviews.length === 0 ? (
                        <p>No reviews yet.</p>
                    ) : (
                        <Row>
                            <Col>
                                {film.reviews.map((review) => (
                                    <ReviewCard key={review.id} review={review} />
                                ))}
                            </Col>
                        </Row>
                    )}
                </Col>
            </Row>
            <CreateReviewModal filmId={id} show={reviewCreateVisible} onHide={() => setReviewCreateVisible(false)} />
            <EditFilmModal filmId={id} show={editFilmVisible} onHide={() => setEditFilmVisible(false)} />
        </Container >
    );
}

export default FilmPage