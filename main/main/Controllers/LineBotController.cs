using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace main.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LineBotController : ControllerBase
    {
        private readonly IConfiguration _config;
        public LineBotController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost]
        public IActionResult POST()
        {
            //get configuration from appsettings.json
            var token = _config.GetSection("LINE-Bot-Setting:channelAccessToken");
            var AdminUserId = _config.GetSection("LINE-Bot-Setting:adminUserID");
            var body = ""; //for JSON Body
            //create vot instance
            var bot = new isRock.LineBot.Bot(token.Value);
            isRock.LineBot.MessageBase responseMsg = null;

            try
            {
                //get JSON Body
                using (StreamReader reader = new StreamReader(Request.Body, System.Text.Encoding.UTF8))
                {
                    body = reader.ReadToEndAsync().Result;
                }
                //parsing JSON
                var ReceivedMessage = isRock.LineBot.Utility.Parsing(body);
                //Get LINE Event
                var LineEvent = ReceivedMessage.events.FirstOrDefault();
                //prepare reply message
                if (LineEvent.type.ToLower() == "message")
                {
                    switch (LineEvent.message.type.ToLower())
                    {
                        case "text":
                            responseMsg =
                                new isRock.LineBot.TextMessage($"you said : {LineEvent.message.text}");
                            break;
                        case "sticker":
                            responseMsg =
                            new isRock.LineBot.StickerMessage(1, 2);
                            break;
                        default:
                            responseMsg = new isRock.LineBot.TextMessage($"None handled message type : { LineEvent.message.type}");
                            break;
                    }
                }
                else
                    responseMsg = new isRock.LineBot.TextMessage($"None handled event type : { LineEvent.type}");

                //回覆訊息
                bot.ReplyMessage(LineEvent.replyToken, responseMsg);
                //response OK
                return Ok();
            }
            catch (Exception ex)
            {
                //回覆訊息
                bot.PushMessage(AdminUserId.Value, "Exception : \n" + ex.Message);
                //response OK
                return Ok();
            }
        }
    }
}