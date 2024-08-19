using System.Collections.Generic;
using Game.Interfaces;

namespace Game.GameCore.Pause
{
    public class PauseHandler : IPause
    {
        private List<IPause> _handlers = new List<IPause>();

        public void Add(IPause handler) => _handlers.Add(handler);
        public void Remove(IPause handler) => _handlers.Remove(handler);
        
        public void SetPause(bool isPaused)
        {
            foreach (var handler in _handlers) 
                handler.SetPause(isPaused);
        }
    }
}