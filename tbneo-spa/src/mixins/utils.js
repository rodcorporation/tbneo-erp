export default {
    methods: {
        salvarToken(token) {
            return sessionStorage.setItem('token', token);
        },
        lerToken() {
            return sessionStorage.getItem('token');
        }
    }    
}