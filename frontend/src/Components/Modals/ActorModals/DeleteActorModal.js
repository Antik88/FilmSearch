import React, { useEffect, useState } from 'react';
import Modal from "react-bootstrap/Modal";
import { Button, Form, Spinner } from "react-bootstrap";
import { observer } from "mobx-react-lite";
import { deleteActorbyId, getAllActors } from '../../../Http/actorApi';

const DeleteActorModal = observer(({ show, onHide }) => {
    const [actors, setActors] = useState([]);
    const [selectedActorId, setSelectedActorId] = useState('');
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        getAllActors().then(data => setActors(data)).finally(() => setLoading(false));
    }, []);

    const handleActorSelect = (event) => {
        setSelectedActorId(event.target.value);
    }

    const handleDelete = () => {
        deleteActorbyId(selectedActorId).then(() => {
            onHide()
        }).finally(() => getAllActors().then((data) => setActors(data)))
    }

    if (loading || !actors) {
        return <Spinner animation={"grow"} />;
    }

    return (
        <Modal
            show={show}
            onHide={onHide}
            centered
        >
            <Modal.Header closeButton>
                <Modal.Title id="contained-modal-title-vcenter">
                    Delete actor
                </Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <Form>
                    <Form.Select value={selectedActorId} onChange={handleActorSelect}>
                        <option value="">Select an actor</option>
                        {actors.map(actor => (
                            <option key={actor.id} value={actor.id}>
                                {actor.firstName} {actor.lastName}
                            </option>
                        ))}
                    </Form.Select>
                </Form>
            </Modal.Body>
            <Modal.Footer>
                <Button variant="outline-secondary" onClick={onHide}>Cancel</Button>
                <Button variant="outline-danger" onClick={handleDelete}>Delete</Button>
            </Modal.Footer>
        </Modal>
    );
});

export default DeleteActorModal;