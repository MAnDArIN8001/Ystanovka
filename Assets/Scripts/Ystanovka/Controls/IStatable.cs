namespace Ystanovka.Controls
{
    public interface IStatable
    {
        public bool State { get; }
        public void Enable();
        public void Disable();
    }
}