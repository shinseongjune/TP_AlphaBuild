using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interaction_Gimmick : GimmickTrigger
{

    public LayerMask DetectionLayer; // �ߵ� ���� ���̾�

    public RectTransform InteractionImge; // UI �̹����� RectTransform
    Camera mainCamera; // ���� ī�޶�

    public Transform targetObject; // 3D ��ü�� Transform

    private void Start()
    {
        mainCamera = Camera.main;
        InteractionImge.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & DetectionLayer) != 0)
        {
            InteractionImge.gameObject.SetActive(true);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (((1 << other.gameObject.layer) & DetectionLayer) != 0 )
        {
            // 3D ��ü�� ��ġ�� ȭ�� ��ǥ�� ��ȯ
            Vector3 screenPos = mainCamera.WorldToScreenPoint(targetObject.position);

            // ȭ�� ��ǥ�� UI ��ǥ�� ��ȯ
            InteractionImge.position = screenPos;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (((1 << other.gameObject.layer) & DetectionLayer) != 0)
        {
            InteractionImge.gameObject.SetActive(false);
        }
    }
}
