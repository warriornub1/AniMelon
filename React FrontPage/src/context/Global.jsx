import { createContext, useContext, useEffect, useReducer, useState } from "react";

export const GlobalContext = createContext();

const baseUrl = "https://api.jikan.moe/v4";

//actions
const LOADING = "LOADING";
const SEARCH = "SEARCH";
const GET_POPULAR_ANIME = "GET_POPULAR_ANIME";
const GET_UPCOMING_ANIME = "GET_UPCOMING_ANIME";
const GET_AIRING_ANIME = "GET_AIRING_ANIME";

//reducer
const reducer = (state, action) => {

    switch(action.type){
        case LOADING:
            return {...state, loading: true}
        case GET_POPULAR_ANIME:
            return {...state, popularAnime: action.payload, loading: false}
        case SEARCH:
            return {...state, searchResults: action.payload, loading: false}
        default:
            return state;
    }
}

export default function GlobalContextProvider({children}) {

    // intial state
    const intialState = {
        popularAnime: [],
        upcomingAnime: [],
        airingAnime: [],
        pictures: [],
        isSearch: false,
        searchResults: [],
        loading: false,
    }

    const [state, dispatch] = useReducer(reducer, intialState);
    const [search, setSearch] = useState('');


    const handleChange = (e) => {
        setSearch(e.target.value);
        if(e.target.value === ''){
            state.isSearch = false;
        }
    }

    const handleSubmit = (e) => {
        e.preventDefault();
        if(search){
            searchAnime(search);
            state.isSearch = true;
        }
        else{
            state.isSearch = false;
            alert('Please enter a search term')
        }
    }

    // fetch popular anime
    const getPopularAnime = async () => {
        dispatch({type: LOADING});
        const response = await fetch(`${baseUrl}/top/anime?filter=bypopularity`);
        const data = await response.json();
        dispatch({type: GET_POPULAR_ANIME, payload: data.data})
    }

    //search anime
    const searchAnime = async(anume) => {
        dispatch({type: LOADING})
        const response = await fetch(`${baseUrl}/search/anime?q=${anime}&order_by=popularity&sort=asc&sfw`);
        const data = await response.json();
        dispatch({type: SEARCH, payload: data.data});
    }

    //initial render
    useEffect(() => {
        getPopularAnime();
    }, [])

    return(
        <GlobalContext.Provider value={{
            ...state,
            handleChange,
            handleSubmit,
            searchAnime,
            search,
        }}>
            {children}
        </GlobalContext.Provider>
    )
}

export const useGlobalContext = () => {
    return useContext(GlobalContext);
}