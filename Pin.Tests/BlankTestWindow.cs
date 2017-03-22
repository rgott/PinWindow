using System;
using System.Collections.Generic;

namespace Pin.Tests
{
    internal class BlankTestWindow : IApplicationWindow
    {
        public void onExit()
        {
            // stub
        }
        public WindowState State { get; set; }


        public List<object> WindowLockState = new List<object>();
        public void PauseState(object lockingObject)
        {
            WindowLockState.Add(lockingObject);
        }

        public void ResumeState(object lockingObject)
        {
            WindowLockState.Remove(lockingObject);
        }

        public void WindowChangeState(WindowState wState)
        {
            // stub
        }
    }
}