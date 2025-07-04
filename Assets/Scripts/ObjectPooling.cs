using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    [SerializeField] private List<ObjectTypeEntry> objectEntries;

    private Dictionary<ObjectType, GameObject> typeToPrefab;
    private Dictionary<ObjectType,Queue<GameObject>> poolDict;

    private Dictionary<ObjectType, List<GameObject>> activeObjects;
    

    private void Awake()
    {
        typeToPrefab = new Dictionary<ObjectType, GameObject>();
        poolDict =new Dictionary<ObjectType,Queue<GameObject>>();
        activeObjects = new Dictionary<ObjectType, List<GameObject>>();

        foreach (var entry in objectEntries)
        {
            if (!typeToPrefab.ContainsKey(entry.type))
            {
                typeToPrefab.Add(entry.type, entry.prefab);
                poolDict.Add(entry.type, new Queue<GameObject>());
            }
        }
        
    }
    public GameObject GetObject(ObjectType objType)
    {
        if (poolDict[objType].TryDequeue(out GameObject obj))
        {
            return obj;
        }
        GameObject newObj = Instantiate(typeToPrefab[objType]);
        if(!activeObjects.ContainsKey(objType))
        {
            activeObjects.Add(objType, new List<GameObject>());
        }

        activeObjects[objType].Add(newObj);

        return newObj;
    }
    public void AddPool(GameObject obj,ObjectType type)
    {
        poolDict[type].Enqueue(obj);
    }
    public void ResetAllObjects()
    {
        foreach (var activeObj in activeObjects)
        {
            ResetObjects(activeObj.Key);
        }
    }
   
    public void ResetObjects(ObjectType objt)
    {
        if (!activeObjects.ContainsKey(objt)) return;
        foreach (GameObject obj in activeObjects[objt])
        {
            if (obj.TryGetComponent<IObject>(out IObject iobj))
            {
                if (iobj.Isactive)
                {
                    iobj.DeActive();
                }
            }
        }
    }
}
public enum ObjectType
{
    Pipe,
    Flyer
}
