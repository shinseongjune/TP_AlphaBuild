using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

#region base
public enum NodeState
{
    Running,
    Success,
    Failure
}

public abstract class Node
{
    public MonsterMainController main;

    // ����� ���¸� ��Ÿ���� ����
    protected NodeState _nodeState;

    // �� ����� �ڽ� ����
    protected List<Node> children = new List<Node>();

    // �ڽ� ��带 �߰��ϴ� �Լ�
    public void AddChild(Node child)
    {
        children.Add(child);
    }

    // ����� ���¸� ��ȯ�ϴ� �Լ�
    public NodeState GetNodeState()
    {
        return _nodeState;
    }

    // �� ��忡�� �����ؾ� �� �߻� �޼���
    public abstract NodeState Evaluate();
}

public class Selector : Node
{
    public override NodeState Evaluate()
    {
        foreach (Node node in children)
        {
            switch (node.Evaluate())
            {
                case NodeState.Failure:
                    continue;
                case NodeState.Success:
                    _nodeState = NodeState.Success;
                    return _nodeState;
                case NodeState.Running:
                    _nodeState = NodeState.Running;
                    return _nodeState;
                default:
                    continue;
            }
        }

        _nodeState = NodeState.Failure;
        return _nodeState;
    }
}

public class Sequence : Node
{
    public override NodeState Evaluate()
    {
        bool anyChildRunning = false;

        foreach (Node node in children)
        {
            switch (node.Evaluate())
            {
                case NodeState.Failure:
                    _nodeState = NodeState.Failure;
                    return _nodeState;
                case NodeState.Success:
                    continue;
                case NodeState.Running:
                    anyChildRunning = true;
                    continue;
                default:
                    _nodeState = NodeState.Success;
                    return _nodeState;
            }
        }

        _nodeState = anyChildRunning ? NodeState.Running : NodeState.Success;
        return _nodeState;
    }
}
#endregion

#region enemy
public class DeathSequence : Sequence
{
    public DeathSequence()
    {
        AddChild(new CheckHealth());
        AddChild(new DeathAction());
    }
}

public class CheckHealth : Node
{
    public override NodeState Evaluate()
    {
        if (main.GetComponent<Stats>().hp <= 0)
        {
            _nodeState = NodeState.Success;
            return _nodeState;
        }
        _nodeState = NodeState.Failure;
        return _nodeState;
    }
}

public class DeathAction : Node
{
    public override NodeState Evaluate()
    {
        main.Die();
        _nodeState = NodeState.Success;
        return _nodeState;
    }
}
#endregion
public class CCSequence : Sequence
{
    public CCSequence()
    {
        AddChild(new CheckCC());
        AddChild(new CancelAttackAction());
    }
}

public class CheckCC : Node
{
    public override NodeState Evaluate()
    {
        if (main.cc.isCCed)
        {
            _nodeState = NodeState.Success;
            return _nodeState;
        }
        _nodeState = NodeState.Failure;
        return _nodeState;
    }
}

public class CancelAttackAction : Node
{
    public override NodeState Evaluate()
    {
        main.CancelAttack();
        _nodeState = NodeState.Success;
        return _nodeState;
    }
}

public class AttackingSequence : Sequence
{
    public AttackingSequence()
    {
        AddChild(new AttackingCondition());
    }
}

public class AttackingCondition : Node
{
    public override NodeState Evaluate()
    {
        if (main.anim.GetCurrentState().IsName("Attack"))
        {
            _nodeState = NodeState.Success;
            return _nodeState;
        }
        _nodeState = NodeState.Failure;
        return _nodeState;
    }
}

public class CombatSequence : Sequence
{
    public CombatSequence()
    {
        AddChild(new TargetOnCondition());
        AddChild(new BackstepSequence());
        AddChild(new ChaseSequence());
    }
}

public class TargetOnCondition : Node
{
    public override NodeState Evaluate()
    {
        if (main.target != null)
        {
            _nodeState = NodeState.Success;
            return _nodeState;
        }
        _nodeState = NodeState.Failure;
        return _nodeState;
    }
}

public class BackstepSequence : Sequence
{
    public BackstepSequence()
    {
        AddChild(new TooCloseCondition());
        AddChild(new BackstepAction());
    }
}

public class ChaseSequence : Sequence
{
    public ChaseSequence()
    {
        AddChild(new TooFarCondition());
        AddChild(new ChaseAction());
    }
}

public class RootNode : Sequence
{
    public RootNode()
    {
        AddChild(new DeathSequence());
        AddChild(new CCSequence());
        AddChild(new AttackingSequence());
        AddChild(new CombatSequence());
        AddChild(new DetectionSequence());
    }
}

public class BehaviorTree
{
    private Node _rootNode;

    void Start()
    {
        // Behavior Tree�� ��Ʈ ��� �ʱ�ȭ
        _rootNode = new RootNode();
    }

    void Update()
    {
        // �� �����Ӹ��� Behavior Tree ��
        _rootNode.Evaluate();
    }
}
