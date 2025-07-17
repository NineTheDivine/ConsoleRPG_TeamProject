using ConsoleRPG_Team.Store_Item;
using ConsoleRPG_Team.Quests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

public enum PlayerClass
{
    None,
    Warrior,
    Mage,
    Rogue
}

namespace ConsoleRPG_Team.Entities
{
    internal class Player : Entity
    {
        public int? beforeHealth { get; set; } = null;
        public int def { get; protected set; }
        private int BonusAtk { get; set; }
        private int BonusDef { get; set; }
            
        public int gold { get; set; }
        public int criticalPro { get; set; } //치명타확률
        public int getExp { get; set; } // 전투후 획득 경험치 표시용
        public int maxExp => levelExp[level]; // Status 표시용
        public Quest? currentQuest { get; set; } = null;

        bool chooseClass = false;

        private int[] levelExp = { 0, 10, 35, 65, 100 }; // 레벨별 필요 경험치

        public PlayerClass playerClass { get; protected set; }

        public List<Item> inventory = new List<Item>();


        public Player()
        {
            name = "주인공"; // 추후 입력해서 설정
            level = 1;
            atk = 5;
            def = 5;
            health = 100;
            beforeHealth = null;
            playerClass = PlayerClass.None;
            maxHealth = 100;
            gold = 1500;
            exp = 0;
            getExp = 0;
            isDead = false;
            criticalPro = 15;
        }

        public override void Attack(Entity target)
        {
            base.Attack(target);
            if (target is Enemy && target.isDead)
            {
                Enemy e = target as Enemy;
                QuestEventBus.Publish(new QuestID(QuestType.SlainEnemy, (int)e.enemyType));
            }
        }
        public override int AtkDiff()
        {
            int atk = GetATK();
            int fluctuation = Math.Max(1, (int)Math.Round(atk * 0.1f));
            int randomAtk = random.Next(atk - fluctuation, atk + fluctuation + 1);

            int criticalChance = random.Next(1, 101);
            if(criticalPro > criticalChance)
            {
                Console.WriteLine("치명타!");
                randomAtk = (int)(randomAtk * 1.6f);       
            }
            return randomAtk;
        }

        public void ShowInventory()
        {
            int i = 1;

            Console.WriteLine("현재 보유중인 아이템");
            Console.WriteLine("=============================================================================");
            Console.WriteLine("|ID|장착|        이름        | 능력치 |             설명             |");
            Console.WriteLine("-----------------------------------------------------------------------------");
            foreach (Item item in GameManager.playerInstance.inventory)
            {
                string equipState = item.item_isEquiped ? "[E]" : "  ";
                string quantityNum = item.item_quantity > 1 ? $"x {item.item_quantity}" : "";
                string upgradeNum = item.item_Upgrade >= 1 ? $"+ {item.item_Upgrade}" : "";

                Console.Write($"|{i}| {equipState} |");
                Console.ForegroundColor = item.GetGradeColor();
                Console.Write($" {item.item_Name + quantityNum + upgradeNum,-15}");
                Console.ResetColor();
                Console.WriteLine($" | {item.item_Pow,5} | {item.item_Description,-23} |");
                i++;
            }
            Console.WriteLine("=============================================================================");
        }

        public void InventorySystem()
        {
            while (true)
            {
                ShowInventory();

                Console.WriteLine("장착 or 해제 하시고 싶은 아이템을 선택해 주세요. 나가려면 0을 눌러주세요.");

                int select = 0;
                bool isSelect = int.TryParse(Console.ReadLine(), out select);

                if (select == 0)
                    break;

                if (!isSelect || select > inventory.Count || select < 0)
                {
                    Console.WriteLine("잘못된 입력");
                    continue;
                }

                Item selectItem = inventory[select - 1];

                if (selectItem.item_Type == ItemType.Consumable)
                {
                    UseConsumable(select);
                }

                else
                {
                    EquipItem(select);
                }
            }
        }

