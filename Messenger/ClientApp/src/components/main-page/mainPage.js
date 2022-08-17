import React, { useEffect } from 'react'
import { useNavigate } from 'react-router';
import { BaseService } from '../../Services/BaseService';
const MainPage = () => {
    const service = new BaseService();
    const navigate = useNavigate("");
    const token = service.getUserToken();

    
    useEffect(() => {
      console.log("work")
      if (token) navigate("/authenticate")
    }, [])
    
  return (
    <div>MainPage</div>
  )
}

export default MainPage