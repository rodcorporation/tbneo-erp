import request from './requestServiceBase';

const logSistemaService = {
    async list(token, id) {
        return await request.requestGet(`/api/v1/log-sistema/${id}`, token);
    },
    async add(token, model) {
        return await request.requestPost('/api/v1/log-sistema', token, model);
    }
}

export default logSistemaService;