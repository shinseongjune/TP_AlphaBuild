using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMainController : MonoBehaviour
{
    public AnimationController anim;
    public LogicController logic;
    public CrowdControlController cc;

    public bool isAttacker;

    public PlayerMainController target;

    public void CancelAttack()
    {

    }

    public void Die()
    {
#if UNITY_EDITOR
        print($"Monster {gameObject.name} is dead.");
#endif

    }
}
