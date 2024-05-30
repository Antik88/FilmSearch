import React, { useContext, useEffect, useRef, useState } from "react";
import { Button, Col, Form, Row, Spinner } from "react-bootstrap";
import { getAllGenres } from "../Http/genreApi";
import { Context } from "..";
import { getAllFilms } from "../Http/filmApi";

const SearchBar = () => {
    const { films } = useContext(Context);

    const [selectedGenres, setSelectedGenres] = useState([]);
    const [loading, setLoading] = useState(true);

    const titleRef = useRef(null);
    const yearRef = useRef(null);

    useEffect(() => {
        getAllGenres().then((data) => films.setGenres(data))
            .finally(() => setLoading(false));
    }, [films]);

    function handleGenreChange(genreId) {
        if (selectedGenres.includes(genreId)) {
            setSelectedGenres(selectedGenres.filter((id) => id !== genreId));
        } else {
            setSelectedGenres([...selectedGenres, genreId]);
        }
    }

    function handleSearch() {
        const searchData = {
            title: titleRef.current.value,
            releaseYear: yearRef.current.value,
            genreNames: selectedGenres,
        };

        getAllFilms(
            searchData.title,
            searchData.releaseYear,
            searchData.genreNames
        ).then((data) => films.setFilms(data));
    }

    function handleClear() {
        titleRef.current.value = "";
        yearRef.current.value = "";
        setSelectedGenres([]);

        getAllFilms().then((data) => films.setFilms(data))
    }

    if (loading || !films.genres) {
        return <Spinner animation={'grow'} />
    }

    return (
        <Col>
            <p>Search options</p>
            <Row className="mt-2">
                <Form.Control placeholder="Film title" ref={titleRef} />
            </Row>
            <Row className="mt-2">
                <Form.Control placeholder="Release year" type="number" ref={yearRef} />
            </Row>
            <Row className="mt-2">
                <Form.Group>
                    <p>Genres:</p>
                    {films.genres.lenght === 0 ?
                        <p>no genres</p>
                        :
                        films.genres.map((genre) => (
                            <Form.Check
                                key={genre.id}
                                type="checkbox"
                                id={`genre-${genre.id}`}
                                label={genre.name}
                                checked={selectedGenres.includes(genre.name)}
                                onChange={() => handleGenreChange(genre.name)}
                            />
                        ))
                    }
                </Form.Group>
            </Row>
            <Row className="mt-2">
                <Col className="d-flex">
                    <Button
                        onClick={handleClear}
                        variant="secondary"
                        className="me-2"
                    >
                        Cancel
                    </Button>
                    <Button
                        onClick={handleSearch}
                    >
                        Search
                    </Button>
                </Col>
            </Row>
        </Col>
    );
};

export default SearchBar;