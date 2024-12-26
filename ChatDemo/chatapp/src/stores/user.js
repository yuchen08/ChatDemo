import { defineStore } from 'pinia';
import { ref } from 'vue';


export const useUserStore = defineStore('user', () => {
    // 定义状态 (state)
    const username = ref('');
    const passward = ref('');
    const loggedIn = ref(false);

    // 定义方法 (actions)
    function login(payload) {
        username.value = payload.username;
        passward.value = payload.passward;
        loggedIn.value = true;
    }

    function logout() {
        username.value = '';
        passward.value = '';
        loggedIn.value = false;
    }

    // 返回状态、计算属性和方法
    return {
        username,
        passward,
        loggedIn,
        login,
        logout,
    };
});