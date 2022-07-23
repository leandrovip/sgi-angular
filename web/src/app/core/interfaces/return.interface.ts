export interface IReturn<T> extends Return {
	data: T;
}

export interface Return {
	success: boolean;
	message: string;
	events: string[];
}
