using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG_Team.Quests
{
    public enum QuestType
    {
        None = -1,
        Other = 0,
        BattleWin,
        SlainEnemy

    };

    public struct QuestID
    {
        public QuestType Type;
        public int ID;

        public QuestID(QuestType T, int id)
        {
            this.Type = T;
            this.ID = id;
        }
    };

    public struct QuestInfo
    {
        public int finalProgress { get; set; }
        public string name { get; set; }
        public string description { get; set; }

        //public Reward
        public QuestInfo(int finalprogress, string name, string description)
        {
            this.finalProgress = finalprogress;
            this.name = name;
            this.description = description;
        }


        public static readonly Dictionary<QuestID, QuestInfo> QUESTCATEGORY = new Dictionary<QuestID, QuestInfo>()
        {
            { new QuestID(QuestType.BattleWin, 0) , new QuestInfo(1, "적과의 경험", "전투에서 1회 승리하세요")},
            { new QuestID(QuestType.BattleWin, 1), new QuestInfo(3, "적 소탕작전", "전투에서 3회 승리하세요") },
            { new QuestID(QuestType.BattleWin, 2), new QuestInfo(5, "적 말살작전", "전투에서 5회 승리하세요") },
            { new QuestID(QuestType.SlainEnemy, 0), new QuestInfo(3, "미니언 처치", "미니언 세마리를 잡으세요") },
            { new QuestID(QuestType.SlainEnemy, 1), new QuestInfo(3, "공허충 처치", "공허충 세마리를 잡으세요") },
            { new QuestID(QuestType.SlainEnemy , 2), new QuestInfo(3, "대포미니언 처치", "대포미니언 세마리를 잡으세요") }
        };
    }


    internal class Quest
    {
        public QuestID questID { get; private set; }
        public QuestInfo questInfo {  get; private set; }
        bool isActive { get; set; } = false;
        public bool isClear { get; protected set; } = false;
        public int currentProgress { get; protected set; } = 0;

        Action questAction;

        public Quest()
        {
            throw new NotImplementedException();
        }
        public Quest(QuestID id)
        {
            this.questID = id;
            if (!QuestInfo.QUESTCATEGORY.ContainsKey(questID))
                throw new NotImplementedException();
            this.questInfo = QuestInfo.QUESTCATEGORY[questID];
            this.isActive = false;
            this.isClear = false;
            this.currentProgress = 0;
            this.questAction = OnProgress;
        }
        public void OnQuestSelected()
        {
            this.isActive = true;
            GameManager.activeQuest = this;
            QuestEventBus.Subscribe(this.questID, this.questAction);
        }
        protected void OnProgress()
        {
            if (!this.isActive || isClear)
                return;
            this.currentProgress++;
            if (this.currentProgress >= this.questInfo.finalProgress)
            {
                this.isClear = true;
            }
        }

        protected void RecieveReward()
        {
            QuestEventBus.Unsubscribe(this.questID, this.questAction);
        }
    }
}
