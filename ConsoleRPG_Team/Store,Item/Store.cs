using ConsoleRPG_Team.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Formats.Asn1.AsnWriter;

namespace ConsoleRPG_Team.Store_Item
{
    internal class Store
    {
        public List<Item> storeItems = new List<Item>();

        private Random random = new Random();
        public Store()
        {
            StoreRandomItem();
        }

        private void StoreRandomItem()
        {
            storeItems.Clear();

            storeItems.Add(Item.items[0]);
            storeItems.Add(Item.items[1]);

            int maxCount = 8;

            List<Item> allItems = Item.items.ToList();

            for (int i = 0; i < maxCount; i++)
            {
                int ran = random.Next(1, 101);
                ItemGrade selectedGrade;

                if (ran <= 50)
                    selectedGrade = ItemGrade.Common;
                else if (ran <= 80)
                    selectedGrade = ItemGrade.Rare;
                else if (ran <= 95)
                    selectedGrade = ItemGrade.Epic;
                else
                    selectedGrade = ItemGrade.Legendary;

                var gradeItems = allItems.Where(item => item.item_Grade == selectedGrade).ToList();

                if (gradeItems.Count > 0)
                {
                    int index = random.Next(0, gradeItems.Count);
                    storeItems.Add(gradeItems[index]);
                    allItems.Remove(gradeItems[index]);
                }
                else
                {
                    i--; 
                }
            }
        }
        public void ShowItems()
        {
            int i = 1;
            Console.WriteLine("=============================================================================");
            Console.WriteLine($"| ID |        이름        | 능력치 |  가격  |             설명             |\t보유 :{GameManager.playerInstance.gold}G");
            Console.WriteLine("-----------------------------------------------------------------------------");

            foreach (Item item in storeItems)
            {
                Console.Write($"| {i,2} |");
                Console.ForegroundColor = item.GetGradeColor();
                Console.Write($" {item.item_Name,-14}");
                Console.ResetColor();
                Console.WriteLine($" | {item.item_Pow,5} | {item.item_Price,4}G | {item.item_Description,-23} |");
                i++;
            }
            Console.WriteLine("===============================================================================");
        }
        public void Buy()
        {
            while (true)
            {
                ShowItems();

                Console.WriteLine("어떤 아이템을 구매 하실건가요? 나가려면 0을 눌러주세요.");

                int select = 0;

                bool isSelect = int.TryParse(Console.ReadLine(), out select);

                if (!isSelect)
                {
                    Console.WriteLine("숫자를 입력해 주세요.");
                    continue;
                }

                if (select == 0)
                    break;

                if (select > storeItems.Count)
                {
                    Console.WriteLine("잘못된 선택입니다.");
                    continue;
                }

                Item buyItem = storeItems[select - 1];

                if (GameManager.playerInstance.gold >= buyItem.item_Price)
                {
                    GameManager.playerInstance.gold -= buyItem.item_Price;

                    GameManager.playerInstance.GetItem(buyItem);

                    Console.WriteLine($"{buyItem.item_Name} 을 구매했습니다.");

                    if (buyItem.item_Type != ItemType.Consumable)
                    {
                        storeItems.RemoveAt(select - 1);
                    }
                }
                else
                {
                    Console.WriteLine($"돈이 부족합니다. {buyItem.item_Price - GameManager.playerInstance.gold}G 가 더 필요합니다.");
                }
            }
        }

