using System;

namespace ConsoleRPG_Team
{
    abstract class Entity
    {
        public string name;
        protected int level;
        private int atk = 10;
        private int health;
        protected int maxHealth;
        protected bool isDead = false;

        //public abstract void OnTurn();

        public abstract void Attack(Player player);
        public abstract void Attack(Enemy enemy);

        public abstract int AtkDiff();

        //public abstract void OnDamageDelt();

        public int Atk
        {
            get { return atk; }
            set { atk = value; }
        }

        public int Health
        {
            get { return health; }
            set
            {
                health = value;
                if (value <= 0)
                {
                    isDead = true;
                }
            }
        }
    }

    class Enemy : Entity
    {
        private int enemyID;
        private string Reward;

        Random random = new Random();

        public Enemy()
        {
            name = "미니언";
        }
        //public override void OnTurn()
        //{
        //    Attack();
        //}

        public override void Attack(Player player)
        {
            int randomAtk = AtkDiff();
            player.Health -= randomAtk;
            Console.WriteLine($"{player.name}의 체력을 {randomAtk} 깎았습니다.");
        }

        public override void Attack(Enemy enemy)
        {   
        }

        public override int AtkDiff() // 데미지 +- 10% 계산
        {
            int randomAtk = random.Next(Atk - (Atk / 10), Atk + (Atk / 10) + 1);

            return randomAtk;
        }

        //public override void OnDamageDelt()
        //{


        //    Console.WriteLine($"체력이 {AtkDiff()} 만큼 깎였습니다.");


        //}
    }

    class Player : Entity
    {
        private int def;
        private int gold;

        Random random = new Random();

        public Player()
        {
            name = "주인공";
        }
        //public override void OnTurn()
        //{
        //    Attack();
        //}

        public override void Attack(Enemy enemy)
        {
            int randomAtk = AtkDiff();
            enemy.Health -= randomAtk;
            Console.WriteLine($"{enemy.name}의 체력을 {randomAtk} 깎았습니다.");
        }

        public override void Attack(Player player)
        {
        }

        public override int AtkDiff() // 데미지 +- 10% 계산
        {
            int randomAtk = random.Next(Atk - (Atk / 10), Atk + (Atk / 10) + 1);

            return randomAtk;
        }

        //public override void OnDamageDelt()
        //{

        //}
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Enemy enemy = new Enemy();
            Player player = new Player();

            enemy.Attack(player);
            enemy.Attack(player);
            enemy.Attack(player);

            player.Attack(enemy);
            player.Attack(enemy);
            player.Attack(enemy);





        }
    }
}

