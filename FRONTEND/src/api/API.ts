import {AxiosRequestConfig} from "axios";

export interface ErrorResponse {
	message: string;
	target?: string;
}

export class API {
	
	
	public static url: string = 'http://localhost:5000';
	
	public static getBearerToken(): string | null {
		return localStorage.getItem('token');
	}
	
	public static removeBearerToken(): void {
		localStorage.removeItem('token');
	}
	
	public static constainsToken() {
		return (API.getBearerToken() !== null);
	}
	
	public static getHeaderAuthorization(config?: AxiosRequestConfig): { 'Authorization': string | null } {
		return {
			'Authorization': API.getBearerToken(),
			...config
		}
	}
}