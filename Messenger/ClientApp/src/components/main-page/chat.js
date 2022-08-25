import React, { useEffect } from 'react'
import convertDate from '../../utils/dateConvert';
import './chat.css'
const Chat = ({chat,handleChoose}) => {
    const user = chat.users.filter(x=>x.id !== localStorage.getItem("id"))[0];
    useEffect(()=>{
        console.log(chat)
        console.log(user)
    },[])
  return (
    <div className = 'chat-preview-item' onClick ={()=>{handleChoose(chat)}}>
        <div className="ava">
            <img src={user?.avatar} alt="user" className = "avatar"/>
        </div>
        <div className="info">
            <h5>{user.userName}</h5>
            <p className = "from">{chat.lastMessage ? chat.lastMessageUserId==localStorage.getItem("id") ? "You:  "+chat.lastMessage : `${user.userName}:  ${chat.lastMessage}` : "Start messenging"}</p>
        </div>
        <div className="date-info">
           {convertDate(chat.lastMessageTime)}
        </div>
    </div>
  )
}

export default Chat