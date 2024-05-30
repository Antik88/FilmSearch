import { $authHost } from ".";

export const createReview = async (filmId, data) => {
    const response = await $authHost.post('Review/create', data, { params: { filmId } })
    return response.data
}

export const updateReview = async (id, data) => {
    const response = await $authHost.put('Review/update', data, { params: { id } })
    return response.data
}

export const deleteReview = async (id) => {
    const response = await $authHost.delete('Review/delete', { params: { id } })
    return response.data
}