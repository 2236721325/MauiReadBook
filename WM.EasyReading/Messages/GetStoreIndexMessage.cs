using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.EasyReading.Service.Dtos;

namespace WM.EasyReading.Messages
{
    public class GetStoreIndexMessage : ValueChangedMessage<BookDetailDto>
    {
        public GetStoreIndexMessage(BookDetailDto value) : base(value)
        {

        }
    }
}
