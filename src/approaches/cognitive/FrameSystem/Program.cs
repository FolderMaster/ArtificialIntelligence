using FrameSystem;

var logger = new ConsoleLogger();

var slots = new List<Slot>()
{
    new Slot("Царство", typeof(string)),
    new Slot("Питание", typeof(string), (s) => s.Slot.Value,
    (s) => s.Slot.Value, (s) =>
    {
        var feed = null as string;
        var preys = s.Frame.GetValue("Питаются") as IEnumerable<Frame>;
        if(preys != null)
        {
            foreach(var prey in preys)
            {
                var regnum = prey.GetValue("Царство") as string;
                if (regnum == "Растение")
                {
                    if (feed != "Плотоядное" && feed != "Всеядное")
                    {
                        feed = "Травоядное";
                    }
                    else
                    {
                        feed = "Всеядное";
                    }
                }
                else if (regnum == "Животное")
                {
                    if (feed != "Травоядное" && feed != "Всеядное")
                    {
                        feed = "Плотоядное";
                    }
                    else
                    {
                        feed = "Всеядное";
                    }
                }
            }
        }
        return feed;
    }),
    new Slot("Питаются", typeof(IEnumerable<Frame>)),
};

var bioFrame = new Frame("Живые организмы", slots)
{
    Logger = logger
};

var dandelionFrame = new Frame("Одуванчик", parent: bioFrame);
var rabbitFrame = new Frame("Кролик", parent: bioFrame);
var kiteFrame = new Frame("Коршун", parent: bioFrame);
var bearFrame = new Frame("Медведь", parent: bioFrame);

logger.Log("Настройка одуванчика");
dandelionFrame.SetValue("Царство", "Растение");
dandelionFrame.SetValue("Питаются", new List<Frame>());

logger.Log();
logger.Log("Настройка кролика");
rabbitFrame.SetValue("Царство", "Животное");
rabbitFrame.SetValue("Питаются", new List<Frame>()
{
    dandelionFrame
});

logger.Log();
logger.Log("Настройка коршуна");
kiteFrame.SetValue("Царство", "Животное");
kiteFrame.SetValue("Питаются", new List<Frame>()
{
    rabbitFrame
});

logger.Log();
logger.Log("Настройка медведя");
bearFrame.SetValue("Царство", "Животное");
bearFrame.SetValue("Питаются", new List<Frame>()
{
    dandelionFrame, rabbitFrame
});

logger.Log();
logger.Log("Питание одуванчика");
dandelionFrame.GetValue("Питание");

logger.Log();
logger.Log("Питание кролика");
rabbitFrame.GetValue("Питание");

logger.Log();
logger.Log("Питание коршуна");
kiteFrame.GetValue("Питание");

logger.Log();
logger.Log("Питание медведя");
bearFrame.GetValue("Питание");
