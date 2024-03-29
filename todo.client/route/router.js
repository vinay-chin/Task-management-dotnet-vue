import { createWebHashHistory, createRouter } from 'vue-router'

import Todo from '../src/components/Todo.vue'
import Signup from '../src/components/Signup.vue'
import Login from '../src/components/Login.vue'

const routes = [
  { path: '', component: Todo },
  { path: '/login', component: Login },
  { path: '/signup', component: Signup },
]

const router = createRouter({
  history: createWebHashHistory(),
  routes,
})

export default router