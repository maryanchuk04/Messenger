import React, { useEffect, useState } from 'react'
import { useNavigate } from 'react-router';
import { BaseService } from '../../Services/BaseService';
import Header from '../header/header';
import './mainPage.css';
import LeftSideMenu from './left-side-menu';
import RightSide from './right-side';
import { UserService } from '../../Services/UserService';
import { ChatService } from '../../Services/ChatService';
import { HubConnectionBuilder, LogLevel } from '@microsoft/signalr';
import { isTemplateHead } from 'typescript';
import Alert from '../alert/alert'

const MainPage = () => {
    const [showAlert, setShowAlert] = useState(false);
    const [message, setMessage] = useState({});
    const [typing,setTyping] = useState(false);
    const[loading, setLoading]= useState(false);
    const base = new BaseService();
    const chatService = new ChatService();
    const service = new UserService();
    const navigate = useNavigate("");
    const token = base.getUserToken();
    const [userChats, setUserChats] = useState([]);
    const [user, setUser] = useState({});
    const [chat, setChat] = useState(null);
    const [messages, setMessages] = useState([]);
    const [conn,setConn] =  useState(null);


    const join=async (connections)=>{
        const connection = new HubConnectionBuilder()
        .withUrl("https://localhost:7023/chatRoom",{
            accessTokenFactory: () => (localStorage.getItem('token'))
        })
        .configureLogging(LogLevel.Information)
        .build();
        
        connection.on("UsersInRoom", (users)=>{
            console.log("UsersInRoom : " ,users);

        })

        connection.on("JoinToRoom", (res)=>{
            console.log("JOIN", res);
        })

        connection.on("JoinToUsersRooms", (res)=>{
            console.log("JOIN", res);
        })

        connection.on("RecieveMessage",async (message) => {
            console.log("RecieveMessage", message);
            setMessages((oldArray) => [...oldArray, message]);
            const res = await service.getUsersChats();
            if(res.ok){
                const response = await res.json();
                setUserChats(response);
            
            }
           
        });

        connection.on("ShowAlert", async (message)=>{
            await setMessage(message);
            console.log("ShowAlert");
            setShowAlert(true)
            setTimeout(()=>setShowAlert(false), 5000);
            
        })

        connection.on("TypingMessage", (res)=>{
            console.log("Typing now");
            console.log(res);
            setTyping(true);
        })


        await connection.start();
        
        
        setConn(connection);
        
        await connection?.invoke("JoinToUsersRooms", connections);
    }
    

    const getUser = async () => {
        const result =await service.getCurrentUserInfo();
        const resp = await result.json();
        localStorage.removeItem("id");
        localStorage.setItem("id", resp.id);
        setUser(resp);
    }

    const getUserChats = async () =>{
        const res = await service.getUsersChats();
        if(!res.ok) {
            alert("Error");
            setTimeout(()=>{window.location.reload()},5000);
        }
        const response = await res.json();
        setUserChats(response);
       
        join(response.map(x=>x.id) );
    }

    const handleChoose =async (item) =>{
        setLoading(true);
        setChat(item);
        const res = await chatService.getChatById(item.id);
        if(res.ok){
            const response = await res.json();
            setMessages(response);
            
            
            console.log(response);
            setLoading(false);  
        }
        //await getUserChats();
        console.log(item);
        
    }

    const send = async (message) =>{
        console.log(message);
        await conn?.invoke("Send", chat?.id, message);
    }

    const isTyping =async () => {
        console.log("Typing...");
        await conn?.invoke("Typing", chat?.id, localStorage.getItem("id"));
    }



    useEffect(async() => {
      if (token) navigate("/authenticate");
      await getUser();
      await getUserChats();
    }, [])
    
  return (
      <>
        <Header user={user}/>
        <div className="layout">
            <LeftSideMenu chats = {userChats} handleChoose = {handleChoose}/>
            <RightSide 
                chat={chat} 
                messages ={messages} 
                loading = {loading} 
                setLoading= {setLoading} 
                setMessages = {setMessages} 
                conn = {conn} 
                send={send} 
                isTyping ={isTyping} 
                typing = {typing}
                setTyping = {setTyping}
            />
        </div>
        {showAlert ? <Alert message={message} handleClose={setShowAlert}/> : <></>}
      </>
    
  )
}

export default MainPage