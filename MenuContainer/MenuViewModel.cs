using GalaSoft.MvvmLight;

namespace Pin.MenuContainer
{
    public class MenuViewModel : ViewModelBase
    {
        public MenuViewModel(MenuItemViewModel Context)
        {
            this.Context = Context;
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
    }
}
