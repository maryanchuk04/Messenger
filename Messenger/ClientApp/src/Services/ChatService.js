import {BaseService} from './BaseService'

const baseService = new BaseService();

export class ChatService{
    getChatById = (id) => baseService.getResource(`Chat/GetChat/${id}`);
    
}