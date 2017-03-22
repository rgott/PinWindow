using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Pin.MenuContainer;
using System.Windows.Input;

namespace Pin
{
    public sealed class MainViewModel : ViewModelBase
    {
        public bool _Clipboard_Copy;
        public bool _Clipboard_Move;
        private MenuItemViewModel _MenuContainerBind;

        public MainViewModel(MenuItemViewModel MenuContainerBind, ISettings Settings, IApplicationWindow Window)
        {
            this.Window = Window;
            this.Settings = Settings;
            this.MenuContainerBind = MenuContainerBind;

            switch (Settings.ClipboardAction)
            {
                case ClipboardEvent.Move:
                    Clipboard_Move = true;
                    Clipboard_Copy = false;
                    break;

                case ClipboardEvent.Copy:
                    Clipboard_Copy = true;
                    Clipboard_Move = false;
                    break;
            }

            OpenDragOverView = new RelayCommand(() =>
            {
                Window.WindowChangeState(WindowState.MinimizedDragging);
            });
            CloseDragOverView = new RelayCommand(() => Window.WindowChangeState(WindowState.Minimized));

            Copy_RadioBtn_Checked = new RelayCommand(CopyCheckedCmd);

            Move_RadioBtn_Checked = new RelayCommand(MoveCheckedCmd);
        }

        public bool Clipboard_Copy
        {
            get
            {
                return _Clipboard_Copy;
            }
            set
            {
                _Clipboard_Copy = value;
                RaisePropertyChanged();
            }
        }

        public bool Clipboard_Move
        {
            get
            {
                return _Clipboard_Move;
            }
            set
            {
                _Clipboard_Move = value;
                RaisePropertyChanged();
            }
        }

        public ICommand CloseDragOverView { get; set; }
        public ICommand Copy_RadioBtn_Checked { get; set; }

        public MenuItemViewModel MenuContainerBind
        {
            get
            {
                return _MenuContainerBind;
            }
            set
            {
                _MenuContainerBind = value;
                RaisePropertyChanged();
            }
        }

        public ICommand Move_RadioBtn_Checked { get; set; }
        public ICommand OpenDragOverView { get; set; }
        public ISettings Settings { get; private set; }

        public IApplicationWindow Window { get; set; }

        public void WindowChangeState(WindowState wState)
        {
            Vis_Minimized = false;
            Vis_MinimizedOpen = false;
            Vis_Maximized = false;
            Vis_ProjectView = false;

            switch (wState)
            {
                case WindowState.Minimized:
                    Vis_Minimized = true;
                    break;

                case WindowState.Normal:
                    Vis_Maximized = true;
                    break;

                case WindowState.MinimizedOpen:
                    Vis_MinimizedOpen = true;
                    break;

                case WindowState.MinimizedDragging:
                    Vis_ProjectView = true;
                    Vis_Minimized = true;
                    break;
            }
        }

        private void CopyCheckedCmd()
        {
            Settings.ClipboardAction = ClipboardEvent.Copy;
            Settings.Save();
        }

        private void MoveCheckedCmd()
        {
            Settings.ClipboardAction = ClipboardEvent.Move;
            Settings.Save();
        }

        #region Item Visibility

        private bool _Vis_Maximized = false;
        private bool _Vis_Minimized = false;
        private bool _Vis_MinimizedOpen = false;

        private bool _Vis_ProjectView = false;

        public bool Vis_Maximized
        {
            get
            {
                return _Vis_Maximized;
            }
            set
            {
                _Vis_Maximized = value;
                RaisePropertyChanged();
            }
        }

        public bool Vis_Minimized
        {
            get
            {
                return _Vis_Minimized;
            }
            set
            {
                _Vis_Minimized = value;
                RaisePropertyChanged();
            }
        }

        public bool Vis_MinimizedOpen
        {
            get
            {
                return _Vis_MinimizedOpen;
            }
            set
            {
                _Vis_MinimizedOpen = value;
                RaisePropertyChanged();
            }
        }

        public bool Vis_ProjectView
        {
            get
            {
                return _Vis_ProjectView;
            }
            set
            {
                _Vis_ProjectView = value;
                RaisePropertyChanged();
            }
        }

        #endregion Item Visibility
    }
}