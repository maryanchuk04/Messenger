import React, { useState } from 'react'
import './sender-bar.css'
const SenderBar = ({send, isTyping,setTyping}) => {
    const [messageField, setMessageField] = useState("");

    const handleSubmit =(e) =>{
        e.preventDefault();

        send(messageField);
        setMessageField("");
    }

    function handleWriting(){
      isTyping();
    }

    const handleBlur = () =>{
      console.log("untyping...");
      setTyping(false);
    }

  return (
    <form className = "sender-bar-container" onSubmit ={(e)=>{handleSubmit(e)}}>
        <textarea type="text" 
          className = "messageInput" 
          placeholder = "Write your message..." 
          rows = {1} 
          onChange ={(e)=>{setMessageField(e.target.value); handleWriting()}} 
          value = {messageField} 
          onBlur = {()=>handleBlur()}
        />
        <button id = "send" type = 'submit' disabled = {messageField.length === 0} >
            <svg fill="none" viewBox="0 0 24 24" height="24" width="24" xmlns="http://www.w3.org/2000/svg">
            <path xmlns="http://www.w3.org/2000/svg" d="M12 2C12.3788 2 12.725 2.214 12.8944 2.55279L21.8944 20.5528C22.067 20.8978 22.0256 21.3113 21.7882 21.6154C21.5508 21.9195 21.1597 22.0599 20.7831 21.9762L12 20.0244L3.21694 21.9762C2.84035 22.0599 2.44921 21.9195 2.2118 21.6154C1.97439 21.3113 1.93306 20.8978 2.10558 20.5528L11.1056 2.55279C11.275 2.214 11.6212 2 12 2ZM13 18.1978L19.166 19.568L13 7.23607V18.1978ZM11 7.23607L4.83402 19.568L11 18.1978V7.23607Z" fill="#0D0D0D"></path>
            </svg>
        </button>
    </form>
  )
}

export default SenderBar