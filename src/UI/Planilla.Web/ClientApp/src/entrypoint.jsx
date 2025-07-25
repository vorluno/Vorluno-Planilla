import React from 'react';
import ReactDOM from 'react-dom/client';
import App from './App';
import './index.css';

window.renderReactApp = (containerId, initialData, dotNetHelper) => {
    const container = document.getElementById(containerId);

    if (container) {
        const root = ReactDOM.createRoot(container);
        root.render(
            <React.StrictMode>
                <App initialData={initialData} dotNetHelper={dotNetHelper} />
            </React.StrictMode>
        );
    } else {
        console.error(`Contenedor no encontrado: #${containerId}`);
    }
};