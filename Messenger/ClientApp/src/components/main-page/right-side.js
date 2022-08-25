import React, { useEffect, useState } from 'react'

import Messages from './messages'
import './ride-side.css'
import SenderBar from './sender-bar'

import { ClipLoader } from 'react-spinners'
import { contains } from 'jquery'

const RightSide = (props) => {
    const { chat, messages, loading, setLoading, setMessages, conn, send, isTyping,typing, setTyping} = props;
    const otherUser = chat?.users.filter(x=>x.id !== localStorage.getItem('id'))[0]

   
    useEffect(()=>{
        console.log(messages);
    },[])

    useEffect(()=>{
        console.log("Chat use Effect")
        
    },[chat])


  return (
    <div className = "ride-side-container">
        {
            !loading ?
                chat ?
                    <>
                        <div className="messages-header">
                            <div className="ava">
                                <img src={otherUser?.avatar} alt="" className = "avatar" />
                            </div>
                            <div className="info">
                                <h3>{otherUser?.userName}</h3>
                            </div>
                        </div>
                        <Messages chat ={chat} messages = {messages} typing = {typing}/>
                        <SenderBar send = {send} isTyping = {isTyping} setTyping = {setTyping}/>
                    </>
                    : <div className = "choose-chat">
                        <p>Choose who you want to write</p>
                    </div>
                : <div className = "loader">
                    <ClipLoader size = {200} color ="white"/>
                </div>
        }
    </div>
  )
}

export default RightSide