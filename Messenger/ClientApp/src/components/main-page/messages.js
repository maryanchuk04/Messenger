import React, { useEffect, useRef, useState } from 'react'
import Message from '../message/message';
import './messages.css'


const Messages = ({chat,messages, typing}) => {
    const bottom = useRef(null);

    useEffect(()=>{
        bottom.current?.scrollIntoView({behavior : 'smooth'})
    }, [messages])
    
  return (
    <div className = "messages-container">
        {
            messages?.map((item)=>(
                <div className={item.senderId !== localStorage.getItem("id") ? 'left' : 'right'}>
                    <Message message ={item}/>
                </div>
            ))
        }
        {typing ? <p>Typing...</p> : <></>}
        <div ref ={bottom}></div>

    </div>
  )
}

export default Messages