using IDE.Services.Abstractions;
using System.Windows;

namespace IDE.Services
{
    internal class CloseService : ICloseService
    {
        public void Close()
        {
            Application.Current.Shutdown();
        }

        public void Close(int code)
        {
            Application.Current.Shutdown(code);
        }
    }
}
