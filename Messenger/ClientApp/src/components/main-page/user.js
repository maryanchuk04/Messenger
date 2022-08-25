import { Divider , Button, IconButton} from '@mui/material'
import React from 'react'
import './user.css'
import MessageIcon from '@mui/icons-material/Message';
const User = ({user, handleChoose, handleClose}) => {
  
  return (
    <div className ="user-component">
        <Divider sx ={{borderColor: 'white'}}/>
        <div className="user">
          <img src={user.avatar} className = "avatar" alt="" style ={{height : "80px", width : "80px"}}/>
          <h3>{user?.userName}</h3> 
          <IconButton className = "message-icon">
            <MessageIcon sx={{color:"white"}} onClick ={()=>{
              handleChoose({
              id : user.id, 
              users : [
                {
                  id : user.id,
                  avatar : user.avatar,
                  userName : user.userName
                },
                {
                  id : localStorage.getItem("id"),
                  userName : "",
                  avatar: ""
                }
              ]});
              handleClose()
              }}/>
          </IconButton>
        </div>
        
        <Divider sx ={{borderColor: 'white'}}/>
    </div>
  )
}

export default User