import React from 'react'
import './auth-page.css'
import LoginForm from './loginForm'


const AuthPage = () => {

  return (
    <div className="page-auth" style = {{backgroundImage : `url("/women-sparklers.jpg")`}}>
        <div className = "container">
            <LoginForm/>
        </div>
    </div>

    
  )
}

export default AuthPage