import {
	Home, SubdirectoryArrowLeft,
} from '@mui/icons-material';

interface ListPage {
	icon: JSX.Element;
	name: string;
	path: string;
	subMenu?: SubMenu[];
	authRequired: boolean;
}

interface SubMenu {
	name: string;
	path: string;
	authRequired: boolean;
}

export const list: ListPage[] = [
	{
		icon: <Home/>,
		name: "Incio",
		path: "/",
		authRequired: false
	},
	{
		icon: <SubdirectoryArrowLeft/>,
		name: "Entidades",
		path: "#",
		authRequired: true,
		subMenu: [
			{
				name: "Provedores",
				path: "/entity/Proveedor",
				authRequired: true
			},{
				name: "Motores",
				path: "/entity/Motor",
				authRequired: true
			},{
				name: "Neumaticos",
				path: "/entity/Neumatico",
				authRequired: true
			},{
				name: "Frenos",
				path: "/entity/Frenos",
				authRequired: true
			},{
				name: "Amortiguadores",
				path: "/entity/Amortiguador",
				authRequired: true
			},{
				name: "Suspensiones",
				path: "/entity/Suspension",
				authRequired: true
			},{
				name: "Transmisiones",
				path: "/entity/Transmision",
				authRequired: true
			},{
				name: "Marca Autos",
				path: "/entity/MarcaAuto",
				authRequired: true
			},{
				name: "Autos",
				path: "/entity/Auto",
				authRequired: true
			},
		]
	}
]



