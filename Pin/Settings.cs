using System;

namespace Pin.Properties {
    
    
    // This class allows you to handle specific events on the settings class:
    //  The SettingChanging event is raised before a setting's value is changed.
    //  The PropertyChanged event is raised after a setting's value is changed.
    //  The SettingsLoaded event is raised after the setting values are loaded.
    //  The SettingsSaving event is raised before the setting values are saved.
    internal sealed partial class Settings : ISettings {
        
        public Settings() { }

        public event ClipboardActionChangedEventHandler ClipboardActionChanged;
        public ClipboardEvent ClipboardAction
        {
            get
            {
                return (ClipboardEvent)ActionEvent;
            }
            set
            {
                ActionEvent = (int)value;
                ClipboardActionChanged?.Invoke(value);
            }
        }
    }
}
