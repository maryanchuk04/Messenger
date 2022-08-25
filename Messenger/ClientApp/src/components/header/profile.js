import { Divider,IconButton, Button } from '@mui/material'
import React, { useEffect, useState } from 'react'
import {uploadImage} from '../../utils/imageUpload'
import './profile.css'
import CheckCircleIcon from '@mui/icons-material/CheckCircle';
import PhotoCamera from '@mui/icons-material/PhotoCamera';
import { UserService } from '../../Services/UserService';
const Profile = ({user, handleClose}) => {
    const [userName, setUserName] = useState(user.userName);
    const [email, setEmail] = useState(user.email);
    const service  = new UserService();
    const [file, setFile] = useState({});

    async function handleFileSelected(e){
        const files = Object(e.currentTarget.files)[0]
        console.log(files)
        setFile(files);
        const res = await uploadImage(files);
        const data = {
          avatar : res
        }
        
        const req = await service.changeAvatar({ avatar : res });
        if(req.ok){
            window.location.reload();
        }
    } 
    
    const changeUserName = async () =>{
        const res = service.changeUserName({userName : userName});
        if(res.ok){
            window.location.reload();
        }
    }

    const changeEmail = () =>{

    }
  return (
    <div className = "profile-container">
        <div className="title">
            <h2>Profile</h2>
            <Divider sx ={{borderColor: 'white'}}/>
        </div>
        <div className="user-information">
            <div className="ava">
                <img src={user.avatar} alt="" className = "avatar" style ={{height : "150px", width : "150px", margin:"auto"}} />
                <IconButton color="primary" aria-label="upload picture" component="label" sx = {{ margin : "auto"}} >
                    <input hidden accept="image/*" type="file" onChange = {(e)=>handleFileSelected(e)} />
                    <PhotoCamera sx ={{color : "white"}}/>
                </IconButton>
            </div>
            <form action="" className = "user-form">
                <div className="field">
                    <input type="text" defaultValue ={user.userName} onChange = {(e)=>setUserName(e.target.value)}/>
                    <IconButton onClick = {()=>changeUserName()}>
                        <CheckCircleIcon sx={{color: "white"}}/>
                    </IconButton>
                </div>
                <div className="field">
                    <input type="text" defaultValue ={user.email} onChange = {(e)=>setEmail(e.target.value)}/>
                    <IconButton onClick = {()=> changeEmail()}>
                        <CheckCircleIcon sx={{color: "white"}} />
                    </IconButton>
                </div>
            </form>
        </div>
        <Button onClick = {handleClose} sx ={{ background: "white",color : "black", margin : "auto"}} variant ='contained'>Close</Button>
    </div>
  )
}

export default Profile