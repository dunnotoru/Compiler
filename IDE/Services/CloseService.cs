using IDE.Services.Abstractions;
using System;

namespace IDE.Services
{
    internal class CloseService : ICloseService
    {
        private Action _close;

        public CloseService(Action close)
        {
            _close = close;
        }

        public void Close()
        {
            _close();
        }

        public void Close(int code)
        {
            _close();
        }
    }
}
