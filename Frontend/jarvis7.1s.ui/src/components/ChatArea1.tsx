export default function MainContent() {
  return (
    <main style={{ flex: 1, padding: "40px" }}>
      <h1 style={{ textAlign: "center" }}>OPENING TEXT</h1>
      
      <div style={{ textAlign: "center", margin: "20px 0" }}>
        <input 
          type="text" 
          placeholder="Type Something" 
          style={{ padding: "10px", width: "60%", borderRadius: "10px", border: "1px solid #ccc" }}
        />
      </div>

      <div style={{ display: "flex", justifyContent: "center", gap: "20px" }}>
        <div style={{ padding: "20px", border: "1px solid #ccc", borderRadius: "10px" }}>Product</div>
        <div style={{ padding: "20px", border: "1px solid #ccc", borderRadius: "10px" }}>Product</div>
        <div style={{ padding: "20px", border: "1px solid #ccc", borderRadius: "10px" }}>Product</div>
      </div>
    </main>
  );
}
