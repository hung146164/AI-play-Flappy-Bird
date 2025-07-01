using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    private ObjectPooling objectPool;
    private void Awake()
    {
        objectPool= FindObjectOfType<ObjectPooling>();
    }
    public void CreatePipe(Vector2 spawnPos)
    {
        GameObject ob= objectPool.GetObject(ObjectType.Pipe);
        if(ob.TryGetComponent<IObject>(out IObject iobj))
        {
            iobj.Active();
        }
        ob.transform.position = spawnPos;
    }

}
