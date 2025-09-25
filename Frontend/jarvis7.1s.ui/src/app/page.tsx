import ChatArea from "@/components/ChatArea";
import ChatInput from "@/components/ChatInput";
import MessageList from "@/components/MessageList";
import Sidebar from "@/components/Sidebar";
import MainChat from "@/components/MainChat";
import MainContent from "@/components/MainChat";

export default function Home()
{
  return (
    <div style={{ display : "flex"}}>
      <Sidebar />
      <MainChat />
    </div>
  )
}