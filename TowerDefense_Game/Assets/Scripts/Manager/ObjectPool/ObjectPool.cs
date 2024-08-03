using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> : IObjectPool where T : IPoolable, new()
{
    private Queue<T> pool;
    private Transform model = null;
    private Transform parent = null;
    int increaseSize; // defult = 4

    public ObjectPool(int _capacity, int _increaseSize)
    {
        pool = new Queue<T>(_capacity);
        increaseSize = _increaseSize;
    }
    public void SetModel(Transform _model, Transform _parent)
    {
        model = _model;
        parent = _parent;
    }
    public T Dequeue()
    {
        if(pool.Count < 1)
        {
            for(int i =0; i< increaseSize; i++)
            {
                //T obj = new T();
                //if (model != null)
                //{
                //    obj.SetModel(model);
                //}
                //Enqueue(obj);

                T poolObj;

                Transform createdModel = GameObject.Instantiate(model, parent);

                if (typeof(T).IsSubclassOf(typeof(MonoBehaviour)))
                {
                    poolObj = createdModel.GetComponent<T>();
                    poolObj.SetModel(createdModel);
                }
                else
                {
                    poolObj = new T();
                    poolObj.SetModel(createdModel);
                }

                Enqueue(poolObj);
            }
        }
        return pool.Dequeue();
    }
    public void Enqueue(T t)
    {
        pool.Enqueue(t);
    }
}
