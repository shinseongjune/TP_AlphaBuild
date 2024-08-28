using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MonsterMainController : MainController
{
    public Transform targetPosition;

    public CrowdControlController cc;
    BehaviorTree ai;

    public bool isAttacker;

    public PlayerMainController target;

    public Vector2 DISTANCE_LIMIT;

    public float DETECTION_RANGE;

    List<IUpdater> updaters = new List<IUpdater>();

    [HideInInspector] public bool isGameStopped;

    private void Awake()
    {
        CharacterController controller = GetComponentInParent<CharacterController>();

        anim = new AnimationController(GetComponentInParent<Animator>());
        cc = new CrowdControlController(this);
        logic = new LogicController(transform.forward, this, controller);

        ai = new BehaviorTree(this);

        updaters.Add(ai);
        updaters.Add(cc);
    }

    private void Update()
    {
        if (isGameStopped) return;

        foreach (IUpdater updater in updaters)
        {
            updater.Update();
        }

        logic.SetLogicalForward(transform.rotation);
    }

    public void CancelAttack()
    {
        anim.ResetTrigger("Attack");
    }

    public void Chase()
    {
        if (target == null) return;

        targetPosition.position = target.transform.position;
    }

    public void Backstep()
    {
        anim.SetTrigger("Backstep");
    }

    public void SetTarget(PlayerMainController target)
    {
        this.target = target;
    }

    public void DoIdle()
    {
        anim.ResetTrigger("Attack");
        anim.ResetTrigger("Backstep");
        anim.SetFloat("MoveZ", 0);
        targetPosition.position = transform.position;
    }

    public void Die()
    {
#if UNITY_EDITOR
        print($"Monster {gameObject.name} is dead.");
#endif
        transform.root.gameObject.SetActive(false);
    }
}
