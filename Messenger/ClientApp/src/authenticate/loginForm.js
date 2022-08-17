import React from 'react';
import './auth-page.css';

const LoginForm = () => {
  return (
    <form className = "form">
        <div className="left">
            <div className="title">
                <h1>Wellcome to Messenger</h1>
            </div>
        </div>
        <div className="right" style = {{backgroundImage : `url("/undraw_secure_login_pdn4.png")`}}>
            
        </div>

    </form>
  )
}

export default LoginForm