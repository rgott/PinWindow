﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Pin.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "14.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("300")]
        public int WINDOW_STATE_NORMAL_WIDTH {
            get {
                return ((int)(this["WINDOW_STATE_NORMAL_WIDTH"]));
            }
            set {
                this["WINDOW_STATE_NORMAL_WIDTH"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("300")]
        public int WINDOW_STATE_NORMAL_HEIGHT {
            get {
                return ((int)(this["WINDOW_STATE_NORMAL_HEIGHT"]));
            }
            set {
                this["WINDOW_STATE_NORMAL_HEIGHT"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("20")]
        public int WINDOW_STATE_MINIMIZED_HEIGHT {
            get {
                return ((int)(this["WINDOW_STATE_MINIMIZED_HEIGHT"]));
            }
            set {
                this["WINDOW_STATE_MINIMIZED_HEIGHT"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("20")]
        public int WINDOW_STATE_MINIMIZED_WIDTH {
            get {
                return ((int)(this["WINDOW_STATE_MINIMIZED_WIDTH"]));
            }
            set {
                this["WINDOW_STATE_MINIMIZED_WIDTH"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::System.Collections.ArrayList Projects {
            get {
                return ((global::System.Collections.ArrayList)(this["Projects"]));
            }
            set {
                this["Projects"] = value;
            }
        }
    }
}
