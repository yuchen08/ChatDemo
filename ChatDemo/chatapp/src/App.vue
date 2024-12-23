<template>
    <div id="app">
        <router-view />
        <ChatApp />
    </div>
</template>

<script>
    import ChatApp from './components/ChatApp.vue'
    import Login from './components/Login.vue'
    //import HelloWorld from './components/HelloWorld.vue'

export default {
        name: 'App',
        components: {
            ChatApp,
            Login
        }
}
</script>
