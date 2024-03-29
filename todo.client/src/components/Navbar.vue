<template>
    <nav class="bg-gray-800 py-4 px-8">
      <div class="container mx-auto flex items-center justify-between">
        <div class="flex items-center">
          <a href="#" class="text-white font-bold text-xl">Task Management</a>
        </div>
        <div class="hidden md:block">
          <div class="flex items-center space-x-4">
            <router-link to="/" class="text-white hover:text-gray-300">Home</router-link>
            <router-link to="/login" class="text-white hover:text-gray-300">Login</router-link>
            <router-link to="/signup" class="text-white hover:text-gray-300">Signup</router-link>
          </div>
        </div>
        <div class="md:hidden">
          <button class="text-white focus:outline-none" @click="toggleMenu">
            <svg class="h-6 w-6" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M4 6h16M4 12h16M4 18h16"></path></svg>
          </button>
        </div>
        <div :class="{ 'hidden': !isMenuOpen, 'block': isMenuOpen }" class="dropdown-menu md:hidden">
          <router-link to="/" class="block text-white hover:text-gray-300 py-2 px-4">Home</router-link>
          <router-link to="/login" class="block text-white hover:text-gray-300 py-2 px-4">About</router-link>
          <router-link to="/signup" class="block text-white hover:text-gray-300 py-2 px-4">Contact</router-link>
        </div>
      </div>
    </nav>
  </template>
  
  <script>
  export default {
    data() {
      return {
        isMenuOpen: false
      };
    },
    methods: {
      toggleMenu() {
        this.isMenuOpen = !this.isMenuOpen;
      }
    }
  };
  </script>