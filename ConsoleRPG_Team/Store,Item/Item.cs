using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

public enum ItemType
{
    None,
    Weapon,
    Armor,
    Consumable
}
public enum ItemGrade
{
    Common = 1,
    Rare,
    Epic,
    Legendary
}
namespace ConsoleRPG_Team.Store_Item
{
    internal class Item
    {
        public int item_ID { get; set; }
        public string item_Name { get; set; }
        public int item_Pow { get; set; }
        public string item_Description { get; set; }
        public int item_Price { get; set; }

        public int item_quantity { get; set; } = 1; // 같은아이템 중첩되게
        public int item_Upgrade { get; set; } = 0; //업그레이드
        public ItemType item_Type { get; set; }

        public ItemGrade item_Grade { get; set; }

        public bool item_isEquiped = false;
        public bool item_isSelled = false;

        public readonly static Item[] items = new Item[10]
        {
            new Item(1, "녹슨 검", 5, "녹슬고 이가 다 나간 검.", 100, ItemType.Weapon , ItemGrade.Common),
            new Item(2, "강철 도끼", 8, "강철로 만든 무거운 도끼.", 150, ItemType.Weapon ,ItemGrade.Rare),
            new Item(3, "마법 지팡이", 6, "마력을 품은 오래된 지팡이.", 200, ItemType.Weapon , ItemGrade.Epic ),
            new Item(4, "엑스칼리버", 10, "전설에 등장하는 성검(聖劍)", 500, ItemType.Weapon , ItemGrade.Legendary),
            new Item(5, "가죽 방어구", 3, "가죽으로 만든 방어구.", 80, ItemType.Armor , ItemGrade.Common),
            new Item(6, "철제 방패", 5, "튼튼한 철로 만든 방패.", 120, ItemType.Armor , ItemGrade.Rare),
            new Item(7, "마법 망토", 2, "마법 저항을 올려주는 망토.", 250, ItemType.Armor, ItemGrade.Epic),
            new Item(8, "용의 비늘", 10, "용의 비늘로 만든 갑옷", 500, ItemType.Armor , ItemGrade.Legendary),
            new UseableItem(9, "체력 포션", "체력을 50 회복시켜준다.", 30, ItemType.Consumable, 50, 0),
            new UseableItem(10, "마나 포션", "마나를 30 회복시켜준다.", 30, ItemType.Consumable, 0, 30),
        };

        public Item(int ID, string name, int pow, string description, int price, ItemType type, ItemGrade grade)
        {
            item_ID = ID;
            item_Name = name;
            item_Pow = pow;
            item_Description = description;
            item_Price = price;
            item_Type = type;
            item_Grade = grade;
        }

        public Item()
        {
        }

        public ConsoleColor GetGradeColor()
        {
            return item_Grade switch
            {
                ItemGrade.Common => ConsoleColor.Gray,
                ItemGrade.Rare => ConsoleColor.Green,
                ItemGrade.Epic => ConsoleColor.Blue,
                ItemGrade.Legendary => ConsoleColor.Yellow,
                _ => ConsoleColor.White
            };
        }
    }
}