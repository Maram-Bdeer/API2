using APIWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("[controller]")]
public class ChatController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ChatController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost("chat")]
    public async Task<IActionResult> AddChat(Chat request)
    {
        var chat = new Chat
        {
            ReceiverPhone = request.ReceiverPhone,
            SenderID = request.SenderID,
            Email = request.Email
        };

        _context.Chats.Add(chat);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(AddChat), new { id = chat.Id }, chat);
    }

    [HttpGet("chat")]
    public async Task<IActionResult> GetAllChats()
    {
        var chats = await _context.Chats.ToListAsync();
        return Ok(chats);
    }

    [HttpGet("chat/{chatId}/messages")]
    public async Task<IActionResult> GetChatMessages(int chatId)
    {
        var messages = await _context.Messages
            .Where(m => m.ChatId == chatId)
            .ToListAsync();

        return Ok(messages);
    }

    [HttpPost("chat/{chatId}/message")]
    public async Task<IActionResult> AddMessage(int chatId, string content, string senderID, string receiverID, DateTime time)
    {
        var message = new Message
        {
            ChatId = chatId,
            Content = content,
            SenderID = senderID,
            ReceiverID = receiverID,
            Time = time
        };

        _context.Messages.Add(message);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(AddMessage), new { id = message.Id }, message);
    }
    

}