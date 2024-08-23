using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : GimmickOutput
{
    public Vector3 targetPos; // 목표 위치
    public Quaternion targetRotation; // 목표 회전
    public AnimationCurve moveCurve; // 이동 애니메이션 커브
    public AnimationCurve rotationCurve; // 회전 애니메이션 커브
    public float moveDuration = 2.0f; // 이동 및 회전에 걸리는 시간

    private Vector3 startPos; // 시작 위치
    private Quaternion startRotation; // 시작 회전

    public bool isDone;

    private void Start()
    {
        isDone = false;
    }

    public override void Act()
    {
        if (!isDone)
        {
            // 현재 오브젝트의 위치와 회전을 시작값으로 저장
            startPos = transform.position;
            startRotation = transform.rotation;

            targetPos = transform.position + targetPos;
            // 코루틴 시작

            StartCoroutine(Movement());
            isDone = true;
        }
    }

    public IEnumerator Movement()
    {
        float elapsedTime = 0f; // 경과 시간 추적

        // 이동 및 회전이 완료될 때까지 반복
        while (elapsedTime < moveDuration)
        {
            // 경과 시간 비율 (0에서 1 사이의 값)
            float t = elapsedTime / moveDuration;

            // 이동 애니메이션 커브를 이용하여 위치 보간
            float moveCurveValue = moveCurve.Evaluate(t);
            float newX =  Mathf.Lerp(startPos.x, targetPos.x, moveCurveValue);
            float newY =  Mathf.Lerp(startPos.y,  targetPos.y, moveCurveValue);
            float newZ =  Mathf.Lerp(startPos.z,  targetPos.z, moveCurveValue);
            transform.position = new Vector3(newX, newY, newZ);

            // 회전 애니메이션 커브를 이용하여 회전 보간
            float rotationCurveValue = rotationCurve.Evaluate(t);
            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, rotationCurveValue);

            // 경과 시간 증가
            elapsedTime += Time.deltaTime;

            // 한 프레임 기다림
            yield return null;
        }

        // 이동 및 회전 완료 후 정확한 목표 위치 및 회전으로 설정
        transform.position = new Vector3(
           targetPos.x ,
           targetPos.y ,
           targetPos.z 
        );
        transform.rotation = targetRotation;
    }
}
