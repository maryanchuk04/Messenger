import React, { Component } from 'react';
import { BrowserRouter, Navigate ,Route,Routes } from 'react-router-dom';
import Header from './components/header/header';
import MainPage from './components/main-page/mainPage';
import AuthPage from './components/authenticate/authPage';
import './custom.css'
import { BaseService } from './Services/BaseService';
export default class App extends Component{
    render (){
      return (
     <BrowserRouter>
      <Routes>
        <Route path = "/home" element = {<React.Fragment><MainPage/></React.Fragment>}/>
        <Route
            path ="/"
            element={
                <Navigate to="/home" />
            }
        />
        <Route
            path ="*"
            element={
                <Navigate to="/home" />
            }
        />
        <Route
            path ="authenticate"
            element={
               <AuthPage/>
            }
        />
      </Routes>
     </BrowserRouter>
    );
  }
}