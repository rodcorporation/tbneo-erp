import Vue from 'vue';
import Router from 'vue-router';

// Views
import Login from './views/commons/Login.vue';
import Main from './views/commons/Main.vue';
import Home from './views/commons/Home.vue';

Vue.use(Router);

const router = new Router({
    mode: 'history',
    base: process.env.BASE_URL,
    routes: [
        { path: '', redirect: '/login' },
        { path: '/login', name: 'login', component: Login },
        {
            path: '/', name: 'main', component: Main,
            children: [
                { path: '/home', name: 'home', component: Home }
            ]
        },
        { path: '*', redirect: '/login' }
    ]
});

export default router;