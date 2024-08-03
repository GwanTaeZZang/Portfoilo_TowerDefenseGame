using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour, IPoolable
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    private ObjectPool<MonsterController> monsterObjectPool;
    private Transform monster;
    private Vector2 monsterPos;
    private float speed = 1f;

    private int[] routeData;
    private int mapWidth;

    private int routePartsIdx;
    private int goalIdx;
    private Vector2 routePartsStartPos;
    private Vector2 routePartsEndPos;
    private Vector2 dir;

    public void Awake()
    {
        monsterObjectPool = ObjectPoolManager.getInstance.GetPool<MonsterController>();
    }

    public void SetMonster(RouteData _routeData, int _goalIdx ,int _mapWidth)
    {
        routeData = _routeData.tileIdxs;
        mapWidth = _mapWidth;
        goalIdx = _goalIdx;
    }

    private void Update()
    {
        if(dir == Vector2.zero)
        {
            return;
        }
        monsterPos = monster.position;
        monsterPos.x += Time.deltaTime * dir.normalized.x * speed;
        monsterPos.y += Time.deltaTime * dir.normalized.y * speed;
        monster.position = monsterPos;

        float distance = Vector2.Distance(monster.position, routePartsEndPos);
        if (distance < 0.01f)
        {
            routePartsIdx++;
            if(routePartsIdx == routeData.Length)
            {
                OnEnqueue();
            }
            else
            {
                SetDir();
            }
        }
    }

    private void SetDir()
    {

        routePartsStartPos.x = routeData[routePartsIdx] % mapWidth;
        routePartsStartPos.y = routeData[routePartsIdx] / mapWidth;

        monster.position = routePartsStartPos;

        if (routePartsIdx == routeData.Length - 1)
        {
            routePartsEndPos.x = goalIdx % mapWidth;
            routePartsEndPos.y = goalIdx / mapWidth;
        }
        else
        {
            routePartsEndPos.x = routeData[routePartsIdx + 1] % mapWidth;
            routePartsEndPos.y = routeData[routePartsIdx + 1] / mapWidth;
        }


        dir = routePartsEndPos - routePartsStartPos;

    }

    public void OnDequeue()
    {
        routePartsIdx = 0;
        //int x = routeData[0] % mapWidth;
        //int y = routeData[0] / mapWidth;

        //this.transform.position = new Vector2(x, y);
        SetDir();

        this.gameObject.SetActive(true);
    }

    public void OnEnqueue()
    {
        dir = Vector2.zero;
        monsterObjectPool.Enqueue(this);
        this.gameObject.SetActive(false);
    }

    public void SetModel(Transform _model)
    {
        monster = _model;
    }
}
