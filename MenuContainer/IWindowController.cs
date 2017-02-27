namespace Pin.MenuContainer
{
    public interface IWindowHandler
    {
        void RequestExit();
        void RequestSizeChange(WindowState winstate);
        void RequestTack(PinnedState winstate);
    }
}
