export interface ApiDataResponse <T>{
    success:boolean;
    httpStatusCode:number;
    message:string;
    data: T;
}


