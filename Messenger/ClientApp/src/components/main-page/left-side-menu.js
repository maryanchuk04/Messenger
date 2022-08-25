import React from 'react'
import Chats from './chats'
import Search from './search'
import './left-side-menu.css'
const LeftSideMenu = ({chats, handleChoose}) => {
  return (
    <div className = "left-sidebar-container">
        <Search/>
        <Chats chats = {chats} handleChoose = {handleChoose}/>
    </div>
  )
}

export default LeftSideMenu