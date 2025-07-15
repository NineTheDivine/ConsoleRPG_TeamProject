using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG_Team.Entities
{
    internal class Player : Entity
    {
        public int def { get; protected set; }
        public int gold { get; protected set; }

        public Player()
        {
            name = "주인공"; // 추후 입력해서 설정
            level = 1;
            atk = 5;
            health = 100;
            beforeHealth = 0;
            maxHealth = 100;
            isDead = false;
        }
    }
}
