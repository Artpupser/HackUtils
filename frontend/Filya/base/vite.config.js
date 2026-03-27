import { defineConfig } from 'vite'

export default defineConfig({
    css: {
        preprocessorOptions: {
            scss: {
                additionalData: `@import "bootstrap/scss/bootstrap";`
            }
        }
    },
    server: {
        host: "localhost",
        port: 50001,
        open: true,
    },
    build: {
        outDir: 'dist'
    }
})