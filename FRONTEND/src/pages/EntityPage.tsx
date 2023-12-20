import EntityPageFactory from "../components/EntityPageFactory.tsx";
import {useParams} from "react-router-dom";
import {UserContext} from "../context/UserContextProvider.tsx";
import {useContext, useEffect, useState} from "react";
import Layout from "../layout/Layout.tsx";
import {CircularProgress} from "@mui/material";
import {models} from "../models/Forms/models.ts";


export default function EntityPage() {
	
	const [isLoading, setIsLoading] = useState(true);
	const {entity} = useParams();
	const {verifyOrRedirect} = useContext(UserContext);
	
	useEffect(() => {
		verifyOrRedirect()
		.finally(() => setIsLoading(false));
		// eslint-disable-next-line
	}, []);
	
	
	if (!entity) {
		return null;
	}
	
	const model = models.find(model => model.entity === entity);
	
	if (!model) {
		return null;
	}
	
	return (
		<Layout>
			{isLoading ? <CircularProgress/> : <EntityPageFactory
				entity={entity}
				entityData={model}
			/>}
		</Layout>
	)
}