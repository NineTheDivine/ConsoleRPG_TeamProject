using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleRPG_Team.Quests;

namespace ConsoleRPG_Team
{

    public static class QuestEventBus
    {
        static private Dictionary<QuestID , List<Action>> eventHandlers = new Dictionary<QuestID, List<Action>>();

        static public void Subscribe(QuestID ID, Action action)
        {
            if (!eventHandlers.ContainsKey(ID))
            {
                eventHandlers[ID] = new List<Action>();
            }
            eventHandlers[ID].Add(action);
        }

        static public void Unsubscribe(QuestID ID, Action action)
        {
            if (eventHandlers.ContainsKey(ID))
            {
                eventHandlers[ID].Remove(action);
            }
        }

        static public void Publish(QuestID ID)
        {
            if (eventHandlers.ContainsKey(ID))
            {
                List<Action> actions = eventHandlers[ID];
                foreach (var action in actions)
                    action.Invoke();
            }
        }
    }
}
