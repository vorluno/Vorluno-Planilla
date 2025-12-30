import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

// https://vitejs.dev/config/
export default defineConfig({
    plugins: [react()],
    base: '/react/', // Base URL para assets en wwwroot/react
    build: {
        // Generar los archivos en la carpeta wwwroot/react de nuestro proyecto Blazor
        outDir: '../wwwroot/react',

        // Asegurarse de que solo se limpie este subdirectorio, y no toda la carpeta wwwroot
        emptyOutDir: true,

        // Configuraci�n para generar nombres de archivo predecibles sin hashes aleatorios
        rollupOptions: {
            output: {
                entryFileNames: 'react-app.js',
                assetFileNames: 'react-app.[ext]'
            }
        }
    }
})