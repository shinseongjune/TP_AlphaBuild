using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GimmickManager : MonoBehaviour
{
    public enum ewfuia
    {
        input,
        trigger,
        output
    }
    public ewfuia gimmickType; // 이 필드는 public이므로 인스펙터에 나타나야 함
}
