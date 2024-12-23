<template>
    <div class="login-container flex justify-center items-center h-screen">
        <div class="login-box p-8 bg-white rounded-lg shadow-lg w-96">
            <h2 class="text-2xl font-semibold text-primary mb-4">會員登入</h2>
            <input v-model="username" placeholder="用戶名" class="p-2 border rounded-lg w-full mb-4" />
            <input v-model="password" type="password" placeholder="密碼" class="p-2 border rounded-lg w-full mb-4" />
            <button @click="login" class="bg-blue-500 text-white rounded-lg py-2 px-4 w-full hover:bg-blue-600 transition duration-300 transform hover:scale-105 active:scale-95 shadow-lg mt-4">
                登入
            </button>
            <p v-if="error" class="text-red-500 mt-4">{{ error }}</p>
        </div>
    </div>
</template>

<script setup>
    import { ref } from 'vue';
    import { useRouter } from 'vue-router';
    import axios from 'axios';

    const username = ref('');
    const password = ref('');
    const error = ref('');
    const router = useRouter();

    const login = async () => {
        if (!username.value || !password.value) {
            error.value = '請輸入用戶名和密碼！';
            return;
        }

        try {
            const response = await axios.post('http://localhost:5006/api/login', { username: username.value, password: password.value });
            if (response.data.success) {
                localStorage.setItem('token', response.data.token);  // 儲存登入令牌
                router.push('/chat');  // 登入成功後跳轉到聊天頁面
            } else {
                error.value = '登入失敗，請檢查帳號或密碼。';
            }
        } catch (err) {
            error.value = '登入錯誤，請稍後再試。';
        }
    };
</script>
