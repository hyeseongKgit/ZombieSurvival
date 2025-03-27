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

    //������ �����ϸ� ���� ��������.
    //1���̺� 1���� 2���̺� 2���� 3���̺� 3����.. �̷������� ���ڱ�
    //���� 0������ �Ǹ� �������̺갡 ���۵�
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
