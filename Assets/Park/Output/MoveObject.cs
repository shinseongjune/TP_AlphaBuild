using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : GimmickOutput
{
    public Vector3 targetPos; // ��ǥ ��ġ
    public Quaternion targetRotation; // ��ǥ ȸ��
    public AnimationCurve moveCurve; // �̵� �ִϸ��̼� Ŀ��
    public AnimationCurve rotationCurve; // ȸ�� �ִϸ��̼� Ŀ��
    public float moveDuration = 2.0f; // �̵� �� ȸ���� �ɸ��� �ð�

    private Vector3 startPos; // ���� ��ġ
    private Quaternion startRotation; // ���� ȸ��

    public bool isDone;

    private void Start()
    {
        isDone = false;
    }

    public override void Act()
    {
        if (!isDone)
        {
            // ���� ������Ʈ�� ��ġ�� ȸ���� ���۰����� ����
            startPos = transform.position;
            startRotation = transform.rotation;

            targetPos = transform.position + targetPos;
            // �ڷ�ƾ ����

            StartCoroutine(Movement());
            isDone = true;
        }
    }

    public IEnumerator Movement()
    {
        float elapsedTime = 0f; // ��� �ð� ����

        // �̵� �� ȸ���� �Ϸ�� ������ �ݺ�
        while (elapsedTime < moveDuration)
        {
            // ��� �ð� ���� (0���� 1 ������ ��)
            float t = elapsedTime / moveDuration;

            // �̵� �ִϸ��̼� Ŀ�긦 �̿��Ͽ� ��ġ ����
            float moveCurveValue = moveCurve.Evaluate(t);
            float newX =  Mathf.Lerp(startPos.x, targetPos.x, moveCurveValue);
            float newY =  Mathf.Lerp(startPos.y,  targetPos.y, moveCurveValue);
            float newZ =  Mathf.Lerp(startPos.z,  targetPos.z, moveCurveValue);
            transform.position = new Vector3(newX, newY, newZ);

            // ȸ�� �ִϸ��̼� Ŀ�긦 �̿��Ͽ� ȸ�� ����
            float rotationCurveValue = rotationCurve.Evaluate(t);
            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, rotationCurveValue);

            // ��� �ð� ����
            elapsedTime += Time.deltaTime;

            // �� ������ ��ٸ�
            yield return null;
        }

        // �̵� �� ȸ�� �Ϸ� �� ��Ȯ�� ��ǥ ��ġ �� ȸ������ ����
        transform.position = new Vector3(
           targetPos.x ,
           targetPos.y ,
           targetPos.z 
        );
        transform.rotation = targetRotation;
    }
}
