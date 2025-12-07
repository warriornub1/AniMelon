import React, { useState } from 'react'
import { useGlobalContext } from "../context/Global";
import Popular from './Popular';

const Homepage = () => {

  const {handleSubmit, search, searchAnime, handleChange} = useGlobalContext()

  const [rendered, setRendered] = useState('popular');

  const switchComponent = () => {
    switch(rendered){
        case 'popular':
            return <Popular rendered={rendered} />
        
        default:
            return <Popular rendered={rendered} />
    }
  }

  return (
    <div>
        <header>
            <div className="logo">
                <h1>
                    {rendered === 'popular' ? 'Popular Anime' : 
                    rnedered === 'airing' ? 'Airing Anime' : 'Upcoming Anime'}
                </h1>
            </div>
            <div className="search-container">
              <div className="filter-btn popular-filter">
                <button onClick={() => setRendered('popular')}>Popular</button>
              </div>
              <form action="" className="search-form">
                <div className="input-control">
                  <input type="text" placeholder="Search Anime" value={search} onChange={searchAnime}/>
                </div>
              </form>
            </div>
        </header>
    </div>
  )
}

export default Homepage