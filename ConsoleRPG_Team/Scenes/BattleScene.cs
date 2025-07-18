using ConsoleRPG_Team.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG_Team.Scenes
{

    internal class BattleScene : Scene
    {
        protected EnemyWave enemyWave;
        private int _enemyCount = 0;
        private string[] askstream = new string[1] { "0. 취소" };
        private string[] nextstream = new string[1] { "0. 다음" };
        
        public BattleScene() 
        {
            throw new NotImplementedException();
        }
        public BattleScene(EnemyWave ew)
        {
            this.enemyWave = ew;
            this._enemyCount = enemyWave.spawnEnemies.Count;
            GameManager.waveEnemyCount = this._enemyCount;
            this.inputstream = new string[3] { "1. 공격" , "2. 스킬사용" , "3. 아이템 사용"};
        }

        public override SceneType OnSceneEnter()
        {
            //Remember Health before Battle
            GameManager.playerInstance.beforeHealth = GameManager.playerInstance.health;
            
            if (this._enemyCount == 0)
            {
                Console.WriteLine("0 Length Enemy Wave at BattleScene");
                return SceneType.Quit;
            }
            do
            {
                //Header
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Battle!!");
                Console.ResetColor();
                Console.WriteLine();

                //Enemy Info
                foreach (Enemy enemy in enemyWave.spawnEnemies)
                {
                    if (enemy.isDead)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine("Lv.{0:D2} {1:8} Dead", enemy.level, enemy.name);
                        Console.ResetColor();                  
                    }  
                    else
                        Console.WriteLine("Lv.{0:D2} {1:8} HP {2,-3}", enemy.level, enemy.name, enemy.health.ToString());
                }

                //Player Info
                Console.WriteLine("[ 내 정보 ]");
                Console.WriteLine("Lv.{0:D2} {1:5} ( {2:8} )", GameManager.playerInstance.level, GameManager.playerInstance.name, GameManager.playerInstance.playerClass.ToString());
                Console.WriteLine("HP : {0:-3} / {1:-3}", GameManager.playerInstance.health.ToString(), GameManager.playerInstance.maxHealth.ToString());
                Console.WriteLine("MP : {0:-3} / {1:-3}", GameManager.playerInstance.mana.ToString(), GameManager.playerInstance.maxMana.ToString());

                //Player turn
                int? input = null;
                do
                {
                    input = AskInput(1, 3, this.inputstream);
                    //Basic Attack
                    if (input == 1)
                    {
                        int? target = _PrintEnemies();
                        //Cancel
                        if (target == 0)
                            input = null;
                        else
                        {
                            int beforeEnemeyHealth = this.enemyWave.spawnEnemies[(int)target - 1].health;
                            GameManager.playerInstance.Attack(enemyWave.spawnEnemies[(int)target - 1]);

                            Console.WriteLine();
                            Console.WriteLine("Lv.{0:D2} {1:8}", this.enemyWave.spawnEnemies[(int)target - 1].level, this.enemyWave.spawnEnemies[(int)target - 1].name);
                            Console.WriteLine("HP {0:3} -> {1:4}", beforeEnemeyHealth.ToString(),
                                this.enemyWave.spawnEnemies[(int)target - 1].isDead ? "Dead" : this.enemyWave.spawnEnemies[(int)target - 1].health.ToString());
                            if (this.enemyWave.spawnEnemies[(int)target - 1].isDead)
                                _enemyCount--;
                            while (AskInput(0, 0, nextstream, "") == null)
                                continue;
                        }
                    }


                    //OnSkill
                    else if (input == 2) //테스트
                    {
                        int? target = null;
                        List<Enemy> targets = new List<Enemy>();

                        //Mage attacks all with skill
                        if (GameManager.playerInstance.playerClass == PlayerClass.Mage)
                        {
                            int? confirm = AskInput(0, 1, new string[] { "1. 파이어볼 시전", "0. 취소" });
                            if (confirm == 1)
                                foreach (Enemy enemy in this.enemyWave.spawnEnemies)
                                {
                                    if (enemy.isDead) continue;
                                    targets.Add(enemy);
                                }
                            else
                                target = 0;
                        }
                        //if not, select one
                        else
                        {
                            target = _PrintEnemies();
                            if (target != 0)
                                targets.Add(this.enemyWave.spawnEnemies[(int)target - 1]);
                        }

                        //Cancel
                        if (target == 0)
                            input = null;

                        //Attack enemy With Skill
                        else
                        {
                            int[] beforeEnemeyHealth = new int[targets.Count];
                            for (int i = 0; i < beforeEnemeyHealth.Length; i++)
                                beforeEnemeyHealth[i] = targets[i].health;
                            bool success = GameManager.playerInstance.UseSkill(targets);
                            if (!success)
                                input = null;
                            else
                            {
                                Console.WriteLine();
                                for (int i = 0; i < beforeEnemeyHealth.Length; i++)
                                {
                                    Console.WriteLine("Lv.{0:D2} {1:8}", targets[i].level, targets[i].name);
                                    Console.WriteLine("HP {0:3} -> {1:4}", beforeEnemeyHealth[i].ToString(),
                                        targets[i].isDead ? "Dead" : targets[i].health.ToString());
                                    if (targets[i].isDead)
                                        _enemyCount--;
                                }
                                while (AskInput(0, 0, nextstream, "") == null)
                                    continue;
                            }
                        }
                    }

                    //Use Item
                    else if (input == 3)
                    {
                        if (!GameManager.playerInstance.UseItemInBattle())
                            input = null;
                    }
                } while (input == null);
                //Player turn end

                //Check Victory state
                if (_enemyCount <= 0)
                    return SceneType.BattleEndScene;
                //Check Victory state end

                //Enemy Phase
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine();
                Console.WriteLine("Enemy Turn!");
                Console.WriteLine();
                Console.ResetColor();
                int beforePlayerHP = GameManager.playerInstance.health;
                foreach (Enemy enemy in enemyWave.spawnEnemies)
                {
                    if (enemy.isDead)
                        continue;
                    enemy.Attack(GameManager.playerInstance);
                    Console.WriteLine();
                }
                Console.WriteLine("Lv.{0:D2} {1:5} ( {2:8} )", GameManager.playerInstance.level, GameManager.playerInstance.name, GameManager.playerInstance.playerClass.ToString());
                Console.WriteLine("HP : {0:-3} -> {1:-4}", beforePlayerHP.ToString(),
                GameManager.playerInstance.isDead ? "Dead" : GameManager.playerInstance.health.ToString());
                

                while (AskInput(0, 0, nextstream) == null)
                    continue;
                //Enemy Phase End

                //Check Player is Dead
                if (GameManager.playerInstance.isDead)
                    return SceneType.GameOverScene;

            } while (true);
        }

        private int? _PrintEnemies()
        {
            int? target = null;
            do
            {
                Console.WriteLine();
                int enemyindex = 1;
                foreach (Enemy enemy in enemyWave.spawnEnemies)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("{0:2}. ", enemyindex.ToString());
                    Console.ResetColor();
                    if (enemy.isDead)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine("Lv.{0:D2} {1:8} Dead", enemy.level, enemy.name);
                        Console.ResetColor();
                    }
                    else
                        Console.WriteLine("Lv.{0:D2} {1:8} HP {2,-3}", enemy.level, enemy.name, enemy.health.ToString());
                    enemyindex++;
                }

                target = AskInput(0, (int)GameManager.waveEnemyCount, askstream, "대상을 선택해주세요.");
                if (target != null && target != 0 && enemyWave.spawnEnemies[(int)target - 1].isDead)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine();
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.ResetColor();
                    target = null;
                }
            } while (target == null);
            return target;
        }
    }
}
