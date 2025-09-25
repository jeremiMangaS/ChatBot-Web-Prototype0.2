export default function Sidebar() {
  return (
    <aside style={{
      width: "200px",
      background: "#f5f5f5",
      padding: "20px",
      display: "flex",
      flexDirection: "column",
      justifyContent: "space-between",
      height: "100vh"
    }}>
      <div>
        <h2>Logo</h2>
        <ul style={{ listStyle: "none", padding: 0 }}>
          <li>Chat</li>
          <li>Menu 1</li>
          <li>Menu 2</li>
          <li>Menu 3</li>
        </ul>
      </div>
      <div>
        Settings
      </div>
    </aside>
  );
}
