using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG_Team.Entity
{
    internal class Player : Entity
    {
        private int def;
        private int gold;

        

        public Player()
        {
            name = "주인공";
        }

        public override void Attack(Entity target)
        {
            int randomAtk = AtkDiff();
            target.Health -= randomAtk;
            Console.WriteLine($"{target.name}의 체력을 {randomAtk} 깎았습니다.");
        }
    }
}
