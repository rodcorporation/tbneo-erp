import request from './requestServiceBase';

const usuarioService = {
    async login(model) {
        return await request.requestPost('/api/v1/login', null, model);
    }
}

export default usuarioService;