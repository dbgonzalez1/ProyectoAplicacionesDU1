import {Button, Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle} from "@mui/material";

export interface MyDialogProps {
    open: boolean;
    question: string;
    dialog?: string;
    onAgree: () => void;
    onDisagree: () => void;
}

export default function MyDialog({question, onAgree, onDisagree, open, dialog}: MyDialogProps) {
    
    return <Dialog
        open={open}
        //onClose={handleClose}
        aria-labelledby="alert-dialog-title"
        aria-describedby="alert-dialog-description"
    >
        <DialogTitle id="alert-dialog-title">
            {question}
        </DialogTitle>
        <DialogContent>
            <DialogContentText id="alert-dialog-description">
                {dialog}
            </DialogContentText>
        </DialogContent>
        <DialogActions>
            <Button onClick={onDisagree}>Rechazar</Button>
            <Button onClick={onAgree} autoFocus>
                Aceptar
            </Button>
        </DialogActions>
    </Dialog>
}