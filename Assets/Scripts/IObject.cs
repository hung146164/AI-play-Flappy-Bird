using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObject
{
    public bool Isactive { get; set; }
    public void ReturnToPool();
    public void Active();
    public void DeActive();
}
