using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GimmickOutput : GameManager
{
    public bool isDone;
    public abstract void Act();
}
