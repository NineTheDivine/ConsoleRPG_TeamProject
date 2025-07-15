using ConsoleRPG_Team.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private void ShowItems()
        {
            int i = 1;
            Console.WriteLine("=============================================================================");
            Console.WriteLine($"| ID |        이름        | 능력치 |  가격  |             설명             |\t보유 :{GameManager.playerInstance.gold}G");
            Console.WriteLine("-----------------------------------------------------------------------------");

            foreach (Item item in storeItems)
            {
                Console.WriteLine($"| {i , 2} | {item.item_Name,-14} | {item.item_Pow,5} | {item.item_Price,4}G | {item.item_Description,-23} |");
                i++;
            }
            Console.WriteLine("===============================================================================");


            //Console.WriteLine("어떤 아이템을 구매 하실건가요?");

        }
        public void Buy()
        {
            while(true)
            {
                ShowItems();

                Console.WriteLine("어떤 아이템을 구매 하실건가요?");
                int selected = int.Parse(Console.ReadLine());

                if (selected == 0)
                    break;

                if (selected > storeItems.Count)
                {
                    Console.WriteLine("잘못된 선택입니다.");
                    continue;
                }

                if (GameManager.playerInstance.gold > storeItems[selected - 1].item_Price)
                {
                    GameManager.playerInstance.gold -= storeItems[selected - 1].item_Price;

                    Console.WriteLine($"{storeItems[selected - 1].item_Name} 을 구매했습니다.");

                    GameManager.playerInstance.inventory.Add(storeItems[selected - 1]);
                    storeItems.RemoveAt(selected - 1);
                }
                else
                {
                    Console.WriteLine($"{GameManager.playerInstance.gold - storeItems[selected - 1].item_Price}G 가 부족합니다.");
                }
            }
        }
                
        public void Sell()
        {

        }
    }
}
