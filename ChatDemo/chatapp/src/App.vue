<template>
    <!--
      App.vue 為應用程式的根元件，僅負責渲染 <router-view />
      讓 Vue Router 根據路由顯示對應頁面元件
    -->
    <div id="app">
        <router-view />
    </div>
</template>

<script setup>
// 此處無需 import 其他元件，僅需 <router-view />
// <script setup> 語法糖讓程式更簡潔
</script>
