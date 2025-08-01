﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleRPG_Team.Store_Item;

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
        public int SubID;

        public QuestID(QuestType T, int id)
        {
            this.Type = T;
            this.SubID = id;
        }
    };

    public struct QuestInfo
    {
        public QuestID questID { get; private set; }
        public int finalProgress { get; set; }
        public string name { get; set; }
        public string description { get; set; }

        public int rewardGold { get; private set; }
        public Item? rewardItem { get; set; }

        //public Reward
        public QuestInfo(QuestID q, int finalprogress, string name, string description, int rewardG, int? rewardItemID = null)
        {
            this.questID = q;
            this.finalProgress = finalprogress;
            this.name = name;
            this.description = description;
            this.rewardGold = rewardG;
            if (rewardItemID != null && rewardItemID > 0 && rewardItemID <= Item.items.Length)
                this.rewardItem = Item.items[(int)rewardItemID-1];
            else
                this.rewardItem = null;
        }


        public static readonly Dictionary<int, QuestInfo> QUESTCATEGORY = new Dictionary<int, QuestInfo>()
        {
            {0 , new QuestInfo(new QuestID(QuestType.BattleWin, 0), 1, "적과의 경험", "전투에서 1회 승리하세요", 250)},
            {1 , new QuestInfo(new QuestID(QuestType.BattleWin, 0), 3, "적 소탕작전", "전투에서 3회 승리하세요", 500, 1) },
            {2 , new QuestInfo(new QuestID(QuestType.BattleWin, 0), 5, "적 말살작전", "전투에서 5회 승리하세요", 1000, 3) },
            {3 , new QuestInfo(new QuestID(QuestType.SlainEnemy, 0), 3, "미니언 처치", "미니언 세마리를 잡으세요", 500, 2) },
            {4 , new QuestInfo(new QuestID(QuestType.SlainEnemy, 1), 3, "공허충 처치", "공허충 세마리를 잡으세요", 1000, 11) },
            {5 , new QuestInfo(new QuestID(QuestType.SlainEnemy , 2), 3, "대포미니언 처치", "대포미니언 세마리를 잡으세요", 1500, 10) }
        };
    }


    internal class Quest
    {
        public int serialNumber {  get; private set; }
        public QuestInfo questInfo {  get; private set; }
        public bool isActive { get; private set; } = false;
        public bool isClear { get; protected set; } = false;
        public int currentProgress { get; protected set; } = 0;

        Action questAction;

        public Quest()
        {
            throw new NotImplementedException();
        }
        public Quest(int serialNum)
        {
            this.serialNumber = serialNum;
            if (!QuestInfo.QUESTCATEGORY.ContainsKey(this.serialNumber))
                throw new NotImplementedException();
            this.questInfo = QuestInfo.QUESTCATEGORY[this.serialNumber];
            this.isActive = false;
            this.isClear = false;
            this.currentProgress = 0;
            this.questAction = OnProgress;
        }
        public void OnQuestSelected()
        {
            if (GameManager.playerInstance.currentQuest != null)
                GameManager.playerInstance.currentQuest.ResetQuest();
            this.isActive = true;
            GameManager.playerInstance.currentQuest = this;
            QuestEventBus.Subscribe(this.questInfo.questID, this.questAction);
        }

        protected void ResetQuest()
        {
            this.currentProgress = 0;
            this.isActive = false;
            this.isClear = false;
            QuestEventBus.Unsubscribe(this.questInfo.questID, questAction);
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

        public void RecieveReward()
        {
            this.isActive = false;
            if (questInfo.rewardItem != null)
                GameManager.playerInstance.GetItem(new(this.questInfo.rewardItem));
            GameManager.playerInstance.gold += this.questInfo.rewardGold;
            QuestEventBus.Unsubscribe(this.questInfo.questID, this.questAction);
            GameManager.playerInstance.currentQuest = null;
        }
    }
}
