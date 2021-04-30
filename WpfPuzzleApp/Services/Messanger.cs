using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfPuzzleApp.Message;

namespace WpfPuzzleApp.Services
{
    public class Messanger : IMessanger
    {
        private readonly Dictionary<Type, List<Action<object>>> _subscriptions = new Dictionary<Type, List<Action<object>>>();

        public void Send<T>(T message) where T : IMessage
        {
            Type type = typeof(T);
            if (_subscriptions.ContainsKey(type))
            {
                foreach (Action<object> action in _subscriptions[type])
                {
                    action.Invoke(message);
                }
            }
        }

        public void Subcribe<T>(Action<object> action) where T : IMessage
        {
            Type type = typeof(T);
            if (!_subscriptions.ContainsKey(type))
            {
                _subscriptions[type] = new List<Action<object>>();
            }
            _subscriptions[type].Add(action);
        }
    }
}