        public void Sell()
        {
            while (true)
            {
                GameManager.playerInstance.ShowInventory();

                List<Item> inven = GameManager.playerInstance.inventory;

                Console.WriteLine("판매하고 싶은 아이템을 선택해주세요. 나가려면 0을 눌러주세요.");
                int select = 0;
                bool isSelect = int.TryParse(Console.ReadLine(), out select);

                if (inven.Count == 0)
                {
                    Console.WriteLine("팔 아이템이 없습니다.");
                    break;
                }

                if (!isSelect)
                {
                    Console.WriteLine("숫자를 입력해주세요.");
                    continue;
                }

                if (select == 0)
                {
                    Console.WriteLine("상점에서 나갑니다.");
                    break;
                }


                if (select > inven.Count || select < 0)
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    continue;
                }


                if (inven[select - 1].item_isEquiped)
                {
                    inven[select - 1].item_isEquiped = false;
                    GameManager.playerInstance.UpdateStat();
                }

                Console.WriteLine($"{inven[select - 1].item_Price / 2}G를 받았습니다.");
                GameManager.playerInstance.gold += inven[select - 1].item_Price / 2;

                if (inven[select - 1].item_quantity > 1)
                {
                    inven[select - 1].item_quantity--;
                }
                else
                {
                    inven.RemoveAt(select - 1);
                }            
            }
        }

        public void Upgrade()
        {
            while(true)
            {
                GameManager.playerInstance.ShowInventory();

                List<Item> inven = GameManager.playerInstance.inventory;

                Console.WriteLine($"보유 {GameManager.playerInstance.gold}G");

                Console.WriteLine("업그레이드 하고싶은 아이템을 선택해주세요. 나가려면 0을 눌러주세요.");
                int select = 0;
                bool isSelect = int.TryParse(Console.ReadLine(), out select);


                if (!isSelect)
                {
                    Console.WriteLine("숫자를 입력해주세요.");
                    continue;
                }

                if (select == 0)
                {
                    break;
                }
                        
                if (select > inven.Count || select < 0)
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    continue;
                }

                Item chooseItem = inven[select - 1];

                if(chooseItem.item_Type == ItemType.Consumable)
                {
                    Console.WriteLine("이 아이템은 강화가 불가능합니다.");
                    continue;
                }

                int baseCost = chooseItem.item_Price / 2;
                int baseSuccess = 80;

                ItemGrade grade = chooseItem.item_Grade;

                int gradeCost = grade switch
                {
                    ItemGrade.Common => 1,
                    ItemGrade.Rare => 2,
                    ItemGrade.Epic => 3,
                    ItemGrade.Legendary => 4,
                    _ => 1
                };

                int gradeSuccess = grade switch
                {
                    ItemGrade.Common => 0,
                    ItemGrade.Rare => - 5,
                    ItemGrade.Epic => - 10,
                    ItemGrade.Legendary => - 15,
                    _ => 0
                };

                int upgradeCost = baseCost + chooseItem.item_Upgrade * 100 * gradeCost;
                int successChance = baseSuccess - chooseItem.item_Upgrade * 10 + gradeSuccess;
                successChance = Math.Clamp(successChance, 5, 95);

                if(GameManager.playerInstance.gold < upgradeCost)
                {
                    Console.WriteLine("돈이 부족합니다.");
                    continue;
                }

                Console.WriteLine($"강화 비용: {upgradeCost}G, 성공 확률: {successChance}% 입니다. 강화하시겠습니까(1/0)?");
                int upgradeCheck = 0;
                bool isUpgrade = int.TryParse(Console.ReadLine(), out upgradeCheck);

                if(!isUpgrade || upgradeCheck != 1)
                {
                    Console.WriteLine("강화를 취소했습니다.");
                    continue;
                }


                GameManager.playerInstance.gold -= upgradeCost;

                int ran = random.Next(1, 101);

                if(ran <= successChance)
                {
                    int upgradePow = chooseItem.item_Grade switch
                    {
                        ItemGrade.Common => 1,
                        ItemGrade.Rare => 2,
                        ItemGrade.Epic => 3,
                        ItemGrade.Legendary => 4,
                        _ => 1
                    };

                    chooseItem.item_Upgrade++;
                    chooseItem.item_Pow += upgradePow;

                    Console.WriteLine($"축하드립니다! {chooseItem.item_Name} +{chooseItem.item_Upgrade}강이 되었습니다. 능력치 +{upgradePow}");           
                }
                else
                {
                    Console.WriteLine("강화 실패!");
                }         
            }  
        }
    }
}
