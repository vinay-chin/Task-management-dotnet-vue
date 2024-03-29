<template>
  <div class="bg-gray-900 min-h-screen flex items-center justify-center">
    <div class="container mx-auto px-4">
      <h2 class="text-3xl font-bold text-white mb-8">Task Management Application (Epam Project)</h2>
      <h2 class="text-sm font-bold text-white mb-8">Hello {{ username }}</h2>
      <div class="bg-gray-800 rounded-lg p-8">
        <div class="flex flex-col mb-4">
          <label for="task-name" class="text-lg font-bold text-white mb-2">Task Name</label>
          <input type="text" id="task-name" name="task-name"
            class="bg-gray-700 text-white border border-gray-600 p-2 rounded" v-model="newTask.name" required>
        </div>
        <div class="flex flex-col mb-4">
          <label for="task-description" class="text-lg font-bold text-white mb-2">Task Description</label>
          <textarea id="task-description" name="task-description"
            class="bg-gray-700 text-white border border-gray-600 p-2 rounded h-40" v-model="newTask.description"
            required></textarea>
        </div>
        <div class="flex flex-col mb-4">
          <label for="task-complexity" class="text-lg font-bold text-white mb-2">Task Complexity</label>
          <select id="task-complexity" name="task-complexity"
            class="bg-gray-700 text-white border border-gray-600 p-2 rounded" v-model="newTask.complexity" required>
            <option value="simple">Simple</option>
            <option value="medium">Medium</option>
            <option value="complex">Complex</option>
          </select>
       </div>
        <div class="flex justify-center">
          <button @click="addTask"
            class="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline">Add
            Task</button>
        </div>
      </div>
      <div class="bg-gray-700 rounded-lg p-8 mt-8">
        <h3 class="text-lg font-bold text-white mb-4">Tasks</h3>
        <ul>
          <li v-for="(task, index) in tasks" :key="index" class="bg-gray-800 rounded-lg p-4 mb-2">
            <div class="flex justify-between">
              <div>
                <h4 class="text-white mb-2">{{ task.name }}</h4>
                <p class="text-white mb-2">{{ task.description }}</p>
              </div>
              <div>
                <span class="text-white">Complexity: {{ task.complexity }}</span>
                <button @click="removeTask(index)"
                  class="ml-4 text-white hover:text-gray-300 focus:outline-none">Remove</button>
              </div>
            </div>
          </li>
        </ul>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue';
const username = ref(localStorage.getItem('username'));
const newTask = ref({
  name: '',
  description: '',
  complexity: ''
});

const tasks = ref([]);

async function addTask() {
  const response = await fetch('http://localhost:5078/tasks', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(newTask.value)
  });

  if (!response.ok) {
    throw new Error(`HTTP error! status: ${response.status}`);
  }

  tasks.value.push(newTask.value);
  newTask.value = { name: '', description: '', complexity: '' };
}

function removeTask(index) {
  tasks.value.splice(index, 1);
}

async function getTasks() {
  const response = await fetch('http://localhost:5078/tasks/getall', {
    method: 'GET',
    headers: {
      'Content-Type': 'application/json'
    }
  });

  if (!response.ok) {
    throw new Error(`HTTP error! status: ${response.status}`);
  }

  const data = await response.json();
  tasks.value = data;
}

onMounted(getTasks);
</script>


