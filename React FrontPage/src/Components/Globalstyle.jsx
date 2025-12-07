import {createGlobalStyle} from 'styled-components';

const GlobalStyle = createGlobalStyle`
@import url('https://fonts.googleapis.com/css2?family=Nunito:ital,wght@0,200..1000;1,200..1000&family=Roboto:ital,wght@0,100..900;1,100..900&display=swap');
    *{
        margin: 0;
        padding: 0;
        box-sizing: border-box;
        list-style: none;
        font-family: 'ÍNTER', sans-serif;
    }

    body{
        background-color: #6c7983;
        font-size: 1.2rem;
    }
`;

export default GlobalStyle;