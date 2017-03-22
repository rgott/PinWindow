namespace Pin
{
    public interface IApplicationWindow
    {
        void WindowChangeState(WindowState wState);
        void onExit();
        void PauseState(object lockingObject);
        void ResumeState(object lockingObject);
        WindowState State { get; }
    }
}
