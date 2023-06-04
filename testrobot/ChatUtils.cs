using ChatUAISDK.Requests;
using ChatUAISDK;

namespace testrobot.Utils
{
    public class ChatUtils
    {
        public static  async Task<string> TextMessageResponseAsync(string Content, string chatBaseUrl, string chatToken)
        {
            var client = new ChatUAIClient(chatBaseUrl, chatToken);
            var conversationId = Guid.NewGuid();

            var askResponse = await client.AskAsync(new AskRequest
            {
                Prompt = Content,
                ConversationId = conversationId,
            });

            if (askResponse.Code == 0)
            {
                return askResponse.Data.Answer;
            }
            else
            {
                return askResponse.Message;
            }

        }
    }
}
