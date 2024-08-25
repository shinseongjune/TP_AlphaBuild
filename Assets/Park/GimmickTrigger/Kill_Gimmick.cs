using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Kill_Gimmick : GimmickTrigger
{
    public List<GameObject> killDetectionObjects;

    private void Update()
    {
        bool allInactive = true;

        // ����Ʈ�� ��� ��ü�� ��ȸ�ϸ鼭 Ȱ�� ���¸� Ȯ��
        for (int i = 0; i < killDetectionObjects.Count; i++)
        {
            if (killDetectionObjects[i].activeSelf)
            {
                allInactive = false; // �ϳ��� Ȱ��ȭ�� ��ü�� ������ false�� ����
                break; // �� �̻� ��ȸ�� �ʿ䰡 �����Ƿ� ���� ����
            }
        }

        // ��� ��ü�� ��Ȱ��ȭ�� ��쿡�� �Լ� ȣ��
        if (allInactive)
        {
            GetComponentInParent<GimmickInput>().InvokeEvent();
        }
    }
}
