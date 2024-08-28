using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GimmickOutput : Singleton<GimmickOutput>
{
    public bool isDone;
    public abstract void Act();
}
