using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Interactivity;
using System.Windows;

namespace Pin.MenuContainer
{
    public class MenuViewModel : ViewModelBase
    {
        public MenuViewModel(MenuItemViewModel Context)
        {
            this.Context = Context;

            _DragEnter = DragEnterCmd;
        }

        private MenuItemViewModel _Context;
        public MenuItemViewModel Context
        {
            get
            {
                return _Context;
            }
            set
            {
                _Context = value;
                RaisePropertyChanged();
            }
        }

        private readonly DragEventHandler _DragEnter;
        private void DragEnterCmd(object sender, DragEventArgs e)
        {
            Context.RaiseWindowChangeState(WindowState.MinimizedDragging);
        }
        public DragEventHandler DragEnter => _DragEnter;
    }
}
