import { $authHost, $host } from ".";

export const getAllFilms = async (title, releaseYear, genreNames) => {
    const params = {
        title,
        releaseYear,
    };

    if (Array.isArray(genreNames) && genreNames.length > 0) {
        genreNames.forEach((genre) => {
            params[`genreNames`] = genre;
        });
    }

    const response = await $host.get('Film/all', {
        params,
    });
    return response.data;
};

export const getFilmById = async (id) => {
    const { data } = await $host.get('Film/' + id)
    return data
}

export const createFilm = async (data) => {
    const response = await $authHost.post('Film/create', data)
    return response
}

export const deleteFilm = async (id) => {
    const { response } = await $authHost.delete('Film/delete', { params: { id } })
    return response
}

export const uploadFilmImage = async (filmId, file) => {
    const formData = new FormData();
    formData.append('_IFormFile', file);

    const response = await $authHost.put(`Film/uploadImg?filmId=${filmId}`, formData)

    return response
}

export const updateFilm = async (id, film) => {
    const response = await $authHost.put('Film/update', film, { params: { id } })
    return response.data
}