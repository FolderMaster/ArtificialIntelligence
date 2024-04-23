namespace FrameSystem
{
    public class SlotActionArgs
    {
        public Frame Frame { get; private set; }

        public Slot Slot { get; private set; }

        public object? NewValue { get; private set; }

        public SlotActionArgs(Frame frame, Slot slot,
            object? newValue)
        {
            Frame = frame;
            Slot = slot;
            NewValue = newValue;
        }
    }
}
