using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Pin.MenuContainer;

namespace Pin
{
    public interface IMainWindow
    {
        void WindowChangeState(WindowState? wState = null);
        void onExit();
        void PauseState(object lockingObject);
        void ResumeState(object lockingObject);
    }
}
