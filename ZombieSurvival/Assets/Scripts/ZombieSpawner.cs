using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public Zombie zombiePrefab;
    public ZombieData[] zombieDatas;
    public Transform[] spawnPoints;
    public List<Zombie> zombies;
    int wave = 1;

    //게임이 시작하면 좀비를 만들어야함.
    //1웨이브 1마리 2웨이브 2마리 3웨이브 3마리.. 이런식으로 하자구
    //좀비가 0마리가 되면 다음웨이브가 시작됨
    public void Init()
    {
        StartCoroutine(WaveStart(wave));
    }

    IEnumerator WaveStart(int wave)
    {
        GameManager.instance.UpdateWaveUI($"WAVE : {wave}");
        for (int i = 0; i < wave; i++)
        {
            var zombie = Instantiate(zombiePrefab, spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity, null);
            zombie.zombieData = zombieDatas[Random.Range(0, zombieDatas.Length)];
            zombie.Init();
            zombies.Add(zombie);
            GameManager.instance.UpdateEnemyLeftText($"Enemy Left : {zombies.Count}");
            zombie.OnDeath += () =>
            {
                zombies.Remove(zombie);
                GameManager.instance.UpdateEnemyLeftText($"Enemy Left : {zombies.Count}");
                GameManager.instance.score += 100;
                GameManager.instance.UpdateScore();
                if (zombies.Count == 0 )
                {
                    StartCoroutine(WaveStart(++wave));
                }
            };
            yield return new WaitForSeconds(1f);
        }
    }
}
