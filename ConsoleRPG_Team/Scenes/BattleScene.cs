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
                Console.WriteLine("Lv.{0:D2} Chad ( {1:4} )", GameManager.playerInstance.level, /*Need to be fixed */GameManager.playerInstance.name);
                Console.WriteLine("HP : {0:-3} / {1:-3}", GameManager.playerInstance.health.ToString(), GameManager.playerInstance.maxHealth.ToString());
                Console.WriteLine("MP : {0:-3} / {1:-3}", GameManager.playerInstance.mana.ToString(), GameManager.playerInstance.maxMana.ToString());

                //Player turn
                int? input = null;
                do
                {
                    input = AskInput(1, 3, this.inputstream);
                    if (input == 1)
                    {
                        int? target = null;
                        //when basic attack
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
                        //Retrun to select action
                        if (target == 0)
                            input = null;
                        //Attack enemy
                        else
                        {
                            int beforeEnemeyHealth = this.enemyWave.spawnEnemies[(int)target - 1].health;
                            GameManager.playerInstance.Attack(this.enemyWave.spawnEnemies[(int)target - 1]);
                            Console.WriteLine();
                            Console.WriteLine("Lv.{0:D2} {1:8}", this.enemyWave.spawnEnemies[(int)target - 1].level, this.enemyWave.spawnEnemies[(int)target - 1].name);
                            Console.WriteLine("HP {0:3} -> {1:4}", beforeEnemeyHealth.ToString(), 
                                this.enemyWave.spawnEnemies[(int)target - 1].isDead ? "Dead" : this.enemyWave.spawnEnemies[(int)target - 1].health.ToString());
                            if (this.enemyWave.spawnEnemies[(int)target - 1].isDead)
                            {
                                _enemyCount--;
                                this.enemyWave.spawnEnemies[(int)target - 1].GiveExp();
                                GameManager.playerInstance.getExp += this.enemyWave.spawnEnemies[(int)target - 1].exp;
                                this.enemyWave.spawnEnemies[(int)target - 1].DropItem();
                            }
                            while (AskInput(0, 0, nextstream, "") == null)
                                continue;
                        }
                    }
                    else if(input == 2) //테스트
                    {
                        int? target = null;
                        //when basic attack
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
                        //Retrun to select action
                        if (target == 0)
                            input = null;
                        //Attack enemy
                        else
                        {
                            int beforeEnemeyHealth = this.enemyWave.spawnEnemies[(int)target - 1].health;

                            bool success = GameManager.playerInstance.UseSkill(this.enemyWave.spawnEnemies[(int)target - 1]);

                            if(success)
                            {
                                Console.WriteLine();
                                Console.WriteLine("Lv.{0:D2} {1:8}", this.enemyWave.spawnEnemies[(int)target - 1].level, this.enemyWave.spawnEnemies[(int)target - 1].name);
                                Console.WriteLine("HP {0:3} -> {1:4}", beforeEnemeyHealth.ToString(),
                                    this.enemyWave.spawnEnemies[(int)target - 1].isDead ? "Dead" : this.enemyWave.spawnEnemies[(int)target - 1].health.ToString());
                                if (this.enemyWave.spawnEnemies[(int)target - 1].isDead)
                                {
                                    _enemyCount--;
                                    this.enemyWave.spawnEnemies[(int)target - 1].GiveExp();
                                    GameManager.playerInstance.getExp += this.enemyWave.spawnEnemies[(int)target - 1].exp;
                                    this.enemyWave.spawnEnemies[(int)target - 1].DropItem();
                                }
                                while (AskInput(0, 0, nextstream, "") == null)
                                    continue;
                            }
                            else
                            {
                                input = null;
                            }
                        }
                            
                    } // 테스트
                    else if(input == 3) // 아이템 사용
                    {
                        if(!GameManager.playerInstance.UseItemInBattle())
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
                Console.WriteLine("Lv.{0:D2} Chad ( {1:4} )", GameManager.playerInstance.level, /*Need to be fixed */GameManager.playerInstance.name);
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
    }
}
