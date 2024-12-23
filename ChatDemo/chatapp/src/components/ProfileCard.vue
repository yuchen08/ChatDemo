<template>
    <div class="profile-section p-6 bg-white rounded-lg shadow-lg">
        <h2 class="text-xl font-semibold text-secondary mb-4">個人資料</h2>
        <hr class="border-t-2 border-gray-300 mb-4" />
        <div class="flex flex-col space-y-2 text-sm text-gray-700">
            <p><strong>姓名：</strong>{{ name }}</p>
            <p><strong>電子郵件：</strong>{{ email }}</p>
            <p><strong>自我介紹：</strong>{{ bio }}</p>
        </div>
    </div>
</template>

<script setup>
    defineProps({
        name: String,
        email: String,
        bio: String
    });
</script>
