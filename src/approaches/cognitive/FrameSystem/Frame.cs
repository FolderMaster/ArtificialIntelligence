namespace FrameSystem
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

        public Frame(string name, IEnumerable<Slot>? slots = null)
        {
            Name = name;
            _slots = slots != null ? slots.ToList() :
                Enumerable.Empty<Slot>().ToList();
        }

        public Frame(string name,
            IEnumerable<Slot>? slots = null,
            Frame? parent = null) : this(name, slots)
        {
            Parent = parent;
            if(Parent != null)
            {
                Inherit(Parent);
            }
        }

        public object? GetValue(string name)
        {
            var slot = Slots.FirstOrDefault((s) => s.Name == name);
            var oldValue = slot.Value;
            var newValue = (object?)null;
            if(slot.NeedAction != null)
            {
                newValue = slot.NeedAction?.Invoke
                    (new SlotActionArgs(this, slot, null));
                Logger?.Log($"Frame {Name} need in slot {slot.Name}: {oldValue} => {newValue}");
                slot.Value = newValue;
            }
            else
            {
                newValue = oldValue;
                Logger?.Log($"Frame {Name} need in slot {slot.Name}: {oldValue}");
            }
            return newValue;
        }

        public void SetValue(string name, object? value = null)
        {
            var slot = Slots.FirstOrDefault((s) => s.Name == name);
            if (slot == null)
            {
                throw new InvalidOperationException();
            }
            var newValue = value;
            var oldValue = slot.Value;
            if (value != null)
            {
                newValue ??= slot.InsertAction?.Invoke
                    (new SlotActionArgs(this, slot, value));
                Logger?.Log($"Frame {Name} insert in slot {slot.Name}: {oldValue}, {value} => {newValue}");
            }
            else
            {
                newValue = slot.RemoveAction?.Invoke
                    (new SlotActionArgs(this, slot, value));
                Logger?.Log($"Frame {Name} remove from slot {slot.Name}: {oldValue}, {value} => {newValue}");
            }
            slot.Value = newValue;
        }

        protected void Inherit(Frame parent)
        {
            parent._children.Add(this);
            Logger = parent.Logger;
            foreach (var slot in parent.Slots)
            {
                _slots.Add((Slot)slot.Clone());
            }
        }
    }
}
