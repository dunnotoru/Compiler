namespace IDE.Services.Abstractions
{
    internal interface ICloseService
    {
        void Close(int code);
        void Close();
    }
}
