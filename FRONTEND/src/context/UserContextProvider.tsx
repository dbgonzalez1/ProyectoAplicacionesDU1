import {createContext, PropsWithChildren, useEffect, useState} from "react";
import User from "../models/User.ts";
import {API} from "../api/API.ts";
import {useSnackbar} from "notistack";
import axios from "axios";


interface UserContextProps {
	user: User | null;
	logout: () => void;
	goToLogin: () => void;
	verifyOrRedirect: () => Promise<void>;
}

export const UserContext = createContext<UserContextProps>(null as unknown as UserContextProps)

function UserContextProvider({children}: PropsWithChildren) {
	
	const {enqueueSnackbar} = useSnackbar();
	const [user, setUser] = useState<User | null>(null);
	const [permitLogin, setPermitLogin] = useState(false);
	const isLoginPage = window.location.pathname === '/login';
	
	const verifyOrRedirect = async () => {
		console.log("verifying or redirecting")
		if (!API.constainsToken()) {
			console.log("redirecting to login")
			goToLogin();
		}
		
		await axios
		.get(`${API.url}/whoami`, {headers: API.getHeaderAuthorization()})
		.then(response => {
			setUser(response.data);
		})
		.catch(error => {
			console.log(error)
			if (error.response) {
				enqueueSnackbar(error.response.data.message, {variant: 'error'});
			}
			logout();
		})
	}
	
	useEffect(() => {
		if (API.constainsToken()) {
			
			const auth = API.getHeaderAuthorization();
			console.log("verifying: " + auth['Authorization'])
			
			
			axios
			.get(`${API.url}/whoami`, {headers: auth})
			.then(response => {
				if (isLoginPage) {
					window.location.href = '/';
					return;
				}
				setUser(response.data);
			})
			.catch(error => {
				console.log(error)
				if (error.response) {
					if (error.response.data) {
						enqueueSnackbar(error.response.data.message, {variant: 'error'});
						return;
					}
					enqueueSnackbar(error.response.data, {variant: 'error'});
				}
				setPermitLogin(true);
				logout();
			})
		} else {
			setPermitLogin(true);
		}
		
	}, []);
	
	const logout = () => {
		API.removeBearerToken();
		setUser(null);
	}
	
	const goToLogin = () => {
		window.location.href = '/login';
	}
	
	
	return (
		<UserContext.Provider value={{user, logout, goToLogin, verifyOrRedirect}}>
			{isLoginPage ? (permitLogin && children) : children}
		</UserContext.Provider>
	);
}

export default UserContextProvider;