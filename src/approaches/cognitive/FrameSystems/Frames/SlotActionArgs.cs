using FrameSystem.Logger;

namespace FrameSystem.Frames
{
    public class SlotActionArgs
    {
        public Frame Frame { get; private set; }

        public Slot Slot { get; private set; }

        public object? Value { get; private set; }

        public ILogger? Logger { get; set; }

        public SlotActionArgs(Frame frame, Slot slot,
            object? value, ILogger? logger)
        {
            Frame = frame;
            Slot = slot;
            Value = value;
            Logger = logger;
        }
    }
}
