import { createApp } from 'vue';
import App from './App.vue';
import axios from 'axios';
import './index.css';//TailWind

// 創建 Vue 實例並設置全局 axios 配置
const app = createApp(App);

// 設置 axios 全局配置，例如設置 baseURL 和 headers
axios.defaults.baseURL = 'http://localhost:5006'; // 設置後端 API 基本 URL
axios.defaults.headers.common['Content-Type'] = 'application/json'; // 設置 Content-Type

//// 設置其他全局配置（如語言，日誌等）
//app.config.globalProperties.$axios = axios;

app.mount('#app');