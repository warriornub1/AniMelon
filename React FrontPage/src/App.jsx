import AnimeItem from './Components/AnimeItem';
import Homepage from './Components/Homepage';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import Popular from './Components/Popular';

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Popular />}/>
        <Route path="/Homepage" element={<Homepage />}/>

        <Route path="/anime/:id" element={<AnimeItem />} />
      </Routes>
    </BrowserRouter>
  )
}

export default App
