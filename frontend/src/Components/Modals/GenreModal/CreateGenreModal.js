import React, { useContext, useState } from 'react';
import Modal from "react-bootstrap/Modal";
import { Button, Form } from "react-bootstrap";
import { observer } from "mobx-react-lite";
import { createGenre, getAllGenres } from '../../../Http/genreApi';
import { Context } from '../../..';

const CreateGenreModal = observer(({ show, onHide }) => {
    const { films } = useContext(Context)
    const [name, setName] = useState('')

    const addGenre = () => {
        if (!name) {
            return;
        }

        const data = {
            'name': name,
        }

        createGenre(data).finally(() => {
            getAllGenres().then(data => films.setGenres(data))
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
                    Create genre
                </Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <Form>
                    <Form.Control
                        required
                        value={name}
                        onChange={e => setName(e.target.value)}
                        className="mt-3"
                        placeholder="Genre name"
                    />
                </Form>
            </Modal.Body>
            <Modal.Footer>
                <Button variant="outline-danger" onClick={onHide}>Cancel</Button>
                <Button variant="outline-success" onClick={addGenre} type='submit'>Add</Button>
            </Modal.Footer>
        </Modal>
    );
});

export default CreateGenreModal;