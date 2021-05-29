import axios from 'axios';

const urlBase = "http://localhost:4308";

const request = {
    async requestGet(uriApi, token) {
        const url = urlBase + uriApi;
        const headers = {};

        if (token) {
            headers['Authorization'] = `Bearer ${token}`
        }

        try {
            const response = await axios.get(url, { headers });

            return response.data || {};
        } catch (error) {
            console.log(error)
            return ['Usuário ou senha inválidos'];
        }
    },
    async requestPost(uriApi, token, body) {
        const url = urlBase + uriApi;
        const headers = {};

        if (token) {
            headers['Authorization'] = `Bearer ${token}`
        }

        try {
            const response = await axios.post(url, body, { headers });

            return response.data || {};
        } catch (error) {
            console.log(error)
            return ['Usuário ou senha inválidos'];
        }
    },
    async requestPut(uriApi, token, body) {
        const url = urlBase + uriApi;
        const headers = {};

        if (token) {
            headers['Authorization'] = `Bearer ${token}`
        }

        try {
            const response = await axios.put(url, body, { headers });

            return response.data || {};
        } catch(error) {
            console.log(error)
            return ['Usuário ou senha inválidos'];
        }
    }
}

export default request