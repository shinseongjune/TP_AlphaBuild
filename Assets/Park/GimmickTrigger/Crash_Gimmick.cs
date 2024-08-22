using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crash_Gimmick : GimmickTrigger
{
    public enum eDetectiontype
    {
        enter, // �ݶ��̴� �ڽ��� �浹 ������
        delay, // float��ŭ ��ٸ��� Ʈ����
        exit // �ݶ��̴� �ڽ��� �浹�� ��������
    }

    public eDetectiontype detectionType; // Ʈ���� ����

    public LayerMask DetectionLayer; // �ߵ� ���� ���̾�

    public float delayTime;

    private float timer; 


    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & DetectionLayer) != 0)
        {
            if (detectionType == eDetectiontype.enter)
            {
                Debug.Log("enter�浹 ������");
                isTriggered = true;
            }
            timer = 0;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (((1 << other.gameObject.layer) & DetectionLayer) != 0 && detectionType == eDetectiontype.delay)
        {
            if (timer >= delayTime)
            {
                Debug.Log(delayTime + " �� ��ŭ ��ٸ� �� ������");
                isTriggered = true;
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (((1 << other.gameObject.layer) & DetectionLayer) != 0)
        {
            if (detectionType == eDetectiontype.exit)
            { 
                Debug.Log("exit�浹 ������");
                isTriggered = true;
            }
            timer = 0;
        }
    }
}
