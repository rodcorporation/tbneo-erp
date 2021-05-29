import request from './requestServiceBase';

const featureFlagService = {
    async list(token, filtroIdProjeto) {
        if (filtroIdProjeto) {
            return await request.requestGet(`/api/v1/feature-flag?idProjeto=${filtroIdProjeto}`, token);
        } else {
            return await request.requestGet('/api/v1/feature-flag', token);
        }
    },
    async get(token, id) {
        return await request.requestGet(`/api/v1/feature-flag/${id}`, token);
    },
    async add(token, model) {
        return await request.requestPost('/api/v1/feature-flag', token, model);
    },
    async update(token, id, model) {
        return await request.requestPut(`/api/v1/feature-flag/${id}`, token, model);
    }
}

export default featureFlagService