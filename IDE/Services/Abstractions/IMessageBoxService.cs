namespace IDE.Services.Abstractions
{
    enum MessageResult
    {
        None = 0,
        OK = 1,
        Cancel = 2,
        Yes = 6,
        No = 7
    }
    interface IMessageBoxService
    {
        void ShowMessage(string message, string caption = "");
        MessageResult ShowYesNoCancel(string message, string caption = "");
    }
}
