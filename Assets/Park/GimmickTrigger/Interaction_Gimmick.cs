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
    private bool isPlayerInTrigger;

    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        InteractionImge.gameObject.SetActive(false);
    }

    private void Start()
    {
        mainCamera = Camera.main;
        InteractionImge.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.G))
        {
            GetComponentInParent<GimmickInput>().InvokeEvent();
        }
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
            isPlayerInTrigger = true;
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (((1 << other.gameObject.layer) & DetectionLayer) != 0)
        {
            InteractionImge.gameObject.SetActive(false);
            isPlayerInTrigger = false;
        }
    }
}
