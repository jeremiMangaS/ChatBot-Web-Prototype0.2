import { Message } from "./ChatArea";

interface MessageListProps {
  messages: Message[];
}

export default function MessageList({ messages }: MessageListProps) {
    // Opening Text
  if (messages.length === 0) {
    return (
      <div className="flex-1 p-6 overflow-y-auto">
        <div className="text-center h-full flex flex-col justify-center items-center">
          <h1 className="text-4xl font-bold">Jarvis7.1s</h1>
          <p className="text-gray-500 mt-2">Mulai percakapan dengan mengetik di bawah.</p>
        </div>
      </div>
    );
  }

  // Message List Layout
  return (
    <div className="flex-1 p-6 overflow-y-auto space-y-4">
      {messages.map((msg, index) => (
        <div
          key={index}
          // Preposition and color
          className={`flex ${msg.role === 'user' ? 'justify-end' : 'justify-start'}`}
        >
          <div
            className={`max-w-lg p-3 rounded-lg ${
              msg.role === 'user'
                ? 'bg-blue-500 text-white'
                : 'bg-gray-200 text-gray-800'
            }`}
          >
            {msg.content}
          </div>
        </div>
      ))}
    </div>
  );
}