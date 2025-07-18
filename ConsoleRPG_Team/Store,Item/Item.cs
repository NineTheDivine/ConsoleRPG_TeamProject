using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
    public class Item
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

        public readonly static Item[] items = new Item[30]
        {
            new UseableItem(1, "체력 포션", "체력을 50 회복시켜준다.", 100, ItemType.Consumable, 50, 0),
            new UseableItem(2, "마나 포션", "마나를 30 회복시켜준다.", 100, ItemType.Consumable, 0, 30),

            new Item(3, "낡은 단검", 1, "쉽게 구할 수 있는 단검.", 500, ItemType.Weapon, ItemGrade.Common),
            new Item(4, "짧은 검", 2, "초보자가 쓰는 짧은 검.", 1000, ItemType.Weapon, ItemGrade.Common),
            new Item(5, "녹슨 검", 1, "녹슬고 이가 다 나간 검.", 500, ItemType.Weapon, ItemGrade.Common),
            new Item(6, "가죽 방어구", 1, "가죽으로 만든 방어구.", 500, ItemType.Armor, ItemGrade.Common),
            new Item(7, "가죽 모자", 1, "가죽으로 만든 간단한 모자.", 500, ItemType.Armor, ItemGrade.Common),
            new Item(8, "천 조끼", 1, "가벼운 천으로 만든 조끼.", 500, ItemType.Armor, ItemGrade.Common),
            new Item(9, "나무 방패", 1, "간단한 나무 방패.", 500, ItemType.Armor, ItemGrade.Common),
            new Item(10, "사냥용 활", 2, "기본 사냥용 활.", 1000, ItemType.Weapon, ItemGrade.Common),
            new Item(11, "낡은 활", 1, "오래된 활.", 500, ItemType.Weapon, ItemGrade.Common),
            new Item(12, "낡은 투구", 1, "오래된 투구.", 500, ItemType.Armor, ItemGrade.Common),

            new Item(13, "강철 도끼", 3, "강철로 만든 무거운 도끼.", 2000, ItemType.Weapon, ItemGrade.Rare),
            new Item(14, "철제 방패", 3, "튼튼한 철로 만든 방패.", 2000, ItemType.Armor, ItemGrade.Rare),
            new Item(15, "은빛 방패", 4, "은으로 만든 튼튼한 방패.", 2500, ItemType.Armor, ItemGrade.Rare),
            new Item(16, "그림자 단검", 4, "어둠을 두른 단검.", 2500, ItemType.Weapon, ItemGrade.Rare),
            new Item(17, "철 갑옷", 3, "단단한 철로 된 갑옷.", 3000, ItemType.Armor, ItemGrade.Rare),
            new Item(18, "화염 지팡이", 5, "불의 힘을 지닌 지팡이.", 3500, ItemType.Weapon, ItemGrade.Rare),
            new Item(19, "스틸 해머", 7, "강철로 만들어진 무거운 망치.", 4500, ItemType.Weapon, ItemGrade.Rare),
            new Item(20, "독의 단검", 4, "독이 묻어있는 단검.", 3500, ItemType.Weapon, ItemGrade.Rare),

            new Item(21, "마법 지팡이", 6, "마력을 품은 오래된 지팡이.", 5000, ItemType.Weapon, ItemGrade.Epic),
            new Item(22, "마법 망토", 6, "마법 저항을 올려주는 망토.", 5000, ItemType.Armor, ItemGrade.Epic),
            new Item(23, "마나 로브", 7, "마법사가 즐겨 입는 로브.", 5500, ItemType.Armor, ItemGrade.Epic),
            new Item(24, "명검 플레어", 8, "불꽃의 힘이 깃든 명검.", 8000, ItemType.Weapon, ItemGrade.Epic),
            new Item(25, "빙결 검", 6, "적을 얼리는 검.", 7000, ItemType.Weapon, ItemGrade.Epic),
            new Item(26, "사파이어 방패", 5, "사파이어가 박힌 방패.", 4500, ItemType.Armor, ItemGrade.Epic),

            new Item(27, "엑스칼리버", 10, "전설에 등장하는 성검(聖劍)", 10000, ItemType.Weapon, ItemGrade.Legendary),
            new Item(28, "용의 비늘", 10, "용의 비늘로 만든 갑옷", 10000, ItemType.Armor, ItemGrade.Legendary),
            new Item(29, "피닉스 갑옷", 15, "피닉스의 불꽃이 깃든 갑옷.", 15000, ItemType.Armor, ItemGrade.Legendary),
            new Item(30, "드래곤 슬레이어", 20, "용을 베는 전설의 검.", 20000, ItemType.Weapon, ItemGrade.Legendary)
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

        public Item(Item item)
        {
            item_ID = item.item_ID;
            item_Name = item.item_Name;
            item_Pow =item.item_Pow;
            item_Description = item.item_Description;
            item_Price = item.item_Price;
            item_Type = item.item_Type;
            item_Grade = item.item_Grade;
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