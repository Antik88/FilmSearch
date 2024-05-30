import React, { useContext, useState, useEffect } from 'react';
import Modal from "react-bootstrap/Modal";
import { Button, Col, Form, FormControl, FormGroup, FormLabel, Row } from "react-bootstrap";
import { observer } from "mobx-react-lite";
import { Context } from '../../..';
import { getAllGenres } from '../../../Http/genreApi';
import { getAllActors } from '../../../Http/actorApi';
import { createFilm, uploadFilmImage } from '../../../Http/filmApi';

const CreateFilmModal = observer(({ show, onHide }) => {
    const { films } = useContext(Context)

    const [title, setTitle] = useState('')
    const [description, setDescription] = useState('')
    const [releaseDate, setReleaseDate] = useState('')
    const [duration, setDuration] = useState('')
    const [director, setDirector] = useState('')
    const [genreIds, setGenreIds] = useState([])
    const [actorIds, setActorIds] = useState([])
    const [file, setFile] = useState(null)

    useEffect(() => {
        getAllGenres().then(data => films.setGenres(data))
        getAllActors().then(data => films.setActors(data))
    }, [])

    const addFilm = () => {
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

        createFilm(data).then(res => {
            if (file) {
                uploadFilmImage(res.data.id, file)
            }
        }).finally(() => {
            clear()
            onHide()
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
                    Create Film
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
                        <FormControl
                            className="mt-3"
                            type="file"
                            onChange={e =>
                                setFile(e.target.files[0])
                            }
                        />
                        <FormLabel>Genres</FormLabel>
                        <Row>
                            {films.genres.map(genre => (
                                <Col xs={12} sm={6} md={4} lg={3} key={genre.id}>
                                    <Form.Check
                                        type="checkbox"
                                        label={genre.name}
                                        checked={genreIds.includes(genre.id)}
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
                <Button variant="outline-success" onClick={addFilm} type='submit'>Add</Button>
            </Modal.Footer>
        </Modal>
    );
});

export default CreateFilmModal;