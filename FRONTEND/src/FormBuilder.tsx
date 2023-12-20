import {FormEvent, ReactNode, useEffect, useState} from "react";
import EntityData, {InputField, SelectField} from "../models/EntityData.ts";
import axios from "axios";
import {API} from "../api/API.ts";
import {useSnackbar} from "notistack";
import {Box, Button, FormControl, Grid, InputLabel, MenuItem, Select, SelectChangeEvent, TextField, Typography} from "@mui/material";
import {LoadingButton} from "@mui/lab";
import {Save, KeyboardReturn} from "@mui/icons-material";

interface FormBuilderProps {
	entityData: EntityData,
	entity: string,
	updateTableData: () => void,
	idToEdit?: string;
	setModelIdToEdit?: (model: string | undefined) => void;
}

export default function FormBuilder(
	{
		entityData,
		entity,
		updateTableData,
		idToEdit,
		setModelIdToEdit
	}
		: FormBuilderProps
) {
	
	const [loading, setLoading] = useState(false);
	const [modelToEdit, setModelToEdit] = useState<object | null>(null);
	const {enqueueSnackbar} = useSnackbar();
	
	const handleSubmit = (event: FormEvent<HTMLFormElement>) => {
		
		event.preventDefault();
		setLoading(true);
		const formDataObject: object = {};
		new FormData(event.currentTarget).forEach((value, key) => formDataObject[key] = value);
		console.log(formDataObject);
		if (modelToEdit) {
			updateEnt(formDataObject);
		} else {
			updateEntity(formDataObject);
		}
	}
	
	const updateEntity = (data: object) => {
		axios
		.post(`${API.url}/${entity}/`, data, {headers: API.getHeaderAuthorization()})
		.then(() => {
			enqueueSnackbar("Creado con exito", {variant: "success"});
			updateTableData();
		})
		.catch(onError)
		.finally(() => {
			setLoading(false)
		})
	}
	
	const updateEnt = (data: object) => {
		console.log("updating model", data);
		axios
		.put(`${API.url}/${entity}/${idToEdit}`, data, {headers: API.getHeaderAuthorization()})
		.then(() => {
			enqueueSnackbar("Actualizado con exito", {variant: "success"});
			updateTableData();
			setModelToEdit(null);
			if (idToEdit && setModelIdToEdit)
				setModelIdToEdit(undefined);
		})
		.catch(onError)
		.finally(() => {
			setLoading(false)
		})
	}
	
	// eslint-disable-next-line @typescript-eslint/no-explicit-any
	const onError = (error: any) => {
		
		if (Array.isArray(error.response.data)) {
			error.response.data.forEach((message: { message: string }) => {
				enqueueSnackbar(message.message, {variant: 'error'});
			});
		} else if (error.response) {
			enqueueSnackbar(error.response.data.message, {variant: 'error'});
		} else {
			enqueueSnackbar('Error en la solicitud', {variant: 'error'});
		}
		
	}
	
	const loadModelToEdit = () => {
		
		if (idToEdit && !modelToEdit) {
			axios
			.get(`${API.url}/${entity}/${idToEdit}`, {headers: API.getHeaderAuthorization()})
			.then(response => setModelToEdit(response.data))
			.catch(error => {
				if (error.response) {
					enqueueSnackbar(error.response.data.message, {variant: 'error'});
				} else {
					enqueueSnackbar('Error en la solicitud', {variant: 'error'});
				}
			});
		}
		
	}
	
	loadModelToEdit();
	
	
	if (!entityData) return <></>

	
	return (
		<Box component="form" onSubmit={handleSubmit}>
			
			<Typography variant="h3" fontWeight="bold">
				{entityData.entity}
			</Typography>
			
			<Grid container spacing={2}>
				{entityData.inputs.map(input => {
					
					if (!input.showInForm) return null;
					
					return (
						<GridItemResponsive key={input.name}>
							
							<InputComponent
								input={input}
								value_={modelToEdit ? modelToEdit[input.name] : ""}
							/>
						
						</GridItemResponsive>
					);
				})}
				
				{entityData.selects.map((select: SelectField) => {
					return (
						<GridItemResponsive key={select.name}>
							
							<SelectComponent
								select={select}
								value={modelToEdit ? modelToEdit[select.optionValue] : ""}
							/>
						
						</GridItemResponsive>
					);
				})}
			</Grid>

			<LoadingButton
				type="submit"
				variant="contained"
				sx={{mt: 3, mb: 2}}
				loading={loading}
				startIcon={<Save/>}

			>

				{modelToEdit ? "Actualizar" : "Crear"}

			</LoadingButton>
			
			{modelToEdit &&
                <Button
                    sx={{mt: 3, mb: 2, mx: 2}}
                    startIcon={<KeyboardReturn/>}
                    onClick={() => {
						setModelToEdit(null);
						if (idToEdit && setModelIdToEdit) setModelIdToEdit(undefined);
					}}
                >
                    Cancelar
                </Button>
			}
		
		</Box>
	)
}

export function GridItemResponsive({children}: { children: ReactNode }) {
	return (
		<Grid item xs={12} sm={6} md={4}>
			{children}
		</Grid>
	)
}

export function InputComponent({input, value_}: { input: InputField, value_: string }) {
	
	const [value, setValue] = useState<string>(value_ || "");
	
	useEffect(() => {
		setValue(value_ || "");
	}, [value_]);
	
	return (
		<TextField
			margin="normal"
			required
			fullWidth
			type={input.type}
			id={input.name}
			label={input.label}
			name={input.name}
			placeholder={input.placeholder}
			value={value}
			onChange={(event) => setValue(event.target.value)}
		/>
	)
}

export function SelectComponent({select, value}: { select: SelectField, value: string }) {
	
	const [values, setValues] = useState<object[] | string[] | null>(null);
	const [selectedValue, setSelectedValue] = useState(value || "");
	
	useEffect(() => {
		
		if (select.options.length > 0) {
			setValues(select.options);
			return;
		}
		axios
		.get(`${API.url}/${select.url}`, {headers: API.getHeaderAuthorization()})
		.then(response => setValues(response.data));
		// eslint-disable-next-line
	}, []);
	
	useEffect(() => {
		setSelectedValue(value || "");
	}, [value]);
	
	const handleChange = (event: SelectChangeEvent) => {
		setSelectedValue(event.target.value);
	};
	
	const labelId = `select-label-${select.name}`;
	
	return (
		<FormControl fullWidth margin="normal">
			
			<InputLabel id={labelId}>{select.label}</InputLabel>
			
			<Select
				labelId={labelId}
				id={select.name}
				label={select.label}
				name={select.name}
				value={selectedValue}
				onChange={handleChange}
			>
				{values ? values.map((value: string | object) => {
						
						if (typeof value === "string") {
							return (
								<MenuItem
									key={value}
									value={value}>
									{value}
								</MenuItem>
							);
						}
						
						return (
							<MenuItem
								key={value[select.optionValue]}
								value={value[select.optionValue]}>
								{value[select.optionLabel]}
							</MenuItem>
						);
					}) :
					<MenuItem value={''}>Cargando...</MenuItem>
				}
			</Select>
		</FormControl>
	)
}
