
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateObject : GimmickOutput
{
    public Transform parentPrefab;  // Canvas�� ������ ����
    public GameObject prefab;

    public override void Act()
    {
        if (parentPrefab == null)
        {
            Instantiate(prefab, Vector3.zero, Quaternion.identity, parentPrefab);
        }

        // �������� �����ϰ�, Canvas�� �ڽ����� ����
        GameObject instantiatedObject = Instantiate(prefab, Vector3.zero, Quaternion.identity, parentPrefab);

        // RectTransform�� �ִ��� Ȯ���ϰ�, �ʿ��� ������ ����
        RectTransform uiPrefab = instantiatedObject.GetComponent<RectTransform>();
        if (uiPrefab != null)
        {
            // RectTransform�� ���� ���� (��: ��ġ, ũ�� ���� ��)
            uiPrefab.anchoredPosition = Vector2.zero;  // UI ����� ��ġ ����
            uiPrefab.sizeDelta = new Vector2(100, 100); // UI ����� ũ�� ����
        }

        isDone = true;
    }
}