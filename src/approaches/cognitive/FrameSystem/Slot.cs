namespace FrameSystem
{
    public class Slot : ICloneable
    {
        public delegate object? SlotAction(SlotActionArgs args);

        public SlotAction? InsertAction { get; set; }

        public SlotAction? RemoveAction { get; set; }

        public SlotAction? NeedAction { get; set; }

        public string Name { get; private set; }

        public object? Value { get; set; }

        public Type Type { get; private set; }

        public Slot(string name, Type? type = null)
        {
            Name = name;
            Type = type ?? typeof(object);
        }

        public Slot(string name, Type? type = null,
            object? value = null) : this(name, type)
        {
            Value = value;
        }

        public Slot(string name,
            SlotAction? insertAction = null,
            SlotAction? removeAction = null,
            SlotAction? needAction = null) :
            this(name, null)
        {
            InsertAction = insertAction;
            RemoveAction = removeAction;
            NeedAction = needAction;
        }

        public Slot(string name, object? value = null, 
            SlotAction? insertAction = null,
            SlotAction? removeAction = null,
            SlotAction? needAction = null) :
            this(name, null, value)
        {
            InsertAction = insertAction;
            RemoveAction = removeAction;
            NeedAction = needAction;
        }

        public Slot(string name, Type? type = null, 
            object? value = null,
            SlotAction? insertAction = null,
            SlotAction? removeAction = null,
            SlotAction? needAction = null) :
            this(name, type, value)
        {
            InsertAction = insertAction;
            RemoveAction = removeAction;
            NeedAction = needAction;
        }

        public object Clone()
        {
            return new Slot(Name, Type, Value,
                InsertAction, RemoveAction, NeedAction);
        }
    }
}
