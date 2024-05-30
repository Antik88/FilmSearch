import { $authHost, $host } from ".";

export const getAllGenres = async () => {
    const response = await $host.get('Genre/all')
    return response.data
}

export const createGenre = async (name) => {
    const response = await $authHost.post('Genre/create', name)
    return response.data
}

export const deleteGenre = async (id) => {
    const response = await $authHost.delete('Genre/delete', { params: { id } })
    return response.data
}

export const updateGenreById = async (id, name) => {
    const response = await $authHost.put('Genre/update', name, { params: { id } })
    return response.data
}