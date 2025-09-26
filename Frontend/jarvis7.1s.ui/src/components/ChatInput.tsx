"use client";

import { useState } from "react";

interface ChatInputProps
{
    onSend : (inputText : string) => void;
    isLoading : boolean;
}

export default function ChatInput({ onSend, isLoading } : ChatInputProps )
{
    const [inputText, setInputText] = useState("");

    const handleSubmit = (e : React.FormEvent) => {
        e.preventDefault();
        if (inputText.trim() && !isLoading) {
            onSend(inputText);
            setInputText("");
        }
    };

    return (
        <div className="p-4 bg-white border-t border-gray-200">
      <form onSubmit={handleSubmit} className="relative">
        <input
          type="text"
          placeholder="Type Something..."
          value={inputText}
          onChange={(e) => setInputText(e.target.value)}
          disabled={isLoading} // Inactive input when loading
          className="w-full p-3 pr-24 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-black disabled:bg-gray-100"
        />
        <button 
          type="submit"
          disabled={isLoading} // Inactive button when loading
          className="absolute inset-y-0 right-0 flex items-center px-4 font-bold text-white bg-black rounded-r-lg hover:bg-gray-800 disabled:bg-gray-500"
        >
          {isLoading ? '...' : 'Send'}
        </button>
      </form>
    </div>
    )
}