        private void UseConsumable(int select)
        {
            UseableItem item = inventory[select - 1] as UseableItem;
            if (item != null)
            {
                item.use(this);
                item.item_quantity -= 1;

                if(item.item_quantity <= 0)
                {
                    inventory.RemoveAt(select - 1);
                }
                Console.WriteLine($"{item.item_Name}을(를) 사용했습니다.");
            }
            else
            {
                Console.WriteLine("이게 뜨면 문제가있따");
            }
        }

        private void EquipItem(int select)
        {
            Item selectItem = inventory[select - 1];

            if(!selectItem.item_isEquiped)
            {
                foreach(Item item in inventory)
                {
                    if(item.item_isEquiped && item.item_Type == selectItem.item_Type)
                    {
                        item.item_isEquiped = false;
                        Console.WriteLine($"{item.item_Name}의 장착을 해제했습니다.");
                    }
                }
                selectItem.item_isEquiped = true;
                Console.WriteLine($"{selectItem.item_Name}을 장착했습니다.");
                UpdateStat();
            }
            else
            {
                selectItem.item_isEquiped = false;
                Console.WriteLine($"{selectItem.item_Name}을 장착을 해제했습니다.");
                UpdateStat();
            }
        }

        private void UpdateStat()
        {
            BonusAtk = 0;
            BonusDef = 0;

            foreach (Item item in inventory)
            {
                if (item.item_isEquiped)
                {
                    if (item.item_Type == ItemType.Weapon)
                    {
                        BonusAtk += item.item_Pow;
                    }
                    if (item.item_Type == ItemType.Armor)
                    {
                        BonusDef += item.item_Pow;
                    }
                }
            }
        }

        public int GetATK()
        {
            UpdateStat();
            return atk + BonusAtk;
        }

        public int GetDef()
        {
            UpdateStat();
            return def + BonusDef;
        }

        public void LevelUp()
        {
            while (level < levelExp.Length && exp >= levelExp[level])
            {
                exp -= levelExp[level];
                level++;
                atk += 1;
                def += 1;
                Console.WriteLine($"레벨업! 현재레벨{level} 공격력 방어력이 + 1 되었습니다.");
            }

            if (playerClass == PlayerClass.None && level >= 3)
            {
                GetClass();
            }
        }

        private void GetClass()
        {
            while (!chooseClass)

            {
                Console.WriteLine("전직할수 있습니다 어떤 직업으로 하시겠습니까?.");
                Console.WriteLine("1.전사 2.마법사 3.도적");

                int select = 0;
                bool isSelect = int.TryParse(Console.ReadLine(), out select);

                if (!isSelect)
                {
                    Console.WriteLine("숫자로 선택해주세요.");
                    continue;
                }

                switch (select)
                {
                    case 1:
                        playerClass = PlayerClass.Warrior;
                        chooseClass = true;
                        maxHealth += 10;
                        Console.WriteLine("최대체력 10 증가");
                        break;
                    case 2:
                        playerClass = PlayerClass.Mage;
                        chooseClass = true;
                        atk += 2;
                        Console.WriteLine("공격력 2증가");
                        break;
                    case 3:
                        playerClass = PlayerClass.Rogue;
                        chooseClass = true;
                        criticalPro += 10;

                        Console.WriteLine("치명타확률 10% 증가");
                        break;
                    default:
                        Console.WriteLine("당신은 아무 직업도 선택하지 않았다..");
                        chooseClass = true;
                        break;
                }
            }
        }

        public void GetItem(Item item)
        {
            var sameItem = inventory.FirstOrDefault(i => i.item_ID == item.item_ID);

            if(sameItem != null && item.item_Type == ItemType.Consumable)
            {
                sameItem.item_quantity += 1;
            }
            else
            {
                UseableItem newItem = new UseableItem()
                {
                    item_ID = item.item_ID,
                    item_Name = item.item_Name,
                    item_Pow = item.item_Pow,
                    item_Description = item.item_Description,
                    item_Type = item.item_Type,
                    item_Price = item.item_Price,
                    item_Grade = item.item_Grade,
                    item_quantity = 1
                };
                inventory.Add(newItem);
            }
        }
    }
}
