using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public Vector3 targetPos; // ��ǥ ��ġ
    public Quaternion targetRotation; // ��ǥ ȸ��
    public AnimationCurve moveCurve; // �̵� �ִϸ��̼� Ŀ��
    public AnimationCurve rotationCurve; // ȸ�� �ִϸ��̼� Ŀ��
    public float moveDuration = 2.0f; // �̵� �� ȸ���� �ɸ��� �ð�

    private Vector3 startPos; // ���� ��ġ
    private Quaternion startRotation; // ���� ȸ��

    public void movement()
    {
        // ���� ������Ʈ�� ��ġ�� ȸ���� ���۰����� ����
        startPos = transform.position;
        startRotation = transform.rotation;

        // �ڷ�ƾ ����
        StartCoroutine(Movement());
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
            float newX = targetPos.x != 0 ? Mathf.Lerp(startPos.x, targetPos.x, moveCurveValue) : startPos.x;
            float newY = targetPos.y != 0 ? Mathf.Lerp(startPos.y, targetPos.y, moveCurveValue) : startPos.y;
            float newZ = targetPos.z != 0 ? Mathf.Lerp(startPos.z, targetPos.z, moveCurveValue) : startPos.z;
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
            targetPos.x != 0 ? targetPos.x : startPos.x,
            targetPos.y != 0 ? targetPos.y : startPos.y,
            targetPos.z != 0 ? targetPos.z : startPos.z
        );
        transform.rotation = targetRotation;
    }
}
