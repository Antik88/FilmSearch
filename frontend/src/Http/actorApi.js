import { $authHost, $host } from ".";

export const getAllActors = async () => {
    const response = await $host.get('Actor/all')
    return response.data
}

export const getActorById = async (id) => {
    const { data } = await $host.get('Actor/' + id)
    return data
}

export const deleteActorbyId = async (id) => {
    const { data } = await $authHost.delete('Actor/delete', { params: { id } })
    return data
}

export const createActor = async (actor) => {
    const response = await $authHost.post('Actor/create', actor)
    return response.data
}

export const updateActor = async (id, actor) => {
    const response = await $authHost.put('Actor/update', actor, { params: { id } })
    return response.data
}

export const uploadActorImage = async (actorId, file) => {
    const formData = new FormData();
    formData.append('_IFormFile', file);

    const response = await $authHost.put('Actor/uploadImg', formData, {
        params: {
            actorId
        }
    })

    return response
}