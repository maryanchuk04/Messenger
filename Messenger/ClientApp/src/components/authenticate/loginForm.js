import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router';
import { UserService } from '../../Services/UserService';
import './auth-page.css';

const LoginForm = () => {
    const [formState, setFormState] = useState(true);
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [confirmPassword, setConfirmPassword] = useState("");
    const navigate = useNavigate("");
    const userService = new UserService();

    const  signIn =async (e) =>{
        e.preventDefault();
        const data ={
            email : email,
            password : password
        }
        const result = await userService.auth(data);
        if(result.ok){
            let resp = await result.json();
            console.log(resp);
            localStorage.setItem("token", resp.token);
            navigate("home");
        }

    }
  return (
    <form className = "form">
        <div className="title">
            <img src="https://see.fontimg.com/api/renderfont4/OV148/eyJyIjoiZnMiLCJoIjoxNDQsInciOjIwMDAsImZzIjo3MiwiZmdjIjoiI0U1RTNFQyIsImJnYyI6IiMxOTAxMDEiLCJ0IjoxfQ/V2VsbGNvbWUgdG8gTWVzc2VuZ2Vy/enthusiastic.png" alt="" />
        </div>
        <div className="form-intro">
            {
                formState ? <>
                    <div className="title">
                        <h3>Sign In</h3>
                    </div>
                    <div className="field">
                        <p>Email:</p>
                        <input type="email" onChange ={(e)=>setEmail(e.target.value)} value = {email} />
                    </div>
                    <div className="field">
                        <p>Password:</p>
                        <input type="password" onChange ={(e)=>setPassword(e.target.value)} value = {password}/>
                    </div>
                    <button id = "signIn" onClick ={(e)=>signIn(e)}>
                        Continue
                    </button>
                    <p onClick = {()=>setFormState(false)} className ='haveAcc'>Do you have account?</p>
                </> : <>
                    <div className="title">
                        <h3>Sign Up</h3>
                    </div>
                    <div className="field">
                        <p>Email:</p>
                        <input type="email" onChange ={(e)=>setEmail(e.target.value)} value = {email}/>
                    </div>
                    <div className="field">
                        <p>Password:</p>
                        <input type="password" onChange ={(e)=>setPassword(e.target.value)} value = {password} />
                    </div>
                    <div className="field">
                        <p>Confirm Password:</p>
                        <input type="password" onChange ={(e)=>setConfirmPassword(e.target.value)} value = {confirmPassword} />
                    </div>
                    <button id = "signIn">
                        Create account
                    </button>
                    <p onClick = {()=>setFormState(true)} className ='haveAcc'>I already have account</p>
                </>
            }
        </div>
    </form>
  )
}

export default LoginForm