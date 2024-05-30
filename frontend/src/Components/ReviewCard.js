import { Rating } from "@smastrom/react-rating"
import React, { useState } from "react"
import { Button, Card } from "react-bootstrap"
import UpdateReviewModal from "./Modals/ReviewModals/UpdateReviewModal"
import { deleteReview } from "../Http/reviewApi"

const ReviewCard = ({ review }) => {

    const [reviewUpdateVisible, setReviewUpdateVisible] = useState(false)

    return (
        <Card key={review.id} className="mb-3">
            <Card.Body>
                <Card.Title>{review.title}</Card.Title>
                <Card.Subtitle className="mb-2 text-muted">{review.userName}</Card.Subtitle>
                <Rating readOnly value={review.stars} style={{ maxWidth: 100 }} />
                <Card.Text>{review.description}</Card.Text>

                {review.userId === localStorage.getItem('userId') && (
                    <Button
                        variant="outline-primary"
                        size="sm"
                        className="float-end"
                        onClick={() => setReviewUpdateVisible(true)}
                    >
                        Edit
                    </Button>
                )}
                {localStorage.getItem('role') && localStorage.getItem('role').includes('Admin') ? (
                    <Button
                        variant="outline-danger"
                        size="sm"
                        className="float-end me-2"
                        onClick={() => deleteReview(review.id).then(() => window.location.reload())}
                    >
                        delete
                    </Button>
                ): <></>}
            </Card.Body>
            <UpdateReviewModal review={review} show={reviewUpdateVisible} onHide={() => setReviewUpdateVisible(false)} />
        </Card>
    )
}

export default ReviewCard