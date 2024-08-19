
namespace Player.Input
{
    public class InputHandler
    {
        private IInput _input;
        
        public InputHandler(IInput input) => _input = input;

        public float Rotate() => _input.ReadRotationInput();
        
        public bool IsFire() => _input.IsFireButtonActive();
       
    }
}