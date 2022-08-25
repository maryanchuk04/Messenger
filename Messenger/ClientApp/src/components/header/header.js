import React, {useState} from 'react'
import { useNavigate } from 'react-router';
import './header.css';
import { Dialog , Button} from '@mui/material';
import useMediaQuery from '@mui/material/useMediaQuery';
import { useTheme } from '@mui/material/styles';
import Profile from './profile';
const Header = ({user}) => {
  const theme = useTheme();
  const fullScreen = useMediaQuery(theme.breakpoints?.down('sm'));
  const navigate = useNavigate("");
  const [open,setOpen] = useState(false);
  const handleClose = () =>{
    setOpen(false);
  }

  return (
    <>
    <header className = 'header'>
        <div className="container-header">
          <div className="title">
            <img src="https://iconarchive.com/download/i78091/igh0zt/ios7-style-metro-ui/MetroUI-Apps-Live-Messenger-Alt-2.ico" alt="" />
            <h1>Messenger</h1>
          </div>
          <div className="userInfo" onClick ={()=>setOpen(true)}>
            <h1>{user.userName}</h1>
            <img src={user.avatar} alt={user.email} className = "avatar"/>
        </div>
        </div>
    </header>
    <Dialog 
      fullScreen = {fullScreen}
      open={open}
      onClose={handleClose}
      aria-labelledby="responsive-dialog-title"
    >
        <Profile user = {user} handleClose ={handleClose}/>
      </Dialog>
    </>
  )
}

export default Header