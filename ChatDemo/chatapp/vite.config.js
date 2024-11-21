import { defineConfig } from 'vite';
import vue from '@vitejs/plugin-vue';
import path from 'path';

// https://vitejs.dev/config/
export default defineConfig({
    plugins: [vue()],
    resolve: {
        alias: {
            '@': path.resolve(__dirname, 'src'), // 設置別名
        },
    },
    server: {
        proxy: {
            '/api': 'http://localhost:5006', // 設置 API 代理
        },
    },
});
