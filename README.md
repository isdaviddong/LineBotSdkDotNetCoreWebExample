#
# LineBotSdk .net core Web Example
#

### how to use?
please DO remember to replace the following parameter in the file 'appsettings.json'
```json
  //config
  "LINE-Bot-Setting": {
    "channelAccessToken": "_______please_input_your_channel_access_token________",
    "adminUserID": "_______please_input_your_admin_user_id________"
  }
```

1. you can simply clone this repo
2. go to the folder which included the file 'main.csproj'
3. Run this app with (of couse, dotnet core 2.2 need to installed):
```
dotnet run 
```
4. and follow the message to open your browser: 
<img src='https://i.imgur.com/VDZ7ebG.png'/>
5. now you can try to send message or sticker to yourself
<img src='https://i.imgur.com/pjiXSqR.png'/> 

deploy and setup the Webhook URL with:
```
https://your_domain_//api/LineBot_
```