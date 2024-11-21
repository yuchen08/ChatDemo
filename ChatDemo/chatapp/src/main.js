import { createApp } from 'vue';
import App from './App.vue';
import axios from 'axios';
import './index.css';//TailWind

// �Ы� Vue ��Ҩó]�m���� axios �t�m
const app = createApp(App);

// �]�m axios �����t�m�A�Ҧp�]�m baseURL �M headers
axios.defaults.baseURL = 'http://localhost:5006'; // �]�m��� API �� URL
axios.defaults.headers.common['Content-Type'] = 'application/json'; // �]�m Content-Type

//// �]�m��L�����t�m�]�p�y���A��x���^
//app.config.globalProperties.$axios = axios;

app.mount('#app');