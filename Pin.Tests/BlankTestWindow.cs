using System;

namespace Pin.Tests
{
    internal class BlankTestWindow : IMainWindow
    {
        public void onExit()
        {
            // stub
        }

        public void PauseState(object lockingObject)
        {
            // stub
        }

        public void ResumeState(object lockingObject)
        {
            // stub
        }

        public void WindowChangeState(WindowState? wState = default(WindowState?))
        {
            // stub
        }
    }
}