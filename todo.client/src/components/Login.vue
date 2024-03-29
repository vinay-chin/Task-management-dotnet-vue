<template>
    <div class="bg-gray-900 min-h-screen flex items-center justify-center">
      <div class="bg-gray-800 rounded-lg p-8 w-1/4">
        <h2 class="text-3xl font-bold text-white mb-8 text-center">Login</h2>
        <form @submit.prevent="handleLogin">
          <div class="flex flex-col mb-4">
            <label for="username" class="text-lg font-bold text-white mb-2">username</label>
            <input v-model="username" type="username" id="username" name="username" class="bg-gray-700 text-white border border-gray-600 p-2 rounded" required>
          </div>
          <div class="flex flex-col mb-4">
            <label for="password" class="text-lg font-bold text-white mb-2">Password</label>
            <input v-model="password" type="password" id="password" name="password" class="bg-gray-700 text-white border border-gray-600 p-2 rounded" required>
          </div>
          <div class="flex justify-center">
            <button type="submit" class="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline">Login</button>
          </div>
        </form>
      </div>
    </div>
  </template>
  
<script setup>
import { ref } from 'vue';
import { useRouter } from 'vue-router';

const router = useRouter();
const username = ref('');
const password = ref('');

const handleLogin = async () => {
    localStorage.setItem('username', username.value);
    router.push('/'); // navigate to home page or wherever you want after login
};
</script>
