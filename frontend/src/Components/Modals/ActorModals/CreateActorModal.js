import React, { useState } from 'react';
import Modal from "react-bootstrap/Modal";
import { Button, Form } from "react-bootstrap";
import { observer } from "mobx-react-lite";
import { createActor, uploadActorImage } from '../../../Http/actorApi';

const CreateActorModal = observer(({ show, onHide }) => {
    const [firstName, setFirstName] = useState('')
    const [lastName, setLastName] = useState('')
    const [birthDate, setBirthDate] = useState('')
    const [file, setFile] = useState(null)

    const addActor = () => {
        if (!firstName || !lastName || !birthDate) {
            alert('Please fill in all required fields');
            return;
        }

        const data = {
            'firstName': firstName,
            'lastName': lastName,
            'birthDate': birthDate,
        }

        createActor(data).then(res => {
            if (file) {
                uploadActorImage(res.id, file)
            }
        }).finally(() => {
            clear()
            onHide()
        })
    }

    const clear = () => {
        setFirstName('')
        setLastName('')
        setBirthDate('')
        setFile(null)
    }

    return (
        <Modal
            show={show}
            onHide={onHide}
            centered
        >
            <Modal.Header closeButton>
                <Modal.Title id="contained-modal-title-vcenter">
                    Create actor
                </Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <Form>
                    <Form.Control
                        required
                        value={firstName}
                        onChange={e => setFirstName(e.target.value)}
                        className="mt-3"
                        placeholder="First name"
                    />
                    <Form.Control
                        required
                        value={lastName}
                        onChange={e => setLastName(e.target.value)}
                        className="mt-3"
                        placeholder="Last name"
                    />
                    <Form.Control
                        required
                        value={birthDate}
                        onChange={e => setBirthDate(e.target.value)}
                        type='date'
                        className="mt-3"
                    />
                    <Form.Control
                        className="mt-3"
                        type="file"
                        onChange={e =>
                            setFile(e.target.files[0])
                        }
                    />
                </Form>
            </Modal.Body>
            <Modal.Footer>
                <Button variant="outline-danger" onClick={onHide}>Cancel</Button>
                <Button variant="outline-success" onClick={addActor} type='submit'>Add</Button>
            </Modal.Footer>
        </Modal>
    );
});

export default CreateActorModal;