import React from 'react';
import './App.css';

const App = ({ initialData }) => {
    return (
        <div>
            <h2>Interfaz de React Cargada Exitosamente</h2>
            <p>A continuación se muestran los datos iniciales recibidos desde el backend de Blazor:</p>
            <pre style={{ background: '#f4f4f4', padding: '10px', border: '1px solid #ddd' }}>
                {JSON.stringify(initialData, null, 2)}
            </pre>
        </div>
    );
};

export default App;