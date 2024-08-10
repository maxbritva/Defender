using Zenject;

namespace Player.Platform
{
    public class PlatformInitialize
    {
        private PlatformVisualChanger _platformVisualChanger;
        public void Initialize(int level)
        {
            _platformVisualChanger.ChooseLevel(level);
        }
        
        [Inject] private void Construct(PlatformVisualChanger visual) => _platformVisualChanger = visual;
    }
}