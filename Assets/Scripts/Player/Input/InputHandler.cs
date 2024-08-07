using System;
using JetBrains.Annotations;

namespace Player.Input
{
    public class InputHandler: IDisposable
    {
        private DesktopInput _input;



        
        public InputHandler()
        {
            _input = new DesktopInput();
            
        }
        public void Dispose()
        {
           
        }
    }
}