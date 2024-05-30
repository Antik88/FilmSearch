import React from "react"

const FilmInfo = ({ film }) => {

    function convertMinutesToHours(minutes) {
        const hours = Math.floor(minutes / 60);
        const remainingMinutes = minutes % 60;
        return `${hours}:${remainingMinutes}`;
    }

    return (
        <div>
            <h1>{film.title}</h1>
            <h2>Description</h2>
            <p>{film.description}</p>
            <p> <span style={{ fontWeight: "bold" }}>Director: </span> {film.director} </p>
            <p>
                <span style={{ fontWeight: "bold" }}>
                    Duration:
                </span>
                {film.duration} min. / {convertMinutesToHours(film.duration)}
            </p>
            <p>
                <span style={{ fontWeight: "bold" }}>Release Date: </span>
                {film.releaseDate}
            </p>
            <h2>Genres</h2>
            <ul style={{ textDecoration: "row" }}>
                {film.genres.map((genre) => (
                    <li key={genre.id}>{genre.name}</li>
                ))}
            </ul>
        </div>
    )
}

export default FilmInfo