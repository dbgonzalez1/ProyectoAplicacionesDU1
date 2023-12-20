import {Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Typography} from "@mui/material";
import {useState} from "react";
import {LoadingButton} from "@mui/lab";
import Button from '@mui/material/Button';
import {API} from "../api/API.ts";
import {useSnackbar} from "notistack";
import axios from "axios";
import {Delete, Update} from "@mui/icons-material";
import EntityData, {InputField} from "../models/EntityData.ts";


export interface ModelProps {
	entityData: EntityData
	entity: string;
	updateTableData: () => void;
	tableData: object[];
	modelIdToEdit: string | undefined,
	showIds?: boolean;
	setModelIdToEdit: (model: string | undefined) => void;
}

export default function TableEntity(
	{
		entityData,
		entity,
		tableData,
		updateTableData,
		setModelIdToEdit,
		showIds = true,
		modelIdToEdit
	}: ModelProps) {
	
	const [loading, setLoading] = useState<boolean>(false)
	const {enqueueSnackbar} = useSnackbar()
	
	const deleteEntity = (id: string) => {
		
		console.log("deleting model", id)
		
		axios
		.delete(`${API.url}/${entity}/${id}`, {headers: API.getHeaderAuthorization()})
		.then(() => {
			enqueueSnackbar("Eliminado con exito", {variant: 'success'})
			updateTableData()
		})
		.catch(error => {
			if (error.response) {
				enqueueSnackbar(error.response.data.message, {variant: 'error'})
			} else {
				enqueueSnackbar("Error al procesar la solicitud", {variant: 'error'})
			}
		})
		.finally(() => setLoading(false))
	}
	
	if (tableData.length === 0) return <Typography variant="h6">No hay registros para mostar</Typography>
	
	const showField: InputField[] = entityData.inputs.filter((field) => field.showInTable)
	
	if (showIds) {
		showField.unshift(
			{
				name: "id",
				value: "",
				type: "text",
				label: "Id",
				placeholder: "Id",
				showInTable: true,
				showInForm: true
			}
		)
	}
	
	return (
		
		<TableContainer component={Paper}>
			<Table>
				<TableHead>
					<TableRow>
						{showField.map((field) => <TableCell key={field.name}>{field.label}</TableCell>)}
						<TableCell align="right">
							<Typography fontWeight="bold">Acciones</Typography>
						</TableCell>
					</TableRow>
				</TableHead>
				
				<TableBody>
					{tableData.map((model) => {
						
						console.log(model)
						
						return <TableRow key={model["id"]}>
							
							{showField.map((field) => {
								return (
									<TableCell key={model["id"] + field.name}>
										{
											getFieldValue(model, field)
										}
									</TableCell>
								);
							})}
							
							<TableCell sx={{display: "flex", flexDirection: "row", justifyContent: "center"}}>
								<LoadingButton
									loading={loading}
									onClick={() => deleteEntity(model["id"])}
									variant="outlined"
									startIcon={<Delete/>}
									color={"error"}
									disabled={!!modelIdToEdit}
								>
									Eliminar
								</LoadingButton>
								
								<Button
									variant="outlined"
									startIcon={<Update/>}
									onClick={() => setModelIdToEdit(model["id"])}
									disabled={!!modelIdToEdit}
									sx={{mx: 2}}
								>
									Editar
								</Button>
							</TableCell>
						</TableRow>
					})}
				
				</TableBody>
			
			</Table>
		</TableContainer>
	)
	
	function getFieldValue(model: object, field: InputField): string {
		
		if (field.name.includes(".")) {
			
			const split = field.name.split(".")
			for (let i = 0; i < split.length; i++) {
				model = model[split[i]]
			}
			
			return model as unknown as string
			
		}
		
		return model[field.name]
	}
	
}