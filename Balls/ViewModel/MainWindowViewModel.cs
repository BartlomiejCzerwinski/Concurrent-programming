using Model;
using System.Collections;
using System.Windows.Input;

namespace ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly ModelAbstractApi ModelLayer;
        private int _BallVal = 1;
        private bool _isStopEnabled = false;
        private bool isStartEnabled = false;
        private bool _isAddEnabled = true;
        private int size = 0;
        private IList _balls;
        public ICommand AddCommand { get; set; }
        public ICommand RunCommand { get; set; }
        public ICommand StopCommand
        { get; set; }
        public MainWindowViewModel()
        {

            ModelLayer = ModelAbstractApi.CreateApi();
            StopCommand = new RelayCommand(Stop);
            AddCommand = new RelayCommand(AddBalls);
            RunCommand = new RelayCommand(Start);

        }

        public bool isStopEnabled
        {
            get { return _isStopEnabled; }
            set
            {
                _isStopEnabled = value;
                RaisePropertyChanged();
            }
        }

        public bool isRunEnabled
        {
            get { return isStartEnabled; }
            set
            {
                isStartEnabled = value;
                RaisePropertyChanged();
            }
        }

        public bool isAddEnabled
        {
            get
            {
                return _isAddEnabled;
            }
            set
            {
                _isAddEnabled = value;

                RaisePropertyChanged();
            }
        }

        public int BallVal
        {
            get
            {

                return _BallVal;
            }
            set
            {

                _BallVal = value;
                RaisePropertyChanged();


            }

        }

        private void AddBalls()
        {
            size += BallVal;
            if (size > 0)
            {
                isRunEnabled = true;
            }
            else
            {
                size = 0;
                isRunEnabled = false;
            }
            Balls = ModelLayer.Start(BallVal);
            BallVal = 1;


        }
        private void Stop()
        {
            isStopEnabled = false;
            isAddEnabled = true;
            isRunEnabled = true;
            ModelLayer.Stop();
        }
        private void Start()
        {
            isStopEnabled = true;
            isRunEnabled = false;
            isAddEnabled = false;
            ModelLayer.StartMoving();
        }
        public IList Balls
        {
            get => _balls;
            set
            {
                if (value.Equals(_balls))
                {
                    return;
                }

                _balls = value;
                RaisePropertyChanged();
            }
        }


    }
}