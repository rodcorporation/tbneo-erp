import Vue from 'vue';
import Router from 'vue-router';

// Views
import Login from './views/commons/Login';
import Main from './views/commons/Main';
import Home from './views/commons/Home';

// -- Projetos
import ProjetoListagem from './views/projeto/ProjetoListagem';
import ProjetoCadastrar from './views/projeto/ProjetoCadastrar';
import ProjetoEditar from './views/projeto/ProjetoEditar';

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
                { path: '/home', name: 'home', component: Home },
                { path: '/projetos', name: 'projetoListagem', component: ProjetoListagem },
                { path: '/projetos/cadastrar', name: 'projetoCadastrar', component: ProjetoCadastrar },
                { path: '/projetos/editar/:id', name: 'projetoEditar', component: ProjetoEditar }
            ]
        },
        { path: '*', redirect: '/login' }
    ]
});

export default router;