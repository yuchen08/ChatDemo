<template>
    <div class="register-page flex justify-center items-center min-h-screen bg-gray-100":style="{ backgroundImage: 'url(' + backgroundImage + ')', backgroundSize: 'cover', backgroundPosition: 'center' }">
        <div class="w-full max-w-md bg-white p-8 rounded-lg shadow-lg">
            <h2 class="text-2xl font-semibold text-center text-primary mb-6">註冊帳戶</h2>
            <form @submit.prevent="registerUser">
                <div class="mb-4">
                    <label for="username" class="block text-sm font-medium text-gray-700">用戶名</label>
                    <input v-model="username" id="username" type="text" required
                           class="mt-1 block w-full p-2 border border-gray-300 rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500" />
                </div>

                <div class="mb-4">
                    <label for="email" class="block text-sm font-medium text-gray-700">電子郵件</label>
                    <input v-model="email" id="email" type="email" required
                           class="mt-1 block w-full p-2 border border-gray-300 rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500" />
                </div>

                <div class="mb-4">
                    <label for="password" class="block text-sm font-medium text-gray-700">密碼</label>
                    <input v-model="password" id="password" type="password" required
                           class="mt-1 block w-full p-2 border border-gray-300 rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500" />
                </div>

                <div class="flex justify-center">
                    <button type="submit" class="bg-blue-500 text-white rounded-lg py-2 px-4 hover:bg-blue-600 focus:outline-none focus:ring-2 focus:ring-blue-500">
                        註冊
                    </button>
                </div>
            </form>
        </div>
    </div>
</template>

<script setup>
import { ref } from 'vue';
import axios from 'axios';
import { useRouter } from 'vue-router';
    import backgroundImage from '@/assets/background.gif';

const username = ref('');
const email = ref('');
const password = ref('');
const router = useRouter();

// 註冊用戶函式
const registerUser = async () => {
  try {
      const response = await axios.post('http://localhost:5006/api/DapperMember/register', {
      Username: username.value,
      Email: email.value,
      Password: password.value
    });
    alert('註冊成功，請前往登入頁面');
    router.push('/login'); // 跳轉到登入頁面
  } catch (error) {
    console.error(error);
    alert('註冊失敗，請稍後再試');
  }
};
</script>

<style scoped>
    /* 可以加一些 Tailwind CSS 配置來美化頁面 */
</style>
