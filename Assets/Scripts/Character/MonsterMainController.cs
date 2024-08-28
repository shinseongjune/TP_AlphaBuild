using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MonsterMainController : MainController
{
    public Transform targetPosition;

    CharacterController controller;

    BehaviorTree ai;

    public bool isAttacker;

    public PlayerMainController target;

    public Vector2 DISTANCE_LIMIT;

    public float DETECTION_RANGE;

    List<IUpdater> updaters = new List<IUpdater>();

    [HideInInspector] public bool isGameStopped;

    private void Awake()
    {
        controller = GetComponentInParent<CharacterController>();

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

        logic.SetLogicalForward(transform.rotation);

        foreach (IUpdater updater in updaters)
        {
            updater.Update();
        }
    }

    public void CancelAttack()
    {
        anim.ResetTrigger("Attack");
    }

    public void Chase()
    {
        if (target == null) return;

        Vector3 offset = (transform.position - target.transform.position).normalized * (DISTANCE_LIMIT.x + DISTANCE_LIMIT.y)/2;
        targetPosition.position = target.transform.position + offset;
        anim.SetFloat("MoveZ", 1);
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
        transform.root.gameObject.SetActive(false);
    }
}
