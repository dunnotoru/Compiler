using IDE.Model;

namespace IDE.ViewModel
{
    internal class TetradViewModel : ViewModelBase
    {
        public string? _op;
        public string? arg1;
        public string? arg2;
        public string? result;

        public TetradViewModel(Tetrad tetrad)
        {
            Op = tetrad.Op;
            FirstArgument = tetrad.FirstArgument;
            SecondArgument = tetrad.SecondArgument;
            Result = tetrad.Result;
        }

        public string? Op
        {
            get { return _op; }
            set { _op = value; OnPropertyChanged(); }
        }
        public string? FirstArgument
        {
            get { return arg1; }
            set { arg1 = value; OnPropertyChanged(); }
        }
        public string? SecondArgument
        {
            get { return arg2; }
            set { arg2 = value; OnPropertyChanged(); }
        }
        public string? Result
        {
            get { return result; }
            set { result = value; OnPropertyChanged(); }
        }
    }
}
