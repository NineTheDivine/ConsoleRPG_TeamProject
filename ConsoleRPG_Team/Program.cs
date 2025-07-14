namespace ConsoleRPG_TeamKJH
{
    abstract class Entity
    {
        protected string name;
        protected int level;
        protected int atk;
        protected int health;
        protected int maxHealth;

        public abstract void OnTurn();

        public abstract void AtkDiff();

        public abstract void OnDamageDelt();

    }


    class Enemy : Entity
    {
        private int enemyID;
        private bool isDead;


        public override void OnTurn()
        {
            Console.WriteLine("턴");
        }

        public override void AtkDiff()
        {

        }

        public override void OnDamageDelt()
        {

        }

        internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }
}
