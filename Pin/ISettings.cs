using System.Collections.Specialized;

namespace Pin
{
    public delegate void ClipboardActionChangedEventHandler(ClipboardEvent actionevent);

    public interface ISettings
    {
        string PrimaryProjectName { get; set; }

        void Save();

        event ClipboardActionChangedEventHandler ClipboardActionChanged;

        ClipboardEvent ClipboardAction { get; set; }
        StringCollection Projects { get; set; }
    }
}