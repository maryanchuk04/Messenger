import React, { useEffect } from 'react'
import './alert.css';
import { IconButton } from '@mui/material';
import CloseIcon from '@mui/icons-material/Close';
const Alert = ({message, handleClose}) => {
    useEffect(()=>{
        console.log(message)
    },[message])

    const close =()=> {
        handleClose(false);
    }
  return (
    <div className = "alert-container">
        <div className="content">
            <div className="ava">
                <img src={message.sender.avatar} className = "avatar" alt="" style ={{width: "60px" , height : "60px"}}/>
            </div>
            <div className="info">
                <h3>{message.sender.userName}</h3>
                <p>{message.content.length < 30 ? message.content : `${message.content.slice(0,30)}...`}</p>
            </div>  
        </div>
        <div className="close-container">
            <IconButton color="primary"  component="label" onClick={()=>close()}>
                <CloseIcon sx= {{color: "black"}}/>
            </IconButton>
        </div>  
    </div>
  )
}

export default Alert