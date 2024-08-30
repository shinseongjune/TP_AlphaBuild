using Cinemachine;
using UnityEngine;

public class CameraSwitcher : GimmickOutput
{
    public CinemachineVirtualCamera vCam;
    public CinemachineFreeLook freeLookCam;

    [Header("지정된 카메라 위치로 이동 = 체크")]
    public bool ismovevirtualcam;

    
    public void buttonVirtual()
    {
        vCam.Priority = 10;
        freeLookCam.Priority = 9;
        isDone = true;
    }

    public void buttonFreeLook()
    {
        freeLookCam.Priority = 10;
        vCam.Priority = 9;
        isDone = true;
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
