import { BaseService } from "./BaseService";

const baseService = new BaseService();

export class UserService{

    auth = data => baseService.setResource('User/Login',data);

    getUserInfo = () => baseService.getResource('User/');

    registration = data => baseService.setResource("User/Registration", data);

    getUsersChats = () => baseService.getResource("Chat/GetUserChats");

    getCurrentUserInfo = () => baseService.getResource("User/GetCurrentUserInfo");

    getAllUsers =() => baseService.getResource("User/GetAllUsers");
    
    changeUserName = (data) => baseService.setResource("User/ChangeUserName", data);

    changeAvatar = (data) => baseService.setResource("User/ChangeAvatar", data);

    changeEmail = (data) => baseService.setResource("User/ChangeMail", data);


}