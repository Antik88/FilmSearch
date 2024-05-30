import React from "react"
import { Card } from "react-bootstrap"
import noAvatar from '../Assets/noAvatar.jpg'
import { NavLink, useNavigate } from "react-router-dom";
import { ACTOR_ROUTE } from "../Utils/consts";

const ActorCard = ({ actor }) => {
    const history = useNavigate()

    return (
        <Card
            style={{
                minHeight: "241px",
                marginBottom: "20px",
                border: "none",
                boxShadow: "0 4px 8px 0 rgba(0,0,0,0.2)",
                transition: "0.3s",
                "&:hover": {
                    boxShadow: "0 8px 16px 0 rgba(0,0,0,0.2)",
                },
            }}
            onClick={() => history(ACTOR_ROUTE + "/" + actor.id)}
        >
            <NavLink
            >
                {actor.imageName ?
                    <Card.Img
                        style={{
                            height: "200px",
                            objectFit: "cover"
                        }}
                        src={
                            process.env.REACT_APP_API_URL +
                            "StaticContent/getImg?FileName=" +
                            actor.imageName
                        }
                    />
                    :
                    <Card.Img style={{ height: "200px" }} src={noAvatar} />
                }
            </NavLink>
            <Card.Body
                style={{
                    textAlign: "center",
                    paddingTop: "0.5rem",
                }}
            >
                {actor.firstName} {actor.lastName}
            </Card.Body>
        </Card>
    )
}

export default ActorCard