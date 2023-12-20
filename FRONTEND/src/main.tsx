import ReactDOM from 'react-dom/client'


import {createBrowserRouter, RouterProvider} from "react-router-dom";
import Index from "./pages/Index.tsx";
import {createTheme, ThemeProvider} from "@mui/material";

import '@fontsource/roboto/300.css';
import '@fontsource/roboto/400.css';
import '@fontsource/roboto/500.css';
import '@fontsource/roboto/700.css';

import Login from "./pages/Login.tsx";
import {SnackbarProvider} from "notistack";
import UserContextProvider from "./context/UserContextProvider.tsx";
import EntityPage from "./pages/EntityPage.tsx";

const router = createBrowserRouter([
    {
        path: '/',
        element: <Index/>
    },
    {
        path: '/login',
        element: <Login/>
    },
    {
        path: '/entity/:entity',
        element: <EntityPage/>
    }
])


const theme = createTheme({
    palette: {
        primary: {
            main: '#1f1b5e',
        },
        background: {
            default: '#f1f4ff'
        }
    }
})


ReactDOM.createRoot(document.getElementById('root')!).render(
    <ThemeProvider theme={theme}>
        <SnackbarProvider maxSnack={4} autoHideDuration={4000}>
            <UserContextProvider>
                <RouterProvider router={router}/>
            </UserContextProvider>
        </SnackbarProvider>
    </ThemeProvider>
)
