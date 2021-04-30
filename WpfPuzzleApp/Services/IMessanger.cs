using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfPuzzleApp.Message;

namespace WpfPuzzleApp.Services
{
    public interface IMessanger
    {
        void Subcribe<T>(Action<object> action) where T : IMessage;
        void Send<T>(T message) where T : IMessage;
    }
}
