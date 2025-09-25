export default function RootLayout({ children, } : { children: React.ReactNode; }) 
{
  return (
    <html lang="en">
      <body>
        <header style={{ background: "black", color: "white", padding: "10px" }}>
          <h1>Jarvis7.1s</h1>
        </header>

        <main style={{ padding: "20px" }}>
          {children} 
        </main>

        <footer style={{ background: "#eee", padding: "10px", marginTop: "20px" }}>
          <p>Still Learn About Next.Js</p>
        </footer>
      </body>
    </html>
  );
}
