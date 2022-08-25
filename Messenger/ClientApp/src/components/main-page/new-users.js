import React, { useState } from 'react'
import {Dialog,DialogTitle,DialogContentText, Divider, Button } from '@mui/material'
import './new-users.css'
import User from './user';
import { UserService } from '../../Services/UserService';
import { compose } from '@mui/system';
import { ClipLoader } from 'react-spinners';

import useMediaQuery from '@mui/material/useMediaQuery';
import { useTheme } from '@mui/material/styles';
const NewUsers = ({handleChoose}) => {
  const theme = useTheme();
  const fullScreen = useMediaQuery(theme.breakpoints.down('sm'));
  const [loading,setLoading] = useState(true);
  const [open, setOpen] =useState(false);
  const [users,setUsers] = useState([]);
  const service = new UserService();
  const handleClose = () => {
    setOpen(false);
  };
  
  const handleOpen =async () =>{
    const res = await service.getAllUsers();
    if(!res.ok){
      console.log(await res.json());
    }else{
      
      const responce = await res.json();
      setUsers(responce);
      console.log(responce);
      setOpen(true);
      setLoading(false);
    }
   
    
  }

  return <><button id ="new-users-button" onClick = {()=>handleOpen()}>
        <svg fill="none" viewBox="0 3 22 22" height="26" width="24" xmlns="http://www.w3.org/2000/svg">
        <path xmlns="http://www.w3.org/2000/svg" d="M10 4C7.79086 4 6 5.79086 6 8C6 10.2091 7.79086 12 10 12C12.2091 12 14 10.2091 14 8C14 5.79086 12.2091 4 10 4ZM4 8C4 4.68629 6.68629 2 10 2C13.3137 2 16 4.68629 16 8C16 11.3137 13.3137 14 10 14C6.68629 14 4 11.3137 4 8ZM19 11C19.5523 11 20 11.4477 20 12V13H21C21.5523 13 22 13.4477 22 14C22 14.5523 21.5523 15 21 15H20V16C20 16.5523 19.5523 17 19 17C18.4477 17 18 16.5523 18 16V15H17C16.4477 15 16 14.5523 16 14C16 13.4477 16.4477 13 17 13H18V12C18 11.4477 18.4477 11 19 11ZM6.5 18C5.24054 18 4 19.2135 4 21C4 21.5523 3.55228 22 3 22C2.44772 22 2 21.5523 2 21C2 18.3682 3.89347 16 6.5 16H13.5C16.1065 16 18 18.3682 18 21C18 21.5523 17.5523 22 17 22C16.4477 22 16 21.5523 16 21C16 19.2135 14.7595 18 13.5 18H6.5Z" fill="#ffffff"></path>
        </svg>
       
  </button>
  <Dialog 
      fullScreen = {fullScreen}
      open={open}
      onClose={handleClose}
      aria-labelledby="responsive-dialog-title"
    >
        <div className="users-list">
          <h2>Users list</h2>
          <Divider sx={{borderColor : "white"}}/>
        {loading ? <ClipLoader/> :
            users?.map((item)=>(
              <User user ={item} handleChoose={handleChoose} handleClose={handleClose}/>
            )
            )
          }
          <Button onClick = {handleClose} sx ={{ background: "white",color : "black", margin : "auto"}} variant ='contained'>Close</Button>

        </div>
      </Dialog>
  </>
}

export default NewUsers