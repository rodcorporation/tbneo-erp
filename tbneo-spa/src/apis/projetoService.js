import request from './requestServiceBase';

const projetoService = {
    async list(token) {
        return await request.requestGet('/api/v1/projeto', token);
    },
    async get(token, id) {
        return await request.requestGet(`/api/v1/projeto/${id}`, token);
    },
    async add(token, model) {
        return await request.requestPost('/api/v1/projeto', token, model);
    },
    async update(token, id, model) {
        return await request.requestPut(`/api/v1/projeto/${id}`, token, model);
    }
}

export default projetoService