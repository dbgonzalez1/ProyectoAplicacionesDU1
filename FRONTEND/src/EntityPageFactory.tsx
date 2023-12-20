import FormBuilder from "./FormBuilder.tsx";
import TableEntity from "./TableEntity.tsx";
import {Fragment, useEffect, useState} from "react";
import {API} from "../api/API.ts";
import EntityData from "../models/EntityData.ts";
import {useSnackbar} from "notistack";
import axios from "axios";

export interface ModelProps {
	entity: string;
	entityData: EntityData;
}

function EntityPageFactory({entity, entityData}: ModelProps) {
	
	const [tableData, setTableData] = useState<object[]>([])
	const [modelIdToEdit, setModelIdToEdit] = useState<string>()
	
	const {enqueueSnackbar} = useSnackbar();
	
	const updateTableData = (): void => {
		axios.get(`${API.url}/${entity}/`, {headers: API.getHeaderAuthorization()})
		.then(response => {
			setTableData(response.data)
		})
		.catch(error => {
			if (error.response) {
				enqueueSnackbar(error.response.data.message, {variant: 'error'})
			} else {
				enqueueSnackbar("Error al procesar la solicitud", {variant: 'error'})
			}
		});
		
	}
	
	useEffect(() => {
		updateTableData()
		// eslint-disable-next-line
	}, []);
	
	return (
		<Fragment>
			
			<FormBuilder
				entityData={entityData}
				entity={entity}
				updateTableData={updateTableData}
				idToEdit={modelIdToEdit}
				setModelIdToEdit={setModelIdToEdit}
			/>
			
			<TableEntity
				entityData={entityData}
				entity={entity}
				tableData={tableData}
				updateTableData={updateTableData}
				modelIdToEdit={modelIdToEdit}
				setModelIdToEdit={setModelIdToEdit}
			/>
		
		</Fragment>
	)
}

export default EntityPageFactory;

