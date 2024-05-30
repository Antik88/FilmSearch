import React, { useContext, useState } from 'react';
import Modal from "react-bootstrap/Modal";
import { Button, Form } from "react-bootstrap";
import { observer } from "mobx-react-lite";
import { Context } from '../../..';
import { getAllGenres, updateGenreById } from '../../../Http/genreApi';

const UpdateGenreModal = observer(({ show, onHide }) => {
    const { films } = useContext(Context)
    const [id, setId] = useState('');
    const [name, setName] = useState('');

    const updateGenre = () => {
        const data = { "name": name }
        updateGenreById(id, data)
            .then(() => {
                getAllGenres().then(data => films.setGenres(data))
            })
            .finally(() => {
                setName('')
                setId('')
                onHide()
            })
    }

    return (
        <Modal
            show={show}
            onHide={onHide}
            centered
        >
            <Modal.Header closeButton>
                <Modal.Title id="contained-modal-title-vcenter">
                    Update genre
                </Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <Form>
                    {films.genres.map((genre) => (
                        <div key={genre.id}>
                            {genre.id}. {genre.name}
                        </div>
                    ))}
                    <Form.Control
                        value={id}
                        onChange={e => setId(e.target.value)}
                        className="mt-3"
                        placeholder="id"
                    />
                    <Form.Control
                        value={name}
                        onChange={e => setName(e.target.value)}
                        className="mt-3"
                        placeholder="name"
                    />
                </Form>
            </Modal.Body>
            <Modal.Footer>
                <Button variant="outline-secondary" onClick={onHide}>Cancel</Button>
                <Button variant="outline-warning" onClick={updateGenre}>Update</Button>
            </Modal.Footer>
        </Modal>
    );
});

export default UpdateGenreModal;