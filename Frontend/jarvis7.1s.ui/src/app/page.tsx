import Sidebar from "@/components/Sidebar";
import ChatArea from "@/components/ChatArea";

export default function Home()
{
  return (
    <div style={{ display : "flex"}}>
      <Sidebar />
      <ChatArea />
    </div>
  );
}