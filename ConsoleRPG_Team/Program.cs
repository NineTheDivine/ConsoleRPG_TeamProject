using ConsoleRPG_Team.Entities;
using System;

namespace ConsoleRPG_Team
{
   
    internal class Program
    {
        static void Main(string[] args)
        {
            EnemyWave wave = new EnemyWave(1, 3, 1, 5);
            wave.SpawnEnemyWave();

            Console.WriteLine("=== 웨이브 적 목록 ===");
            foreach (var enemy in wave.spawnEnemies)
            {
                Console.WriteLine($"적 이름: {enemy.name}, 레벨: {enemy.level}, 체력: {enemy.Health}");
            }
        }
    }
}
