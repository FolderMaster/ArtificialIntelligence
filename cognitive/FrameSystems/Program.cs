using FrameSystem.Frames;
using FrameSystem.Logger;

var logger = new ConsoleLogger();

void UpdateFeedSlot(Frame predator,
    IEnumerable<Frame>? preys, ILogger? logger)
{
    var feed = (Feed?)null;
    if (preys != null)
    {
        foreach (var prey in preys)
        {
            var oldFeed = feed;
            var regnum = (Regnum?)prey.NeedValue("Царство");
            if (regnum == Regnum.Plant || regnum == Regnum.Mushroom)
            {
                if (feed != Feed.Carnivore &&
                    feed != Feed.Omnivore)
                {
                    feed = Feed.Herbivore;
                }
                else
                {
                    feed = Feed.Omnivore;
                }
            }
            else if (regnum == Regnum.Animal)
            {
                if (feed != Feed.Herbivore &&
                    feed != Feed.Omnivore)
                {
                    feed = Feed.Carnivore;
                }
                else
                {
                    feed = Feed.Omnivore;
                }
            }
            logger?.Log($"Feed set to {feed}, because regnum is " +
                $"{regnum} and old feed is {oldFeed}",
                new Dictionary<string, object>()
                {
                    ["Background"] = ConsoleColor.DarkBlue,
                    ["Foreground"] = ConsoleColor.White
                });
        }
    }
    predator.UpdateValue("Питание", feed);
}

var slots = new List<Slot>()
{
    new Slot("Царство", typeof(Regnum),
        afterUpdateAction: (args) =>
        {
            foreach (var child in args.Frame.Parent.Children)
            {
                var preys = child.NeedValue("Питаются")
                    as IEnumerable<Frame>;
                if (preys != null)
                {
                    if (preys.Contains(args.Frame))
                    {
                        UpdateFeedSlot(child, preys, logger);
                    }
                }
            }
        }),
    new Slot("Питание", typeof(Feed), needFunction: (args) =>
    {
        if (args.Value != null)
        {
            return args.Value;
        }
        var preys = args.Frame.NeedValue("Питаются")
            as IEnumerable<Frame>;
        var feed = (Feed?)null;
        if (preys == null ? true : !preys.Any())
        {
            feed = Feed.Producer;
            logger?.Log($"Feed set to {feed}, " +
                $"because preys' count is 0",
                new Dictionary<string, object>()
                {
                    ["Background"] = ConsoleColor.DarkBlue,
                    ["Foreground"] = ConsoleColor.White
                });
        }
        else
        {
            feed = Feed.Reducer;
            logger?.Log($"Feed set to {feed}, " +
                $"because preys' count is more than 0",
                new Dictionary<string, object>()
                {
                    ["Background"] = ConsoleColor.DarkBlue,
                    ["Foreground"] = ConsoleColor.White
                });
        }
        return feed;
    }),
    new Slot("Питаются", typeof(IEnumerable<Frame>),
        afterUpdateAction: (args) => UpdateFeedSlot
            (args.Frame, args.Value as IEnumerable<Frame>, args.Logger))
};

var bioFrame = new Frame("Живые организмы:", slots, logger);

var dandelionFrame = new Frame("Одуванчик", parent: bioFrame);
var rabbitFrame = new Frame("Кролик", parent: bioFrame);
var kiteFrame = new Frame("Коршун", parent: bioFrame);
var bearFrame = new Frame("Медведь", parent: bioFrame);
var bacteriaFrame = new Frame("Бактерия", parent: bioFrame);

rabbitFrame.UpdateValue("Питаются", new LogList<Frame>()
{
    dandelionFrame
});
kiteFrame.UpdateValue("Питаются", new LogList<Frame>()
{
    rabbitFrame
});
bearFrame.UpdateValue("Питаются", new LogList<Frame>()
{
    dandelionFrame, rabbitFrame
});
bacteriaFrame.UpdateValue("Питаются", new LogList<Frame>()
{
    dandelionFrame, rabbitFrame
});

logger.Log(null);
dandelionFrame.UpdateValue("Царство", Regnum.Plant);
rabbitFrame.UpdateValue("Царство", Regnum.Animal);
kiteFrame.UpdateValue("Царство", Regnum.Animal);
bearFrame.UpdateValue("Царство", Regnum.Animal);
bacteriaFrame.UpdateValue("Царство", Regnum.Bacteria);

logger.Log(null);
dandelionFrame.NeedValue("Питание");
rabbitFrame.NeedValue("Питание");
kiteFrame.NeedValue("Питание");
bearFrame.NeedValue("Питание");
bacteriaFrame.NeedValue("Питание");

public enum Feed
{
    Carnivore,
    Omnivore,
    Herbivore,
    Producer,
    Reducer
}

public enum Regnum
{
    Animal,
    Plant,
    Bacteria,
    Mushroom
}

public class LogList<T> : List<T>
{
    public override string ToString() =>
        "[" + string.Join(", ", this) + "]";
}