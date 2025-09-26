export default function Sidebar() {
  return (

    <aside className="w-64 bg-white p-5 flex flex-col justify-between">
      <div>
        <h2 className="text-2xl font-bold mb-5">Logo</h2>
        <ul className="space-y-2">

          <li className="p-2 rounded-md hover:bg-gray-200 cursor-pointer">Chat</li>
          <li className="p-2 rounded-md hover:bg-gray-200 cursor-pointer">Menu 1</li>
          <li className="p-2 rounded-md hover:bg-gray-200 cursor-pointer">Menu 2</li>
          <li className="p-2 rounded-md hover:bg-gray-200 cursor-pointer">Menu 3</li>
          
        </ul>
      </div>
    </aside>

  );
}
