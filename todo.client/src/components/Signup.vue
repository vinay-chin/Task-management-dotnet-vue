<template>
    <div class="bg-gray-900 min-h-screen flex items-center justify-center">
      <div class="bg-gray-800 w-1/4 rounded-lg p-8">
        <h2 class="text-3xl font-bold text-white mb-8 text-center">Sign Up</h2>
        <form @submit.prevent="handleSignup">
          <div class="flex flex-col mb-4">
            <label for="name" class="text-lg font-bold text-white mb-2">Name</label>
            <input v-model="user.username" type="text" id="name" name="name" class="bg-gray-700 text-white border border-gray-600 p-2 rounded" required>
          </div>
          <div class="flex flex-col mb-4">
            <label for="password" class="text-lg font-bold text-white mb-2">Password</label>
            <input v-model="user.password" type="password" id="password" name="password" class="bg-gray-700 text-white border border-gray-600 p-2 rounded" required>
          </div>
          <div class="flex flex-col mb-4">
            <label for="password" class="text-lg font-bold text-white mb-2">Re-Type Password</label>
            <input type="password" id="" name="" class="bg-gray-700 text-white border border-gray-600 p-2 rounded" required>
          </div>
          <div class="flex justify-center">
            <button type="submit" class="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline">Sign Up</button>
          </div>
        </form>
      </div>
    </div>
  </template>
  
  <script setup>
import { ref, onMounted } from 'vue';

const user = ref({
  username: '',
  password: '',
});

const tasks = ref([]);
import { useRouter } from 'vue-router';

const router = useRouter();
async function handleSignup() {
  const response = await fetch('http://localhost:5078/signup', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(user.value)
  });

  if (!response.ok) {
    throw new Error(`HTTP error! status: ${response.status}`);
  }
router.push('/login').catch(() => {});
}

  </script>