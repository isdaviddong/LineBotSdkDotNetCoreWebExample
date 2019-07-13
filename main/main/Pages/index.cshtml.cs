using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace main.Pages
{
    public class indexModel : PageModel
    {
        public string result = "";


        private readonly IConfiguration _config;
        public indexModel(IConfiguration config)
        {
            _config = config;
        }

        [BindProperty]
        public string txbToken { get; set; }
        [BindProperty]
        public string txbUserId { get; set; }
        [BindProperty]
        public string txbMessage { get; set; }
        [BindProperty]
        public int txbStickerPkgId { get; set; }
        [BindProperty]
        public int txbStickerStkId { get; set; }

        public void OnGet()
        {
            //get configuration from appsettings.json
            var token = _config.GetSection("LINE-Bot-Setting:channelAccessToken");
            var AdminUserId = _config.GetSection("LINE-Bot-Setting:adminUserID");
            txbToken = token.Value;
            txbUserId = AdminUserId.Value;
            txbMessage = "";
        }

        public void OnPostSendMessage()
        {
            if (string.IsNullOrEmpty(txbToken) ||
                string.IsNullOrEmpty(txbUserId) ||
                string.IsNullOrEmpty(txbMessage))
            {
                this.result = "cannot push message due to missing information...";
            }
            else
            {
                var bot = new isRock.LineBot.Bot(this.txbToken);

                var ret = bot.PushMessage(this.txbUserId, this.txbMessage);
                this.result = ret;
            }
        }
        public void OnPostSendSticker()
        {
            if (string.IsNullOrEmpty(txbToken) ||
                    string.IsNullOrEmpty(txbUserId))
            {
                this.result = "missing data...";
            }
            else
            {
                var bot = new isRock.LineBot.Bot(this.txbToken);

                var ret = bot.PushMessage(
                    this.txbUserId,
                    new isRock.LineBot.StickerMessage(this.txbStickerPkgId, this.txbStickerStkId)
                    );
                this.result = ret;
            }
            return;
        }
    }
}