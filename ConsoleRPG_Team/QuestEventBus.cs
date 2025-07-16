using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG_Team
{

    public static class QuestEventBus
    {
        static private Dictionary<int , List<Action>> eventHandlers = new Dictionary<int, List<Action>>();

        static public void Subscribe(int EventID, Action action)
        {
            if (!eventHandlers.ContainsKey(EventID))
            {
                eventHandlers[EventID] = new List<Action>();
            }
            eventHandlers[EventID].Add(action);
        }

        static public void Unsubscribe(int EventID, Action action)
        {
            if (eventHandlers.ContainsKey(EventID))
            {
                eventHandlers[EventID].Remove(action);
            }
        }

        static public void Publish(int EventID)
        {
            if (eventHandlers.ContainsKey(EventID))
            {
                List<Action> handlers = eventHandlers[EventID];
                foreach (Action handler in handlers)
                    handler.Invoke();
            }
        }
    }
}
