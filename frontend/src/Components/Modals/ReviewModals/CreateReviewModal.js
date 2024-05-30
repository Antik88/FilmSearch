import React, { useContext, useState } from 'react';
import Modal from "react-bootstrap/Modal";
import { Button, Form } from "react-bootstrap";
import { observer } from "mobx-react-lite";
import { Context } from '../../..';
import { createReview } from '../../../Http/reviewApi';
import { Rating } from '@smastrom/react-rating';

const CreateReviewModal = observer(({ filmId, show, onHide }) => {
    const { films } = useContext(Context)

    const [title, setTitle] = useState('')
    const [description, setDescription] = useState('')
    const [stars, setStars] = useState(null)

    const addReview = () => {
        if (!title || !description || !stars) {
            return;
        }

        const data = {
            'title': title,
            'description': description,
            'stars': stars,
        }

        createReview(filmId, data).finally(() => {
            setTitle('')
            setDescription('')
            setStars('')
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
                    Create review
                </Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <Form>
                    <Form.Control
                        required
                        value={title}
                        onChange={e => setTitle(e.target.value)}
                        className="mt-3"
                        placeholder="Review title"
                    />
                    <Form.Control
                        required
                        as="textarea"
                        rows={3}
                        value={description}
                        onChange={(e) => setDescription(e.target.value)}
                        className="mt-3"
                        placeholder="Review description"
                    />
                    <Rating style={{ maxWidth: 200, marginTop: '10px' }} value={stars} onChange={setStars} />
                </Form>
            </Modal.Body>
            <Modal.Footer>
                <Button variant="outline-danger" onClick={onHide}>Cancel</Button>
                <Button variant="outline-success" onClick={addReview} type='submit'>Add</Button>
            </Modal.Footer>
        </Modal>
    );
});

export default CreateReviewModal;