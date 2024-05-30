import React, { useContext, useEffect, useState } from 'react';
import Modal from "react-bootstrap/Modal";
import { Button, Form } from "react-bootstrap";
import { observer } from "mobx-react-lite";
import { Context } from '../../..';
import { deleteFilm, getAllFilms } from '../../../Http/filmApi';

const DeleteFilmModal = observer(({ show, onHide }) => {
    const { films } = useContext(Context)

    const [selectedFilmId, setSelectedFilmId] = useState('');

    useEffect(() => {
        getAllFilms().then(data => films.setFilms(data));
    }, []);

    const handleFilmSelect = (event) => {
        setSelectedFilmId(event.target.value);
    };

    const handleDelete = () => {
        if (selectedFilmId) {
            deleteFilm(selectedFilmId).then(() => {
                getAllFilms().then(data => films.setFilms(data))
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
                    Delete film 
                </Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <Form>
                    <Form.Group controlId="filmSelect">
                        <Form.Label>Select a fiilm to delete:</Form.Label>
                        <Form.Control
                            as="select"
                            value={selectedFilmId}
                            onChange={handleFilmSelect}
                        >
                            <option value="">Select a film</option>
                            {films.films.map((film) => (
                                <option key={film.id} value={film.id}>
                                    {film.title}
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

export default DeleteFilmModal;