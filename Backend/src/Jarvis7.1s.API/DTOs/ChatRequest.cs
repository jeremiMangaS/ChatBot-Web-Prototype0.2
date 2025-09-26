namespace Jarvis7s.API.DTOs;

public class ChatMessage
{
    public string Role { get; set; }
    public string Content { get; set; }
}

public class ChatRequest
{
    public string Prompt { get; set; }
}

public class ChatApiRequest
{
    public List<ChatMessage> History { get; set; }
}