using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController
{
    private MapData mapData;

    private ObjectPool<MonsterController> monsterObjectPool;

    public WaveController(MapData _mapData)
    {
        mapData = _mapData;

        GameObject monsterParent = new GameObject();
        monsterParent.name = "MonsterParent";

        monsterObjectPool = ObjectPoolManager.getInstance.GetPool<MonsterController>();
        monsterObjectPool.SetModel(Resources.Load<Transform>("Monster/MonsterPrefab"), monsterParent.transform);
    }

    public void SpawnMonster()
    {
        //MonsterController monster = Instantiate<MonsterController>(Resources.Load<MonsterController>("Monster/MonsterPrefab"), monsterParent.transform);

        int idx = Random.Range(0, 2);
        Debug.Log($"monster route is {idx}");
        MonsterController monster = monsterObjectPool.Dequeue();
        monster.SetMonster(mapData.routes[idx], mapData.goalIdx, mapData.wid);
        monster.OnDequeue();
    }
}
