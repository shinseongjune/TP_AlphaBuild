using Cinemachine;
using UnityEngine;

public class CameraSwitcher : GimmickOutput
{
    public CinemachineVirtualCamera vCam;
    public CinemachineFreeLook freeLookCam;

    [Header("������ ī�޶� ��ġ�� �̵� = üũ")]
    public bool ismovevirtualcam;

    
    public void buttonVirtual()
    {
        vCam.MoveToTopOfPrioritySubqueue();
    }

    public void buttonFreeLook()
    {
        freeLookCam.MoveToTopOfPrioritySubqueue();
    }

    public override void Act()
    {
        if (ismovevirtualcam)
        {
            buttonVirtual();
        }
        else
        {
            buttonFreeLook();
        }
        isDone = true;
    }
}
