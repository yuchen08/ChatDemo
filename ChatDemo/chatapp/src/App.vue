<template>
    <div id="app">
        <ChatApp /><!-- 引入您的 ChatApp 組件 -->
    </div>
</template>

<script>
    import ChatApp from './components/ChatApp.vue'
    //import HelloWorld from './components/HelloWorld.vue'

export default {
  name: 'App',
  components: {
      ChatApp //註冊 ChatApp 組件
  }
}
</script>
