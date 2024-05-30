import React, { useContext, useEffect, useState } from 'react';
import Modal from "react-bootstrap/Modal";
import { Button, Form } from "react-bootstrap";
import { observer } from "mobx-react-lite";
import { getAllGenres, deleteGenre } from '../../../Http/genreApi';
import { Context } from '../../..';

const DeleteGenreModal = observer(({ show, onHide }) => {
    const { films } = useContext(Context)

    const [selectedGenreId, setSelectedGenreId] = useState('');

    useEffect(() => {
        getAllGenres().then(data => films.setGenres(data));
    }, []);

    const handleGenreSelect = (event) => {
        setSelectedGenreId(event.target.value);
    };

    const handleDelete = () => {
        if (selectedGenreId) {
            deleteGenre(selectedGenreId).then(() => {
                getAllGenres().then(data => films.setGenres(data))
                onHide();
            });
        }
    };

    return (
        <Modal
            show={show}
            onHide={onHide}
            centered
        >
            <Modal.Header closeButton>
                <Modal.Title id="contained-modal-title-vcenter">
                    Delete Genre
                </Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <Form>
                    <Form.Group controlId="genreSelect">
                        <Form.Label>Select a genre to delete:</Form.Label>
                        <Form.Control
                            as="select"
                            value={selectedGenreId}
                            onChange={handleGenreSelect}
                        >
                            <option value="">Select a genre</option>
                            {films.genres.map((genre) => (
                                <option key={genre.id} value={genre.id}>
                                    {genre.name}
                                </option>
                            ))}
                        </Form.Control>
                    </Form.Group>
                </Form>
            </Modal.Body>
            <Modal.Footer>
                <Button variant="outline-warning" onClick={onHide}>Cancel</Button>
                <Button variant="outline-danger" onClick={handleDelete}>Delete</Button>
            </Modal.Footer>
        </Modal>
    );
});

export default DeleteGenreModal;