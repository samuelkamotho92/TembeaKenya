using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TembeaKenyaServiceBus.Services.IService
{
    internal interface IMessage
    {
        Task publishMessage(object message, string Tpoci_Queue_Name);
    }
}
