using Dalamud.Game.Text;
using Dalamud.Game.Text.SeStringHandling;

namespace GrindTracker
{
    public class Message
    {
        internal ulong ReceiverId { get; set; }
        internal ulong ContentId { get; set; } // 0 if unknown
        internal XivChatType Type { get; set; }
        internal uint SenderId { get; set; }
        internal SeString Sender { get; set; }
        internal SeString Content { get; set; }

        public Message(ulong receiverId, ulong contentId, XivChatType type, uint senderId, SeString sender, SeString content)
        {
            ReceiverId = receiverId;
            ContentId = contentId;
            Type = type;
            SenderId = senderId;
            Sender = sender;
            Content = content;
        }
    }
}