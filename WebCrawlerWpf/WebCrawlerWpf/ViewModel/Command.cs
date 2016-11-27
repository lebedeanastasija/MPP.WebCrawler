using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WebCrawlerWpf.ViewModel
{
    class Command: ICommand
    {
        private readonly Action action;

        #pragma warning  disable 67
        public event EventHandler CanExecuteChanged;
        #pragma warning restore 67

        public Command(Action action)
        {
            this.action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            action();
        }
    }
}
