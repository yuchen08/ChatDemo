<template>
    <div class="chat-app">
        <h1>聊天機器人測試 Demo</h1>

        <!-- 個人資料顯示 -->
        <div class="profile-section">
            <h2>個人資料</h2>
            <p><strong>姓名：</strong>{{ name || '尚未設定' }}</p>
            <p><strong>電子郵件：</strong>{{ email || '尚未設定' }}</p>
            <p><strong>網頁資訊：</strong>{{ bio || '尚未設定' }}</p>
        </div>

        <!-- 聊天區域 -->
        <div class="chat-section">
            <h2>提問區</h2>
            <input v-model="question"
                   placeholder="請輸入您的問題"
                   @keyup.enter="askQuestion"
                   class="question-input" />
            <button @click="askQuestion">提交問題</button>
        </div>

        <!-- 回答和錯誤顯示 -->
        <div v-if="answer" class="response">
            <p><strong>回答：</strong>{{ answer }}</p>
        </div>
        <div v-if="error" class="error">
            <p><strong>錯誤：</strong>{{ error }}</p>
        </div>
    </div>
</template>

<script>
    import axios from 'axios';

    export default {
        name: 'ChatApp',
        data() {
            return {
                name: '許浴宸',  // 預設的姓名
                email: 'hsuyuchen08@gmail.com',  // 預設的電子郵件
                bio: 'Vue3+.Net Web API練習網頁。',  // 預設的自我介紹
                question: '',  // 用戶輸入的問題
                answer: '',    // 服務器返回的答案
                error: null    // 錯誤信息
            };
        },
        methods: {
            async askQuestion() {
                if (!this.question.trim()) {
                    this.error = '問題不能為空！';
                    return;
                }

                this.error = null;

                try {
                    const response = await axios.get(`http://localhost:5006/api/ollama/${this.question}`);
                    this.answer = response.data; // 更新答案
                } catch (err) {
                    this.error = '無法獲得答案，請稍後再試。' + err;
                }
            }
        }
    };
</script>

<style scoped>
    .chat-app {
        max-width: 600px;
        margin: auto;
        padding: 1em;
        font-family: Arial, sans-serif;
        color: #333;
    }

    h1, h2 {
        text-align: center;
        color: #00509e;
    }

    .profile-section, .chat-section {
        margin-bottom: 1.5em;
        padding: 1em;
        border: 1px solid #ccc;
        border-radius: 8px;
        background-color: #f9f9f9;
    }

        .profile-section h2, .chat-section h2 {
            color: #0074d9;
            font-size: 1.2em;
            margin-bottom: 0.5em;
        }

        .profile-section p {
            font-size: 1em;
            margin: 0.5em 0;
        }

    .question-input {
        width: 100%;
        font-size: 1.1em;
        padding: 0.5em;
        margin: 0.5em 0;
        border: 1px solid #bbb;
        border-radius: 4px;
    }

    button {
        background-color: #0074d9;
        color: white;
        padding: 0.7em 1.5em;
        border: none;
        border-radius: 4px;
        font-size: 1em;
        cursor: pointer;
        transition: background-color 0.3s;
        width: 100%;
    }

        button:hover {
            background-color: #00509e;
        }

    .response, .error {
        margin-top: 1em;
        padding: 0.8em;
        border-radius: 4px;
    }

    .response {
        background-color: #e6f7ff;
        color: #0074d9;
    }

    .error {
        background-color: #ffe6e6;
        color: #d90000;
    }

    /* RWD */
    @media (max-width: 600px) {
        .chat-app {
            padding: 0.5em;
        }

        button {
            padding: 0.5em 1em;
            font-size: 0.9em;
        }

        .profile-section p {
            font-size: 0.9em;
        }
    }
</style>
