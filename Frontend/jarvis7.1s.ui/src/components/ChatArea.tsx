"use client"; // Client Component

import ChatInput from "@/components/ChatInput";
import MessageList from "@/components/MessageList";

import { useState } from "react";
import Chatinput from "./ChatInput";

// Message 
export interface Message
{
    role: "user" | "ai";
    content: string;
}

export default function MainContent() {

    // State
    const [messages, setMessages] = useState<Message[]>([]);
    const [isLoading, setIsLoading] = useState(false);

    // Function
    const handleSend = async (inputText : string) => {
        const newUserMessage : Message = { role : "user", content : inputText };
        setMessages(prevMessages => [...prevMessages, newUserMessage]);
        setIsLoading(true);
        
        // Send request to Backend
        try 
        {
            const response = await fetch('http://localhost:5323/api/chat', {
                method : 'POST',
                headers : {
                    'Content-Type' : 'application/json',
                },
                body : JSON.stringify({ prompt : inputText }),
            });

            if (!response.ok) {
                throw new Error('Network reponse is not okay')
            }

            const data = await response.json();
            const newAiMessage : Message = { role : "ai", content : data.responseText}
            setMessages(prevMessages => [...prevMessages, newAiMessage]);
        }
        catch (error)
        {
            console.error("Fetch error : ", error);
            const errorMessage : Message = {role : "ai", content : "Sorry, there's an issues..."};
            setMessages(prevMessages => [...prevMessages, errorMessage]);
        }
        finally
        {
            setIsLoading(false);
        }

    }

  return (
    <main className="flex-1 flex flex-col overflow-hidden">
        <MessageList messages={messages} />
        <ChatInput onSend={handleSend} isLoading={isLoading} />
    </main>
  );
}
