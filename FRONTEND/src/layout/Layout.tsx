import {CssBaseline} from "@mui/material";
import Navbar from "./Navbar";
import {PropsWithChildren} from "react";

function Layout({children}: PropsWithChildren) {
    return (
        <>
            <CssBaseline/>
            <Navbar>
                {children}
            </Navbar>
        </>
    
    )
    
}

export default Layout;