import { BrowserRouter } from 'react-router-dom';
import * as ReactDOMClient from 'react-dom/client';
import './index.css';
import App from './App';
import React from 'react';
import { AuthContextProvider } from './store/auth-context';

const root = ReactDOMClient.createRoot(
  document.getElementById('root')
);

root.render(
  <AuthContextProvider>
  <BrowserRouter>
  <App />
</BrowserRouter>
</AuthContextProvider>,
);
