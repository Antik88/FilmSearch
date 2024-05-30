import React, { useEffect, useRef, useState } from 'react';
import Modal from "react-bootstrap/Modal";
import { Button, Form, Spinner } from "react-bootstrap";
import { observer } from "mobx-react-lite";
import { getActorById, updateActor, uploadActorImage } from '../../../Http/actorApi';

const UpdActorModal = observer(({ id, show, onHide }) => {
    const [actor, setActor] = useState()
    const [loading, setLoading] = useState(true);

    const [firstName, setFirstName] = useState('')
    const [lastName, setLastName] = useState('')
    const [birthDate, setBirthDate] = useState('')
    const [file, setFile] = useState(null)

    const fileInputRef = useRef(null);

    useEffect(() => {
        getActorById(id)
            .then((data) => {
                setActor(data);
                if (data) {
                    setFirstName(data.firstName);
                    setLastName(data.lastName);
                    setBirthDate(data.birthDate);
                }
            })
            .finally(() => setLoading(false));
    }, [id]);

    const clear = () => {
        setFirstName('')
        setLastName('')
        setBirthDate('')
        setFile(null)
    }

    const updActor = () => {
        let updData = {
            'firstName': '',
            'lastName': '',
            'birthDate': ''
        }

        if (firstName) {
            updData.firstName = firstName;
        }

        if (lastName) {
            updData.lastName = lastName;
        }

        if (birthDate) {
            updData.birthDate = birthDate;
        }

        updateActor(id, updData).then(() => {
            if (file) {
                uploadActorImage(id, file)
            }
        }).finally(() => {
            clear()
            onHide()
        })

    };

    const handleFileButtonClick = () => {
        fileInputRef.current.click();
    }

    const handleFileChange = (e) => {
        setFile(e.target.files[0]);
    }

    if (loading || !actor) {
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
                    Update actor
                </Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <Form>
                    <Form.Control
                        value={firstName}
                        onChange={e => setFirstName(e.target.value)}
                        className="mt-3"
                        placeholder={actor.firstName}
                    />
                    <Form.Control
                        value={birthDate}
                        onChange={e => setBirthDate(e.target.value)}
                        type='date'
                        className="mt-3"
                    />
                    <Form.Control
                        value={lastName}
                        onChange={e => setLastName(e.target.value)}
                        className="mt-3"
                        placeholder={actor.lastName}
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
                </Form>
            </Modal.Body>
            <Modal.Footer>
                <Button variant="outline-danger" onClick={onHide}>Cancel</Button>
                <Button variant="outline-warning" onClick={updActor}>Update</Button>
            </Modal.Footer>
        </Modal>
    );
});

export default UpdActorModal;