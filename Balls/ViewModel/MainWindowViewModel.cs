using Model;
using System.Windows.Controls;
using System.Windows.Input;

namespace ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly ModelAbstractApi ModelLayer;
        private int _NumberOfBalls;

        public MainWindowViewModel()
        {

            ModelLayer = ModelAbstractApi.CreateApi(360, 360);
            Scommand1 = new RelayCommand(Start);
            Scommand2 = new RelayCommand(Stop);
            Acommand = new RelayCommand(CreateEllipses);

        }


        public ICommand Scommand1
        { get; set; }

        public ICommand Scommand2
        { get; set; }

        public ICommand Acommand
        { get; set; }


        public int NumberOfBalls
        {
            get
            {
                return _NumberOfBalls;
            }
            set
            {
                _NumberOfBalls = value;
                RaisePropertyChanged();
            }
        }

        public Canvas Canvas
        {
            get => ModelLayer.Canvas;

        }
        private void Start()
        {
            ModelLayer.Start();
        }


        private void CreateEllipses()
        {
            ModelLayer.CreateEllipses(NumberOfBalls);
        }

        private void Stop()
        {
            ModelLayer.Stop();
        }



    }
}
