using System;
using System.Collections.Generic;
using UnityEngine;

public class PipeManager : MonoBehaviour
{
    private ObjectPooling objectPool;

    private List<Pipe> activePipes = new List<Pipe>();
    private void Awake()
    {
        objectPool = FindObjectOfType<ObjectPooling>();
    }

    public void CreatePipe()
    {
        GameObject pipeobj = objectPool.GetObject(ObjectType.Pipe);
        if(pipeobj.TryGetComponent<Pipe>(out Pipe pipe))
        {
            pipe.Active();
            if(!activePipes.Contains(pipe))
            {
                activePipes.Add(pipe);
            }
        }
    }
    public void ResetPipes()
    {
        objectPool.ResetObjects(ObjectType.Pipe); 
        activePipes.Clear();
    }

    public (float,float) GetNextPipe(Vector3 birdPos)
    {
        float minDis = 100;
        float centerYtriger = 0;
        foreach (Pipe pipe in activePipes)
        {
            float pipeX = pipe.transform.localPosition.x;
            float distance= pipeX - birdPos.x;
            if (distance>0 && minDis > distance)
            {
                minDis= distance;
                centerYtriger = pipe.GetTriggerCenterY();
            }
        }
        return minDis == 100 ?( 0,0):(minDis,centerYtriger);
    }
}
