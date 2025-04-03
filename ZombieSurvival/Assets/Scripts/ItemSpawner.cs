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
            //맵에 아이템이 3개 이상 생성되지 않게 제어
            if (currentItems.Count < 3)
            {
                var item = Instantiate(itemPrefabs[Random.Range(0, itemPrefabs.Count)]).GetComponent<IItem>();
                //여기 인터페이스에 대리자 어쩌구 구조고민하고있었음
                item.Remove += () => { currentItems.Remove(item); Debug.Log($"지금 아이템 개수 : {currentItems.Count}"); };
                currentItems.Add(item);
                Debug.Log($"지금 아이템 개수 : {currentItems.Count}");
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}
