using System.Linq;

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
