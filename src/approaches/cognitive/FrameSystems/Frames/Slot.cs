namespace FrameSystem.Frames
{
    public class Slot : ICloneable
    {
        private object? _value;

        public delegate object? SlotFunction(SlotActionArgs args);

        public delegate void SlotAction(SlotActionArgs args);

        public SlotFunction? BeforeUpdateFunction { get; set; }

        public SlotAction? AfterUpdateAction { get; set; }

        public SlotFunction? NeedFunction { get; set; }

        public string Name { get; private set; }

        public object? Value
        {
            get => _value;
            set
            {
                if(value == null)
                {
                    _value = null;
                }
                else
                {
                    if (!Type.IsAssignableFrom(value.GetType()))
                    {
                        throw new InvalidOperationException();
                    }
                    _value = value;
                }
            }
        }

        public Type Type { get; private set; }

        public Slot(string name, Type? type = null,
            object? value = null,
            SlotFunction? beforeUpdateFunction = null,
            SlotAction? afterUpdateAction = null,
            SlotFunction? needFunction = null)
        {
            Name = name;
            Type = type ?? typeof(object);
            Value = value;
            BeforeUpdateFunction = beforeUpdateFunction;
            AfterUpdateAction = afterUpdateAction;
            NeedFunction = needFunction;
        }

        public object Clone() => new Slot(Name, Type, Value,
            BeforeUpdateFunction, AfterUpdateAction, NeedFunction);

        public override string ToString() =>
            $"Slot \"{Name}\"";
    }
}
