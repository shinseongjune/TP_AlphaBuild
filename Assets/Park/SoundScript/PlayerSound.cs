using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : SonudManager
{
    [Header("°È´Â ¼Ò¸®")]
    public List<AudioClip> footSound;

    public void FootStepSound()
    {
        PlaySound(footSound[Random.Range(0, footSound.Count)]);
    }

}
