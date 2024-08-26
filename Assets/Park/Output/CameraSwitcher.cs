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
        vCam.Priority = 10;
        freeLookCam.Priority = 9;
    }

    public void buttonFreeLook()
    {
        freeLookCam.Priority = 10;
        vCam.Priority = 9;
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
