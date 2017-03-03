using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Pin
{
    public interface IMainWindow
    {
        void WindowChangeState(WindowState? wState = null);
        void onExit();
    }
}
