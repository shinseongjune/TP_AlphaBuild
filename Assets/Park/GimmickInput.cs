
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GimmickInput : MonoBehaviour
{
    public List<GimmickTrigger> Trigger;
    [Serializable]
    public class SimpleEvent : UnityEvent { }

    // �ν����Ϳ��� ȣ���� �� �ִ� �̺�Ʈ
    public SimpleEvent OutputEvent;

    public void InvokeEvent()
    {
        // �̺�Ʈ�� ������ ��쿡�� ȣ��
        if (OutputEvent != null)
        {
            // �̺�Ʈ�� ȣ��
            OutputEvent.Invoke();
        }
    }
}