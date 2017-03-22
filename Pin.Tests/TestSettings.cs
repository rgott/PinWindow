﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Pin.Tests
{
    public class BlankTestSettings : ISettings
    {
        public BlankTestSettings()
        {
            ActionEvent = (int)Pin.ClipboardEvent.Move;
            var projectCollection = new StringCollection();

            Projects = projectCollection;
        }

        public int ActionEvent { get; set; }
        public string PrimaryProjectName { get; set; }

        public StringCollection Projects { get; set; }

        public event ClipboardActionChangedEventHandler ClipboardActionChanged;
        
        public ClipboardEvent ClipboardAction
        {
            get
            {
                return ClipboardEvent.Move;
            }
            set
            {
                // stub
            }
        }

        public void Save()
        {
            // stub only do nothing
        }
    }
}
