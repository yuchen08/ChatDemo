<template>
    <div class="login-container flex flex-col justify-center items-center min-h-screen":style="{ backgroundImage: 'url(' + backgroundImage + ')', backgroundSize: 'cover', backgroundPosition: 'center' }">
        <h2 class="text-2xl font-bold mb-4">請先登入以使用聊天機器人</h2>
        <div class="login-box p-8 bg-white rounded-lg shadow-lg w-96">
            <h3 class="text-xl font-semibold mb-4">登入</h3>
            <input v-model="username" placeholder="用戶名" class="p-2 border rounded-lg w-full mb-4" />
            <input v-model="password" type="password" placeholder="密碼" class="p-2 border rounded-lg w-full mb-4" />
            <button @click="login" class="bg-blue-500 text-white rounded-lg py-2 px-4 w-full hover:bg-blue-600 transition duration-300">
                登入
            </button>
            <p v-if="error" class="text-red-500 mt-4">{{ error }}</p>

            <div class="flex justify-center mt-4">
                <button @click="goToRegister" class="text-blue-500 hover:underline">
                    沒有帳戶？前往註冊
                </button>
            </div>
        </div>
    </div>
</template>

<script setup>
    import { ref } from 'vue';
    import { useRouter } from 'vue-router';
    import axios from 'axios';
    import backgroundImage from '@/assets/background.gif';
    import { useUserStore } from '@/stores/user';

    const username = ref('');
    const password = ref('');
    const error = ref('');
    const router = useRouter();
    const userStore = useUserStore();

    const login = async () => {
        if (!username.value || !password.value) {
            error.value = '請輸入用戶名和密碼！';
            return;
        }

        try {
            const response = await axios.post('http://localhost:5006/api/DapperMember/login', {
                username: username.value,
                password: password.value,
                email: ''
            });

            if (response.data.success) {

                const defaultLoginPayload = {
                    username: username.value,
                    password: password.value,
                };

                userStore.login(defaultLoginPayload);

                //localStorage.setItem('token', response.data.token);
                router.push('/');
            } else {
                error.value = '帳號密碼錯誤';
            }
        } catch (err) {
            error.value = err.response ? err.response.data.message : '伺服器無回應，請稍後再試。';
        }
    };

    const goToRegister = () => {
        router.push('/register');
    };
</script>
