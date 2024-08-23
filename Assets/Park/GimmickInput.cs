
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GimmickInput : MonoBehaviour
{
    public List<GimmickTrigger> Triggers;
    
    

    [Serializable]
    public class SimpleEvent : UnityEvent { }

    // �ν����Ϳ��� ȣ���� �� �ִ� �̺�Ʈ
    public SimpleEvent OutputEvent;


    private void Update()
    {
        if (Triggers.Count < 2)
        {
            //Ʈ���Ű� 1���϶� 
            for (int i = 0; i < Triggers.Count; i++)
            {
                if (Triggers[i].isTriggered)
                {
                    InvokeEvent();
                    Triggers[i].isTriggered = false;
                }
            }
        }
        else
        { 
            //Ʈ���Ű� 2�� �̻��϶�
        }
    }

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