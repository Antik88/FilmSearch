import React, { useContext, useState, useEffect, useRef } from 'react';
import Modal from "react-bootstrap/Modal";
import { Button, Col, Form, FormControl, FormGroup, FormLabel, Row } from "react-bootstrap";
import { observer } from "mobx-react-lite";
import { Context } from '../../..';
import { getAllGenres } from '../../../Http/genreApi';
import { getAllActors } from '../../../Http/actorApi';
import { createFilm, getFilmById, updateFilm, uploadFilmImage } from '../../../Http/filmApi';

const EditFilmModal = observer(({ filmId, show, onHide }) => {
    const { films } = useContext(Context)

    const [film, setFilm] = useState('')
    const [title, setTitle] = useState('')
    const [description, setDescription] = useState('')
    const [releaseDate, setReleaseDate] = useState('')
    const [duration, setDuration] = useState('')
    const [director, setDirector] = useState('')
    const [genreIds, setGenreIds] = useState([])
    const [actorIds, setActorIds] = useState([])
    const [file, setFile] = useState(null)

    const fileInputRef = useRef(null);

    useEffect(() => {
        getAllGenres().then(data => films.setGenres(data))
        getAllActors().then(data => films.setActors(data))
    }, [])

    useEffect(() => {
        getFilmById(filmId).then(data => {
            setFilm(data)
            if (data) {
                setTitle(data.title);
                setDescription(data.description);
                setReleaseDate(data.releaseDate);
                setDuration(data.duration);
                setDirector(data.director);
            }
        })
    }, [filmId])

    const handleFileButtonClick = () => {
        fileInputRef.current.click();
    }

    const handleFileChange = (e) => {
        setFile(e.target.files[0]);
    }

    const editFilm = () => {
        if (!title || !description || !releaseDate || !duration || !director) {
            return;
        }

        const data = {
            "title": title,
            "description": description,
            "releaseDate": releaseDate,
            "duration": duration,
            "director": director,
            "genreIds": genreIds,
            "actorIds": actorIds
        }

        updateFilm(filmId, data).then(res => {
            if (file) {
                uploadFilmImage(filmId, file)
            }
        }).finally(() => {
            clear()
            onHide()
            window.location.reload()
        })
    }

    const clear = () => {
        setTitle('');
        setDescription('');
        setReleaseDate('');
        setDuration('');
        setDirector('');
        setGenreIds([]);
        setActorIds([]);
        setFile(null);
    };

    const handleGenreChange = (genreId) => {
        if (genreIds.includes(genreId)) {
            setGenreIds(genreIds.filter(id => id !== genreId))
        } else {
            setGenreIds([...genreIds, genreId])
        }
    }

    const handleActorChange = (actorId) => {
        if (actorIds.includes(actorId)) {
            setActorIds(actorIds.filter(id => id !== actorId))
        } else {
            setActorIds([...actorIds, actorId])
        }
    }

    return (
        <Modal
            show={show}
            onHide={onHide}
            centered
        >
            <Modal.Header closeButton>
                <Modal.Title id="contained-modal-title-vcenter">
                    Edit film
                </Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <Form>
                    <FormGroup>
                        <FormControl
                            required
                            value={title}
                            onChange={e => setTitle(e.target.value)}
                            className="mt-3"
                            placeholder="Film title"
                        />
                        <FormControl
                            required
                            value={description}
                            as="textarea"
                            onChange={e => setDescription(e.target.value)}
                            className="mt-3"
                            placeholder="Film description"
                        />
                        <FormControl
                            required
                            value={releaseDate}
                            type="date"
                            onChange={e => setReleaseDate(e.target.value)}
                            className="mt-3"
                            placeholder="Film release date"
                        />
                        <FormControl
                            required
                            value={duration}
                            type='number'
                            onChange={e => setDuration(e.target.value)}
                            className="mt-3"
                            placeholder="Film duration"
                        />
                        <FormControl
                            required
                            value={director}
                            onChange={e => setDirector(e.target.value)}
                            className="mt-3"
                            placeholder="Film director"
                        />
                        <div className="mt-3 d-flex align-items-center">
                            <Button variant="outline-primary" onClick={handleFileButtonClick}>
                                Choose File
                            </Button>
                            {file && (
                                <>
                                    <span className='ms-2'>{file.name}</span>
                                    <Button
                                        variant="outline-danger"
                                        className="ms-2"
                                        onClick={() => setFile(null)}
                                    >
                                        Clear
                                    </Button>
                                </>
                            )}
                            <input
                                ref={fileInputRef}
                                type="file"
                                onChange={handleFileChange}
                                style={{ display: 'none' }}
                            />
                        </div>
                        <FormLabel>Genres</FormLabel>
                        <Row>
                            {films.genres.map(genre => (
                                <Col xs={12} sm={6} md={4} lg={3} key={genre.id}>
                                    <Form.Check
                                        type="checkbox"
                                        label={genre.name}
                                        onChange={() => handleGenreChange(genre.id)}
                                    />
                                </Col>
                            ))}
                        </Row>
                        <FormLabel>Actors</FormLabel>
                        <Row>
                            {films.actors.map(actor => (
                                <Col xs={12} sm={6} md={4} lg={3} key={actor.id}>
                                    <Form.Check
                                        type="checkbox"
                                        label={`${actor.firstName} ${actor.lastName}`}
                                        checked={actorIds.includes(actor.id)}
                                        onChange={() => handleActorChange(actor.id)}
                                    />
                                </Col>
                            ))}
                        </Row>
                    </FormGroup>
                </Form>
            </Modal.Body>
            <Modal.Footer>
                <Button variant="outline-danger" onClick={onHide}>Cancel</Button>
                <Button variant="outline-warning" onClick={editFilm} type='submit'>Edit</Button>
            </Modal.Footer>
        </Modal>
    );
});

export default EditFilmModal;