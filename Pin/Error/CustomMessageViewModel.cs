using System;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace Pin.Error
{
    public class CustomMessageViewModel : ViewModelBase
    {
        public Action Close { get; set; }
        public CustomMessageViewModel(Action Close)
        {
            MessageAction = new RelayCommand(Close);
        }

        private string _Message;
        public string Message
        {
            get
            {
                return _Message;
            }
            set
            {
                _Message = value;
                RaisePropertyChanged();
            }
        }


        private string _MessageBtnContent = "Close";
        public string MessageBtnContent
        {
            get
            {
                return _MessageBtnContent;
            }
            set
            {
                _MessageBtnContent = value;
                RaisePropertyChanged();
            }
        }


        private ICommand _MessageAction;
        public ICommand MessageAction
        {
            get
            {
                return _MessageAction;
            }
            set
            {
                _MessageAction = value;
                RaisePropertyChanged();
            }
        }

    }
}
