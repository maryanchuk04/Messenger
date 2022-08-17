export class BaseService{

    getUserToken = () => {
        let token = localStorage.getItem("token");
        return token === null || token === undefined
    };
}