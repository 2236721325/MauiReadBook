using CommunityToolkit.Mvvm.Messaging.Messages;

namespace WM.EasyReading.Messages
{
    public class OnReadBookPageDisappearing : ValueChangedMessage<bool>
    {
        public OnReadBookPageDisappearing(bool value) : base(value)
        {
        }
    }
}
