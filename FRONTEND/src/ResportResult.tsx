import {Box, Button, LinearProgress} from "@mui/material";
import {Close, Download} from "@mui/icons-material";
import {Fragment, useEffect, useState} from "react";
import {useSnackbar} from "notistack";
import ReportFiles from "../models/ReportFiles.ts";
import {API} from "../api/API.ts";

interface ReportResultProps {
    reportFile: ReportFiles,
    close: () => void
}

function ReportResult({reportFile, close}: ReportResultProps) {
    
    const [pdfUrl, setPdfUrl] = useState<string>();
    const [excelUrl, setExcelUrl] = useState<string>();
    
    const {enqueueSnackbar} = useSnackbar()
    
    useEffect(() => {
        
        fetch(`${API.url}/download/${reportFile.idFilePDF}`)
            .then(response => {
                if (response.ok) {
                    response
                        .blob()
                        .then(blob => setPdfUrl(window.URL.createObjectURL(blob)))
                        .catch(error => enqueueSnackbar(error, {variant: 'error'}))
                    
                } else {
                    response
                        .json()
                        .then(error => enqueueSnackbar(error.message, {variant: 'error'}))
                }
            }).catch(error => enqueueSnackbar(error, {variant: 'error'}));
        
        
        fetch(`${API.url}/download/${reportFile.idFileExcel}`)
            .then(response => {
                if (response.ok) {
                    response
                        .blob()
                        .then(blob => setExcelUrl(window.URL.createObjectURL(blob)))
                        .catch(error => enqueueSnackbar(error, {variant: 'error'}))
                    
                } else {
                    response
                        .json()
                        .then(error => enqueueSnackbar(error.message, {variant: 'error'}))
                }
            }).catch(error => enqueueSnackbar(error, {variant: 'error'}));
        
        // eslint-disable-next-line
    }, []);
    
    if (!pdfUrl || !excelUrl) return <LinearProgress/>
    
    return (
        <Fragment>
            
            <Box display="flex">
                
                <Button
                    startIcon={<Close/>}
                    onClick={close}
                >
                    Cerrar
                </Button>
                
                <Button
                    startIcon={<Download/>}
                    href={pdfUrl}
                    download="resumen.pdf"
                >
                    Descargar PDF
                </Button>
                
                <Button
                    startIcon={<Download/>}
                    href={excelUrl}
                    download="resumen.xlsx"
                >
                    Descargar Excel Detallado
                </Button>
            
            </Box>
            
            <embed
                src={pdfUrl}
                type="application/pdf"
                style={{
                    width: "100%",
                    height: "75vh",
                    overflow: "hidden",
                    zoom: "100%"
                }}
            />
        
        </Fragment>
    )
}

export default ReportResult;