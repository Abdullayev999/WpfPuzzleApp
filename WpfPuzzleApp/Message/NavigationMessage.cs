using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfPuzzleApp.Message
{
    public class NavigationMessage : IMessage
    {
        public Type ViewModelType { get; set; }
    }
}
