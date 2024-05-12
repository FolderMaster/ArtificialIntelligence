using FrameSystem.Logger;

namespace FrameSystem.Frames
{
    public class Frame
    {
        private List<Frame> _children = new();

        private List<Slot> _slots = new();

        public IEnumerable<Slot> Slots => _slots;

        public string Name { get; private set; }

        public ILogger? Logger { get; set; }

        public Frame? Parent { get; private set; }

        public IEnumerable<Frame> Children => _children;

        public Frame(string name, IEnumerable<Slot>? slots = null,
            ILogger? logger = null)
        {
            Name = name;
            _slots = slots != null ? slots.ToList() :
                Enumerable.Empty<Slot>().ToList();
            Logger = logger;
        }

        public Frame(string name, IEnumerable<Slot>? slots = null,
            ILogger? logger = null, Frame? parent = null) :
            this(name, slots, logger)
        {
            Parent = parent;
            if (Parent != null)
            {
                Inherit(Parent);
            }
        }

        public object? NeedValue(string name)
        {
            var slot = Slots.FirstOrDefault((s) => s.Name == name);
            ArgumentNullException.ThrowIfNull(slot, nameof(slot));
            var value = (object?)null;
            if (slot.NeedFunction != null)
            {
                Logger?.Log($"{this} need action in {slot}",
                    new Dictionary<string, object>()
                    {
                        ["Background"] = ConsoleColor.DarkYellow,
                        ["Foreground"] = ConsoleColor.White
                    });
                var oldValue = slot.Value;
                value = slot.NeedFunction
                    (new SlotActionArgs(this, slot, slot.Value, Logger));
                slot.Value = value;
                Logger?.Log($"{this} need in {slot}: old value - " +
                    $"{oldValue}, new value - {value}",
                    new Dictionary<string, object>()
                    {
                        ["Background"] = ConsoleColor.DarkYellow,
                        ["Foreground"] = ConsoleColor.White
                    });
            }
            else
            {
                value = slot.Value;
                Logger?.Log($"{this} need in {slot}: {value}",
                    new Dictionary<string, object>()
                    {
                        ["Background"] = ConsoleColor.Yellow,
                        ["Foreground"] = ConsoleColor.Black
                    });
            }
            return value;
        }

        public void UpdateValue(string name, object? value)
        {
            var slot = Slots.FirstOrDefault((s) => s.Name == name);
            ArgumentNullException.ThrowIfNull(slot, nameof(slot));
            var oldValue = slot.Value;
            if (slot.BeforeUpdateFunction != null)
            {
                Logger?.Log($"{this} before update action in {slot}: {value}",
                    new Dictionary<string, object>()
                    {
                        ["Background"] = ConsoleColor.DarkCyan,
                        ["Foreground"] = ConsoleColor.White
                    });
                value = slot.BeforeUpdateFunction
                    (new SlotActionArgs(this, slot, value, Logger));
            }
            Logger?.Log($"{this} update in {slot}: " +
                $"old value - {oldValue}, new value - {value}",
                new Dictionary<string, object>()
                {
                    ["Background"] = ConsoleColor.Cyan,
                    ["Foreground"] = ConsoleColor.White
                });
            slot.Value = value;
            if (slot.AfterUpdateAction != null)
            {
                Logger?.Log($"{this} after update in {slot}: " +
                    $"old value - {oldValue}, new value - {value}",
                    new Dictionary<string, object>()
                    {
                        ["Background"] = ConsoleColor.DarkCyan,
                        ["Foreground"] = ConsoleColor.White
                    });
                slot.AfterUpdateAction
                    (new SlotActionArgs(this, slot, value, Logger));
            }
        }

        protected void Inherit(Frame parent)
        {
            parent._children.Add(this);
            Logger ??= parent.Logger;
            foreach (var slot in parent.Slots)
            {
                _slots.Add((Slot)slot.Clone());
            }
        }

        public override string ToString() =>
            $"Frame \"{Name}\"";
    }
}
