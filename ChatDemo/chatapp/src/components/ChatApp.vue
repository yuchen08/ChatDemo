<template>
    <!-- 檢查登入狀態，若已登入則顯示聊天頁面 -->
    <div v-if="isLoggedIn" class="chat-app min-h-screen p-8 flex flex-col items-center justify-center"
         :style="{ backgroundImage: 'url(' + backgroundImage + ')', backgroundSize: 'cover', backgroundPosition: 'center' }">

        <!-- 頁面標題與Logo -->
        <div class="flex items-center justify-center space-x-4 mb-6">
            <img src="@/assets/First.gif" alt="Logo" class="w-12 sm:w-14 md:w-16 lg:w-20 loading" />
            <h1 class="text-4xl font-semibold text-center text-primary">聊天機器人</h1>
        </div>

        <!-- 登出按鈕與歡迎字串 -->
        <div class="flex items-center space-x-4 mt-4">
            <span class="text-lg font-medium text-gray-700">會員您好！</span>
            <button @click="logout" class="bg-red-500 text-white rounded-lg py-2 px-4 hover:bg-red-600 transition duration-300 transform hover:scale-105 active:scale-95 shadow-lg">
                登出
            </button>
        </div>

        <!-- 提問區域 -->
        <div class="chat-section mt-8 p-6 bg-white rounded-lg shadow-lg w-full sm:max-w-md md:max-w-lg">
            <h2 class="text-xl font-semibold text-secondary mb-4">請輸入您的問題：</h2>
            <input v-model="question" @keyup.enter="askQuestion"
                   class="p-2 border rounded-lg w-full mb-4" />
            <button @click="askQuestion"
                    class="bg-blue-500 text-white rounded-lg py-2 px-4 w-full hover:bg-blue-600 transition duration-300 transform hover:scale-105 active:scale-95 shadow-lg mt-4">
                提交問題
            </button>
        </div>

        <!-- 加載動圖和提示文字 -->
        <div v-if="loading" class="mt-4 text-center flex flex-col items-center justify-center space-y-4">
            <p class="text-lg text-gray-700">稍等，正在為您產生答案...</p>
            <img src="@/assets/ollama.gif" alt="Ollama" class="mx-auto loading" />
        </div>

        <!-- 回答區域 -->
        <div v-if="answer" class="mt-6 p-4 bg-blue-100 text-primary rounded-lg shadow-lg w-full sm:max-w-md md:max-w-lg">
            <p><strong>回答：</strong>{{ answer }}</p>
        </div>

        <!-- 錯誤區域 -->
        <div v-if="error" class="mt-6 p-4 bg-red-100 text-red-500 rounded-lg shadow-lg w-full sm:max-w-md md:max-w-lg">
            <p><strong>錯誤：</strong>{{ error }}</p>
        </div>


        <!-- 個人資料區域 -->
        <div class="profile-section mt-8 p-6 bg-white rounded-lg shadow-lg w-full sm:max-w-md md:max-w-lg">
            <h2 class="text-xl font-semibold text-secondary mb-4">個人資料</h2>
            <hr class="border-t-2 border-gray-300 mb-4" />
            <div class="flex flex-col space-y-2 text-sm text-gray-700">
                <p><strong>作者姓名：許浴宸</strong></p>
                <p><strong>電子郵件：hsuyuchen08@gmail.com</strong></p>
                <p><strong>自我介紹：畢業於雲林科技大學，希望尋找程式設計相關研發替代役職缺</strong></p>
            </div>
        </div>
    </div>
</template>

<script setup>
    import { ref, onMounted } from 'vue';
    import { useRouter } from 'vue-router';
    import backgroundImage from '@/assets/background.gif';
    import axios from 'axios';
    import { useUserStore } from '@/stores/user';

    const isLoggedIn = ref(false);
    const question = ref('');
    const answer = ref('');
    const error = ref(null);
    const loading = ref(false);
    const router = useRouter();
    const userStore = useUserStore();

    // 檢查登入狀態
    onMounted(() => {
        if (userStore.loggedIn) {
            isLoggedIn.value = true;
        } else {
            router.push('/login'); // 未登入直接跳轉到登入頁面
        }
    });

    const logout = () => {
        userStore.logout();
        router.push('/login');
    };

    const askQuestion = async () => {
        if (!question.value.trim()) {
            error.value = '問題不能為空！';
            return;
        }
        error.value = null;
        loading.value = true;

        try {
            const response = await axios.get(`http://localhost:5006/api/ollama/question/${question.value}`);
            answer.value = response.data;
        } catch (err) {
            error.value = '無法獲得答案，請稍後再試。' + err;
        } finally {
            loading.value = false;
        }
    };
</script>
