import React from "react";
import { Button, CardImg, Col } from 'react-bootstrap';
import { Card, CardBody } from 'react-bootstrap';
import { useNavigate } from "react-router-dom";
import { FILM_ROUTE } from "../Utils/consts";
import noImage from '../Assets/noimage.jpg'

const FilmCard = ({ film }) => {
    const history = useNavigate()

    return (
        <Card
            style={{
                border: "none",
                boxShadow: "0 4px 8px 0 rgba(0,0,0,0.2)",
                transition: "0.3s",
                "&:hover": {
                    boxShadow: "0 8px 16px 0 rgba(0,0,0,0.2)",
                },
            }}
        >
            <CardBody>
                {film.imageName ? (
                    <CardImg
                        className="carousel-image"
                        style={{
                            height: "300px",
                            objectFit: "cover"
                        }}
                        src={
                            process.env.REACT_APP_API_URL +
                            "StaticContent/getImg?FileName=" +
                            film.imageName
                        }
                    />
                ) : (
                    <CardImg
                        className="carousel-image"
                        src={noImage}
                        style={{
                            width: "100%",
                            height: "300px",
                            objectFit: "cover"
                        }}
                    />
                )}
                <p>{film.title}</p>
                <Button
                    variant="outline-primary"
                    className="mt-2"
                    onClick={() => history(FILM_ROUTE + "/" + film.id)}
                    style={{ textDecoration: "none", cursor: "pointer", }}
                >
                    more
                </Button>
            </CardBody>
        </Card>
    );
};

export default FilmCard 
