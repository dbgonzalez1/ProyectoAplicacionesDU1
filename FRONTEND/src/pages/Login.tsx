import {
	Avatar,
	Box,
	Container,
	CssBaseline,
	Grid,
	Link, TextField,
	Typography
} from "@mui/material";
import {
	TypographyOwnProps
} from "@mui/material/Typography/Typography";
import {
	FormEvent,
	useState
} from "react";
import {
	Home,
	LockOutlined
} from "@mui/icons-material";
import axios from "axios";
import {API} from "../api/API.ts";
import {useSnackbar} from "notistack";
import {LoadingButton} from "@mui/lab";


function Copyright(props: TypographyOwnProps) {
	return (
		<Typography variant="body2" color="text.secondary" align="center" {...props}>
			{"Copyright ©  "}
			<Link color="inherit" href="/">
				GRUPO #3 PROYECTO UNIDAD 1
			</Link>{' '}
		</Typography>
	);
}


function Login() {
	
	const {enqueueSnackbar} = useSnackbar();
	const [loading, setLoading] = useState(false);
	
	const handleSubmit = (event: FormEvent<HTMLFormElement>) => {
		event.preventDefault();
		const data = new FormData(event.currentTarget);
		const datasend = {
			username: data.get('username'),
			password: data.get('password')
		};
		
		setLoading(true);
		
		axios.post(`${API.url}/authenticate`, datasend)
		.then(response => {
			const token = "Bearer " + response.data;
			
			localStorage.setItem('token', token);
			
			window.location.href = '/';
			
			
		})
		.catch(error => {
			if (error.response) {
				console.log(error)
				setLoading(false);
				enqueueSnackbar(error.response.data.message, {variant: 'error'});
			} else {
				setLoading(false);
				enqueueSnackbar('Error en la solicitud', {variant: 'error'});
			}
		});
	}
	
	return (
		<Container component="main" maxWidth="xs">
			<CssBaseline/>
			<Box sx={{
				marginTop: 8,
				display: 'flex',
				flexDirection: 'column',
				alignItems: 'center',
			}}>
				
				<Link
					href="/"
					display="flex"
					flexDirection="row"
					alignItems="center"
					sx={{textDecoration: "none", mb: 5}}
				>
					<Avatar sx={{
						mr: 1,
						bgcolor: 'primary.main'
					}}
					>
						<Home/>
					</Avatar>
					Pagina principal
				</Link>
				
				<Avatar sx={{
					m: 1,
					bgcolor: 'primary.main'
				}}>
					<LockOutlined/>
				</Avatar>
				
				<Typography component="h1" variant="h5">
					Inciar Sesion
				</Typography>
				
				<Box component="form" onSubmit={handleSubmit} noValidate sx={{mt: 1}}>
					
					<TextField
						margin="normal"
						required
						fullWidth
						id="username"
						label="Nombre de usuario"
						name="username"
						autoComplete="username"
						autoFocus
					/>
					
					<TextField
						margin="normal"
						required
						fullWidth
						name="password"
						label="Contraseña"
						type="password"
						id="password"
						autoComplete="current-password"
					/>
					
					<LoadingButton
						loading={loading}
						type="submit"
						fullWidth
						variant="contained"
						sx={{mt: 3, mb: 2}}
					>
						Entrar
					</LoadingButton>
					
					<Grid container>
						<Grid item xs>
							<Link href="#" variant="body2">
								Olvidaste tu contraseña?
							</Link>
						</Grid>
						<Grid item>
							<Typography variant="body2" display={"inline"}>No tienes una cuenta? </Typography>
							<Link href="#" variant="body2">
								Registrate
							</Link>
						</Grid>
					</Grid>
				
				</Box>
			</Box>
			<Copyright sx={{mt: 8, mb: 4}}/>
		</Container>
	);
	
}

export default Login;