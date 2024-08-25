using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class GimmickInput : MonoBehaviour
{
    [System.Serializable]
    public class SimpleEvent : UnityEvent
    {
    }

    public List<SimpleEvent> OutputEvent;

    public void InvokeEvent()
    {
        if (OutputEvent != null && OutputEvent.Count > 0)
        {
            StartCoroutine(InvokeEventsCoroutine());
        }
    }

    private bool isOutputGimmick;
    private bool isDoneAll;

    private IEnumerator InvokeEventsCoroutine()
    {
        OutputEvent[0].Invoke();

        // OutputEvent ����Ʈ�� ��ȸ�ϸ鼭 �ϳ��� ����
        for (int i = 0; i < OutputEvent.Count; i++)
        {
            isOutputGimmick = false;
            isDoneAll = false; // �� �̺�Ʈ���� �ʱ�ȭ

            for (int j = 0; j < OutputEvent[i].GetPersistentEventCount(); j++)
            {
                Object targetObj = OutputEvent[i].GetPersistentTarget(j);
                GimmickOutput associatedOutput = targetObj.GetComponent<GimmickOutput>();
                if (associatedOutput != null)
                {
                    isOutputGimmick = true;
                }
            }

            if (isOutputGimmick)
            {
                // �ش� ������Ʈ�� isDone�� true�� �� ������ ���
                while (!isDoneAll)
                {
                    yield return null;
                    isDoneAll = true; // �⺻������ �Ϸ�� ������ ����
                    for (int j = 0; j < OutputEvent[i].GetPersistentEventCount(); j++)
                    {
                        Object targetObj = OutputEvent[i].GetPersistentTarget(j);
                        GimmickOutput associatedOutput = targetObj.GetComponent<GimmickOutput>();
                        if (associatedOutput != null && !associatedOutput.isDone)
                        {
                            isDoneAll = false; // �Ϸ���� ���� ��� false�� ����
                            break; // �� �̻� Ȯ���� �ʿ� ����
                        }
                    }

                    print("��ٸ��� ��");
                }
            }

            // i + 1�� OutputEvent.Count�� �ʰ����� �ʵ��� Ȯ��
            if (i + 1 < OutputEvent.Count)
            {
                OutputEvent[i + 1].Invoke();
                print(i + "��° �̺�Ʈ ����");
            }
        }
    }

}
