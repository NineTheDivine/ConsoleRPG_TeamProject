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

        public Store()
        {
            Item item = new Item();
            storeItems.AddRange(Item.items);
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

                    var sameItem = GameManager.playerInstance.inventory.FirstOrDefault(i => i.item_ID == buyItem.item_ID);

                    if (sameItem != null && buyItem.item_Type == ItemType.Consumable)
                    {
                        sameItem.item_quantity += 1;
                    }
                    else
                    {
                        UseableItem newItem = new UseableItem()
                        {
                            item_ID = buyItem.item_ID,
                            item_Name = buyItem.item_Name,
                            item_Pow = buyItem.item_Pow,
                            item_Description = buyItem.item_Description,
                            item_Type = buyItem.item_Type,
                            item_Price = buyItem.item_Price,
                            item_Grade = buyItem.item_Grade,
                            item_quantity = 1
                        };
                        GameManager.playerInstance.inventory.Add(newItem);
                    }

                    Console.WriteLine($"{buyItem.item_Name} 을 구매했습니다.");

                    if (buyItem.item_Type != ItemType.Consumable)
                    {
                        storeItems.RemoveAt(select - 1);
                    }
                }
                else
                {
                    Console.WriteLine($"{GameManager.playerInstance.gold - buyItem.item_Price}G 가 부족합니다.");
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
                }

                Console.WriteLine($"{inven[select - 1].item_Price / 2}G를 받았습니다.");
                GameManager.playerInstance.gold += inven[select - 1].item_Price / 2;
                storeItems.Add(inven[select - 1]);
                inven.RemoveAt(select - 1);

            }
        }
    }
}
