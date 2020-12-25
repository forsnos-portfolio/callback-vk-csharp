using System;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using CallBackBot.Controllers.Dto;
using CallBackBot.Options;
using Microsoft.AspNetCore.Mvc;
using VkNet.Abstractions;
using VkNet.Model.RequestParams;

namespace CallBackBot.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CallbackController : ControllerBase
    {
        private static readonly RandomNumberGenerator Rng = RandomNumberGenerator.Create();

        private readonly VkOptions _vkOptions;
        private readonly IVkApi _vkApi;

        public CallbackController(IVkApi vkApi, VkOptions vkOptions)
        {
            _vkApi = vkApi ?? throw new ArgumentNullException(nameof(vkApi));
            _vkOptions = vkOptions ?? throw new ArgumentNullException(nameof(vkOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Callback(
            [FromBody] Updates updates,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            switch (updates.Type)
            {
                case "confirmation": return Ok(_vkOptions.Confirmation);
                case "message_new":
                {
                    var peerId = updates.Object.Message.PeerId;
                    // shuffle words in message
                    var words = updates.Object.Message.Text.Split(' ').ToArray();
                    var message = string.Join(' ', words.OrderBy(n => Guid.NewGuid()));
                    await SendMessage(peerId, message, cancellationToken);
                    break;
                }
            }

            return Ok("ok");
        }

        private Task<long> SendMessage(
            long peerId,
            string message,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (peerId <= 0) throw new ArgumentOutOfRangeException(nameof(peerId));
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(message));

            var randomId = GetRandomId();
            return _vkApi.Messages.SendAsync(new MessagesSendParams
            {
                RandomId = randomId,
                PeerId = peerId,
                Message = message
            });
        }

        private static int GetRandomId()
        {
            var intBytes = new byte[4];
            Rng.GetBytes(intBytes);
            return BitConverter.ToInt32(intBytes, 0);
        }
    }
}