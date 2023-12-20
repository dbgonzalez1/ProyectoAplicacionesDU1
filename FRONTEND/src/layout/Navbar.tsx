import {
	AppBar,
	AppBarProps, Avatar, Box,
	Button, Container,
	Drawer,
	IconButton,
	styled,
	Toolbar,
	Typography,
	useMediaQuery
} from "@mui/material";
import {
	Fragment,
	PropsWithChildren, useContext,
	useState
} from "react";
import NavlistDrawer from "./NavlistDrawer";
import {
	CarRepair,
	ChevronLeft,
	Login, Logout,
	Menu
} from "@mui/icons-material";
import {UserContext} from "../context/UserContextProvider.tsx";

const drawerWidth = 290;

interface MyContainerProps {
	open?: boolean;
}

const MyContainer = styled("main", {
	shouldForwardProp: (prop) => prop !== 'open'
})<MyContainerProps>(({theme, open}) => ({
	
	paddingTop: theme.spacing(2),
	transition: theme.transitions.create(['margin', 'width'], {
		easing: theme.transitions.easing.sharp,
		duration: theme.transitions.duration.leavingScreen,
	}),
	
	marginLeft: 0,
	...(open && {
		width: `calc(100% - ${drawerWidth}px)`,
		transition: theme.transitions.create(['margin', 'width'], {
			easing: theme.transitions.easing.easeOut,
			duration: theme.transitions.duration.enteringScreen,
		}),
		marginLeft: `${drawerWidth}px`,
	}),
}));

interface MyAppBarProps extends AppBarProps {
	open?: boolean;
}

const MyAppBar = styled(AppBar, {
	shouldForwardProp: prop => prop !== 'open',
})<MyAppBarProps>(({theme, open}) => ({
	
	transition: theme.transitions.create(['margin', 'width'], {
		easing: theme.transitions.easing.sharp,
		duration: theme.transitions.duration.leavingScreen,
	}),
	
	...(open && {
		width: `calc(100% - ${drawerWidth}px)`,
		marginLeft: `${drawerWidth}px`,
		transition: theme.transitions.create(['margin', 'width'], {
			easing: theme.transitions.easing.easeOut,
			duration: theme.transitions.duration.enteringScreen,
		}),
	}),
	
}));


/**
 
 xs, extra-small: 0px
 sm, small: 600px
 md, medium: 900px
 lg, large: 1200px
 xl, extra-large: 1536px
 
 * */


function Navbar({children}: PropsWithChildren) {
	
	const {user, logout, goToLogin} = useContext(UserContext);
	const [open, setOpen] = useState(false);
	const isSmallScreen = useMediaQuery('(max-width: 900px)');
	const variant = isSmallScreen ? 'temporary' : 'persistent';
	
	const toggleDrawer = () => {
		if (!isSmallScreen) {
			setOpen(!open);
		} else {
			setOpen(true);
		}
	}
	
	const logoutCallback = () => {
		logout();
		window.location.reload();
	}
	
	return (
		<Fragment>
			<MyAppBar
				position="static"
				sx={{
					height: {xs: 66, md: 82},
					justifyContent: "center",
					
				}}
				open={isSmallScreen ? false : open}
			>
				<Toolbar>
					<IconButton
						color="inherit"
						onClick={toggleDrawer}
					>
						{open ? <ChevronLeft/> : <Menu/>}
					</IconButton>
					<Typography
						variant="h6"
						sx={{
							flexGrow: 1,
							display: "flex",
							alignItems: "center",
						}}
					>
						<CarRepair fontSize={"large"} sx={{mx: 2}}/> ENSAMBLADO DE AUTOS
					</Typography>
					{user &&
                        <Box display="flex" alignItems="center" mx={2}>
                            <Avatar sx={{
								width: 32,
								height: 32,
								bgcolor: 'secondary.main',
							}}>
								{user.username.charAt(0).toUpperCase()}
                            </Avatar>
                            <Typography
                                variant="caption"
                                sx={{
									display: {xs: "none", md: "flex"},
									ml: 1,
									fontWeight: "bold"
								}}
                            >
								{user.username.toUpperCase()}
                            </Typography>
                        </Box>
					}
					<Button
						color="inherit"
						startIcon={user ? <Logout/> : <Login/>}
						sx={{
							display: {xs: "none", md: "flex"},
						}}
						onClick={user ? logoutCallback : goToLogin}
					>
						{user ? 'Cerrar sesión' : 'Iniciar sesión'}
					</Button>
					<IconButton
						color="inherit"
						sx={{
							display: {xs: "flex", md: "none"},
						}}
						onClick={user ? logoutCallback : goToLogin}
					>
						{user ? <Logout/> : <Login/>}
					</IconButton>
				</Toolbar>
			</MyAppBar>
			<Drawer
				open={open}
				onClose={() => setOpen(false)}
				anchor="left"
				variant={variant}
			>
				<NavlistDrawer sx={{
					width: drawerWidth
				}}
				/>
			</Drawer>
			<MyContainer open={isSmallScreen ? false : open}>
				<Container>
					{children}
				</Container>
			</MyContainer>
		</Fragment>
	)
	
}

export default Navbar;