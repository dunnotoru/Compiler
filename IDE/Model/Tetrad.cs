namespace IDE.Model
{
    internal class Tetrad
    {
        public Tetrad(string or, string firstArgument, string secondArgument, string result)
        {
            Op = or;
            FirstArgument = firstArgument;
            SecondArgument = secondArgument;
            Result = result;
        }

        public string Op { get; set; }
        public string FirstArgument { get; set; }
        public string SecondArgument { get; set; }
        public string Result { get; set; }
    }
}
