using ConsoleRPG_Team.Store_Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG_Team.Entities
{
    internal class Player : Entity
    {
        public int? beforeHealth { get; set; } = null;
        public int def { get; protected set; }
        public int gold { get; set; }

        public int getExp {  get; set; }

        public List<Item> inventory = new List<Item>();

        public Player()
        {
            name = "주인공"; // 추후 입력해서 설정
            level = 1;
            atk = 15;
            def = 5;
            health = 100;
            beforeHealth = null;
            maxHealth = 100;
            gold = 1500;
            exp = 0;
            getExp = 0;
            isDead = false;
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
                Console.WriteLine($"|{i}| {equipState} | {item.item_Name,-15} | {item.item_Pow,5} | {item.item_Description,-23} |");
                i++;
            }
            Console.WriteLine("=============================================================================");
        }

        public void EquipItem()
        {
            while(true)
            {
                ShowInventory();

                Console.WriteLine("장착 or 해제 하시고 싶은 아이템을 선택해 주세요. 나가려면 0을 눌러주세요.");

                int select = 0;
                bool isSelect = int.TryParse(Console.ReadLine(), out select);

                if (select == 0)
                    break;

                if (select > inventory.Count || select < 0)
                {
                    Console.WriteLine("잘못된 입력");
                    continue;
                }

                    inventory[select - 1].item_isEquiped = !inventory[select - 1].item_isEquiped;

            }
        }

        public void LevelUp()
        {
            int[] levelExp = { 0, 10, 35, 65, 100 };

            while(level < levelExp.Length && exp >= levelExp[level])
            {
                exp -= levelExp[level];
                level++;
                atk += 1;
                def += 1;
                Console.WriteLine($"레벨업! 현재레벨{level} 공격력 방어력이 + 1 되었습니다.");
            }
        }
    }
}
