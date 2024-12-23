import { createRouter, createWebHistory } from 'vue-router';
import Login from '@/components/Login.vue';
import ChatApp from '@/components/ChatApp.vue';

const routes = [
    {
        path: '/login',
        component: Login,
    },
    {
        path: '/chat',
        component: ChatApp,
    },
];

const router = createRouter({
    history: createWebHistory(),
    routes,
});

export default router;
