import Vue from 'vue';
import Router from 'vue-router';

Vue.use(Router);

import Login from './views/commons/Login.vue';
import Main from './views/commons/Main.vue.vue';
import Home from './views/commons/Home.vue';

export default new Router ({
    mode: 'history',
    base: process.env.BASE_URL,
    routes:[
        { path: '', redirect: '/login' },
        { path: '/login', name: 'login', component: Login },
        { path: '/', name: 'main', component: Main,
            children: [
                { path: '/home', name: 'home', component: Home }
            ]},
    ]
});