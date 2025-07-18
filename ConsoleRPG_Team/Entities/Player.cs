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
        protected int BonusAtk { get; set; }
        protected int BonusDef { get; set; }

        public int gold { get; set; }
        public int criticalPro { get; set; } //치명타확률
        public int getExp { get; set; } // 전투후 획득 경험치 표시용
        public int maxExp => levelExp[level]; // Status 표시용
        public Quest? currentQuest { get; set; } = null;

        public bool chooseClass { get; protected set; } = false;

        protected int[] levelExp = { 0, 10, 35, 65, 100 }; // 레벨별 필요 경험치

        public PlayerClass playerClass { get; protected set; }

        public List<Item> inventory = new List<Item>();


        public Player()
        {
            name = "주인공"; // 추후 입력해서 설정
            level = 1;
            atk = 5;
            def = 1;
            health = 100;
            mana = 50;
            maxMana = 50;
            beforeHealth = null;
            playerClass = PlayerClass.None;
            maxHealth = 100;
            gold = 1500;
            exp = 0;
            getExp = 0;
            isDead = false;
            criticalPro = 15;
        }

        public Player(Player p)
        {
            name = p.name;
            level = p.level;
            atk = p.atk;
            def = p.def;
            health = p.health;
            mana = p.mana;
            maxMana = p.maxMana;
            beforeHealth = p.beforeHealth;
            playerClass = p.playerClass;
            maxHealth = p.maxHealth;
            gold = p.gold;
            exp = p.exp;
            isDead = p.isDead;
            criticalPro = p.criticalPro;
            inventory = p.inventory;
        }

        public override int AtkDiff()
        {
            int atk = GetATK();
            int fluctuation = Math.Max(1, (int)Math.Round(atk * 0.1f));
            int randomAtk = random.Next(atk - fluctuation, atk + fluctuation + 1);

            int criticalChance = random.Next(1, 101);
            if (criticalPro > criticalChance)
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

                if(!isSelect)
                {
                    Console.WriteLine("숫자를 입력해주세요.");
                    continue;
                }

                if (select == 0)
                    break;

                if (select > inventory.Count || select < 0)
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
        public bool UseItemInBattle()
        {
            var items = inventory.Where(item => item is UseableItem && item.item_Type == ItemType.Consumable).ToList();

            if(items.Count == 0)
            {
                Console.WriteLine("사용 가능한 아이템이 없습니다.");
                return false;
            }

            Console.WriteLine("사용할 아이템을 선택하세요.");
            for(int i = 0; i < items.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {items[i].item_Name} (수량 : {items[i].item_quantity})");
            }
            Console.WriteLine("0. 취소");

            do
            {
                Console.WriteLine("번호를 입력하세요.");
                int select;
                bool isNum = int.TryParse(Console.ReadLine(), out select);
                if (!isNum || select == 0)
                {
                    Console.WriteLine("아이템 사용을 취소합니다.");
                    return false;
                }
                else if (select >= 1 && select <= items.Count)
                {
                    UseConsumable(inventory.IndexOf(items[select - 1]) + 1);
                    return true;
                }
            } while (true);     
        }

        private void UseConsumable(int select)
        {
            UseableItem item = inventory[select - 1] as UseableItem;
            if (item != null)
            {
                item.use(this);
                item.item_quantity -= 1;

                if (item.item_quantity <= 0)
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

            if (!selectItem.item_isEquiped)
            {
                foreach (Item item in inventory)
                {
                    if (item.item_isEquiped && item.item_Type == selectItem.item_Type)
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

        public void UpdateStat()
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
        }


        public virtual bool UseSkill(List<Enemy> targets)
        {
            if (targets.Count != 1)
            {
                Console.WriteLine("잘못된 대상 지정입니다.");
                return false;
            }
            Enemy enemy = targets[0];
            if (this.mana < 20)
            {
                Console.WriteLine($"{name}은 마나가 부족해서 스킬을 사용할수 없다.");
                return false;
            }

            mana -= 20;
            int skillDamage = NoneSKill();

            Console.WriteLine($"[강하게 때리기] {name}이 최선을 다해 공격을 했다.");
            Console.WriteLine();
            Console.WriteLine("{0:5} 의 강하게 때리기!!", this.name);
            Attack(enemy, skillDamage, true);
            Console.WriteLine();
            return true;
        }

        private int NoneSKill()
        {
            int dmg = (int)(AtkDiff() * 1.2f);
            return dmg;
        }


        public void GetItem(Item item)
        {
            var sameItem = inventory.FirstOrDefault(i => i.item_ID == item.item_ID);

            if (sameItem != null && item.item_Type == ItemType.Consumable)
            {
                sameItem.item_quantity += 1;
            }
            else
            {
                if (item is UseableItem)
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
                        item_quantity = 1,
                        healAmount = (item as UseableItem)?.healAmount ?? 0,
                        manaAmount = (item as UseableItem)?.manaAmount ?? 0
                    };
                    inventory.Add(newItem);
                }
                else
                {
                    Item newItem = new Item()
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

        public void ChangeName()
        {
            Console.WriteLine("바꿀 이름을 작성해주세요.");
            string name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("이름은 비어있을 수 없습니다.");
                return;
            }

            if(name.Length > 10)
            {
                Console.WriteLine("이름이 너무 깁니다. 10자 이내로 줄여주세요.");
                return;
            }

            this.name = name;
        }
    }
}
