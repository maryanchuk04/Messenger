import React, { useEffect } from 'react'
import convertDate from '../../utils/dateConvert'
import './message.css'
const Message = ({message}) => {
    useEffect(()=>{
     
    },[])
  return (
    <div className = 'message'>
        <div className="message-intro">
            <div className = "message-container">
                <p>{message.content}</p>
            </div>
            <p className = "date">{convertDate(message.when)}</p>
        </div>
    </div>
    
  )
}

export default Message