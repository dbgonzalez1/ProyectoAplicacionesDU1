import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

// https://vitejs.dev/config/
/*
Specifically the "@esbuild/win32-x64" package is present but this platform
 needs the "@esbuild/linux-x64"
* */

export default defineConfig({
  plugins: [react()],
})
