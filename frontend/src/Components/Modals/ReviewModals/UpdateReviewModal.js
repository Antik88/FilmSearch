import React, { useEffect, useState } from 'react';
import Modal from "react-bootstrap/Modal";
import { Button, Form } from "react-bootstrap";
import { observer } from "mobx-react-lite";
import { updateReview } from '../../../Http/reviewApi';
import { Rating } from '@smastrom/react-rating';

const UpdateReviewModal = observer(({ review, show, onHide }) => {
    const [title, setTitle] = useState('')
    const [description, setDescription] = useState('')
    const [stars, setStars] = useState(null)


    useEffect(() => {
        setTitle(review.title)
        setDescription(review.description)
        setStars(review.stars)
    }, [])

    const editReview = () => {
        if (!title || !description || !stars) {
            return;
        }

        const data = {
            'title': title,
            'description': description,
            'stars': stars,
        }

        updateReview(review.id, data).finally(() => {
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
                    Update review
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
                <Button variant="outline-success" onClick={editReview} type='submit'>Edit</Button>
            </Modal.Footer>
        </Modal>
    );
});

export default UpdateReviewModal;