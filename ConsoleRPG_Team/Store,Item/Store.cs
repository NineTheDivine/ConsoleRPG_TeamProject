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
            Console.WriteLine("=============================================================================");
            Console.WriteLine("| ID |        이름        | 능력치 |  가격  |             설명             |");
            Console.WriteLine("-----------------------------------------------------------------------------");
            foreach (Item item in storeItems)
            {
                Console.WriteLine($"| {item.item_ID,2} | {item.item_Name,-14} | {item.item_Pow,5} | {item.item_Price,4}G | {item.item_Description,-23} |");
            }
            Console.WriteLine("===============================================================================");
        }
        public void Buy()
        {
            
        }

        public void Sell()
        {

        }
    }
}
