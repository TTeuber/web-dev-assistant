using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotenv.net;
using Dotnet.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dotnet.Models;
using Microsoft.EntityFrameworkCore;
using OpenAI_API;
using OpenAI_API.Chat;
using OpenAI_API.Models;

namespace Dotnet.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly ChatContext _context;
        private readonly OpenAIAPI _api = new OpenAIAPI(DotEnv.Read()["OPENAI_API_KEY"]);

        public ChatController(ChatContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Chat>>> GetChats()
        {
            if (_context.Chat == null)
            {
                return NotFound();
            }
            return await _context.Chat.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Chat>> PostChat([FromBody]PostRq message)
        {
            _context.Chat.Add(new Chat{ Id = message.ChatId, Title = "New Chat" });
            // _context.Message.Add(new Message{ Id = Guid.NewGuid(), ChatId = message.ChatId, Content = message.Content, Role = "user" });
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMessages", new { id = message.ChatId }, message);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessages(Guid id)
        {
            if (_context.Message == null)
            {
                return NotFound();
            }


            var messages = await _context.Message.Where(m => m.ChatId == id).ToListAsync();

            if (messages == null)
            {
                return NotFound();
            }

            return messages;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChat(Guid id)
        {
            var chat = await _context.Chat.FindAsync(id);
            if (chat == null)
            {
                return NotFound();
            }

            _context.Chat.Remove(chat);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> PostMessage([FromBody] PostRq message, Guid id)
        {
            _context.Message.Add(new Message
                { Role = "user", Content = message.Content, ChatId = message.ChatId, Id = message.Id ?? Guid.NewGuid() }
            );

            await _context.SaveChangesAsync();

            var messages = await _context.Message.Where(m => m.ChatId == message.ChatId).ToListAsync();
            var chatMessages = new List<ChatMessage>();
            messages.ForEach(m => chatMessages.Add(new ChatMessage(ChatMessageRole.FromString(m.Role), m.Content )));

            var response = await _api.Chat.CreateChatCompletionAsync(new ChatRequest()
                {
                    Model = Model.ChatGPTTurbo,
                    Temperature = 0.5,
                    MaxTokens = 40,
                    Messages = chatMessages.ToArray()
                }
            );

            var result = response.Choices[0].Message;
            _context.Message.Add(new Message
                { Role = "assistant", Content = result.Content, ChatId = message.ChatId, Id = Guid.NewGuid() }
            );
            await _context.SaveChangesAsync();

            return Ok(new Message(){Role = "assistant", Content = result.Content, ChatId = message.ChatId, Id = Guid.NewGuid() });
            // return CreatedAtAction("GetMessages", new { id = message.ChatId }, message);
        }

        [HttpPut]
        public async Task<IActionResult> ChangeTitle([FromBody] PutTitleRq request)
        {
            var chat = await _context.Chat.FindAsync(request.ChatId);
            if (chat == null)
            {
                return NotFound();
            }

            chat.Title = request.Title;
            _context.Entry(chat).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            // return NoContent();
            return AcceptedAtAction("GetChats", new { id = request.ChatId }, request);
        }
    }
    public class PostRq
    {
        public string Content { get; set; }
        public Guid ChatId { get; set; }
        public Guid? Id { get; set; }
    }

    public class PutTitleRq
    {
        public string Title { get; set; }
        public Guid ChatId { get; set; }
    }
}
