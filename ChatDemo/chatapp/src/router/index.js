import { createRouter, createWebHistory } from 'vue-router';
import Login from '@/components/Login.vue';
import ChatApp from '@/components/ChatApp.vue';
import Register from '@/components/Register.vue';
import { useUserStore } from '@/stores/user';

const routes = [
    {
        path: '/login',
        component: Login,
    },
    {
        path: '/',
        component: ChatApp,
    },
    {
        path: '/register',
        component: Register,
    },
];

const router = createRouter({
    history: createWebHistory(),
    routes,
});

router.beforeEach(async (to, from, next) => {
    const userStore = useUserStore();

    if (userStore.loggedIn) {
        //Pass
        return next();
    }
    else {
        // 定义不需要判断的页面路径或名称
        const exemptPages = ['/login','/register']; // 根据路径

        // 如果目标页面在免判断列表中，直接跳过
        if (exemptPages.includes(to.path)) {
            return next();
        }
        return next('/login');

    }
});

export default router;
