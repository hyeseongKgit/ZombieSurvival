using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField]
    List<GameObject> itemPrefabs;

    List<IItem> currentItems;
    public void Init()
    {
        currentItems = new List<IItem>();
        StartCoroutine(SpawnItem());
    }

    IEnumerator SpawnItem()
    {
        while(true)
        {
            //�ʿ� �������� 3�� �̻� �������� �ʰ� ����
            if (currentItems.Count < 3)
            {
                var item = Instantiate(itemPrefabs[Random.Range(0, itemPrefabs.Count)]).GetComponent<IItem>();
                //���� �������̽��� �븮�� ��¼�� ��������ϰ��־���
                item.Remove += () => { currentItems.Remove(item); Debug.Log($"���� ������ ���� : {currentItems.Count}"); };
                currentItems.Add(item);
                Debug.Log($"���� ������ ���� : {currentItems.Count}");
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}
