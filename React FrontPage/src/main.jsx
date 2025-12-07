import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import App from './App.jsx'
import GlobalStyle from './Components/Globalstyle.jsx'
import GlobalContextProvider from './context/Global.jsx'

createRoot(document.getElementById('root')).render(
  <StrictMode>
    <GlobalStyle />
    <GlobalContextProvider>
      <App />
    </GlobalContextProvider>
  </StrictMode>,
)
