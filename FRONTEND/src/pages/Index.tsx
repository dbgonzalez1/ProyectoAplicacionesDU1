import Layout from "../layout/Layout.tsx";
import {Box, Typography} from "@mui/material";
import background from "../../public/BACKGROUND.png";

function Index() {
    return (
        <Layout>
            <Typography variant="h2" fontWeight="bold">
                SISTEMA DE ENSAMBLADO DE AUTOS
            </Typography>
            <Typography variant="h4">
                Bienvenido
            </Typography>
            <Box sx={{
                display: "grid",
                placeItems: "center",
            
            }}>
                <img src={background} alt="background"/>
            </Box>
        </Layout>
    )
}

export default Index;