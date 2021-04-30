using MVVMTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfPuzzleApp.Models
{
    public class PartOfPuzzle : BaseViewModel
    {
        private string path;

        public string Path { get => path; set => ChangeProp(out path, value); }

    }
}

