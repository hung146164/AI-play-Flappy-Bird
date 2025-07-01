using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Obstacle:MonoBehaviour, IObject
{
    public bool Isactive { get; set; } =false;
    private ObjectPooling objectPool;
    protected virtual void Awake()
    {
        objectPool = FindAnyObjectByType<ObjectPooling>();
    }
    public float speed = 5f;
    public void ReturnToPool()
    {
        gameObject.SetActive(false);
        objectPool.AddPool(gameObject, ObjectType.Pipe);
    }

    public void Active()
    {
        gameObject.SetActive(true);
        Isactive = true;
    }
    public void DeActive()
    {
        gameObject.SetActive(false);
        Isactive = false;
    }

}
