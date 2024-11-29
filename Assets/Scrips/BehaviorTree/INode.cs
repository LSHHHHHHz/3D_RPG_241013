using System;
using System.Collections.Generic;
using UnityEngine;

public interface INode
{
    public enum STATE
    {
        RUN,
        SUCCESS,
        FAIL
    }

    public STATE Evaluate();
}
// ActionNode: ������ ���
public class ActionNode : INode
{
    private readonly Func<INode.STATE> _action;

    public ActionNode(Func<INode.STATE> action)
    {
        _action = action;
    }

    public INode.STATE Evaluate()
    {
        return _action != null ? _action() : INode.STATE.FAIL;
    }
}
// SelectorNode: �ڽ� ��� �� �ϳ��� ������ ������ ����Ȯ��
public class SelectorNode : INode
{
    private readonly List<INode> _children = new List<INode>();

    public void Add(INode node)
    {
        _children.Add(node);
    }

    public INode.STATE Evaluate()
    {
        foreach (var child in _children)
        {
            var childState = child.Evaluate();
            if (childState == INode.STATE.RUN || childState == INode.STATE.SUCCESS)
                return childState;
        }
        return INode.STATE.FAIL;
    }
}
// SequenceNode: ��� �ڽ� ��尡 �����ؾ� ��ȯ
public class SequenceNode : INode
{
    private readonly List<INode> _children = new List<INode>();

    public void Add(INode node)
    {
        _children.Add(node);
    }

    public INode.STATE Evaluate()
    {
        foreach (var child in _children)
        {
            var childState = child.Evaluate();
            if (childState == INode.STATE.FAIL) return INode.STATE.FAIL;
            if (childState == INode.STATE.RUN) return INode.STATE.RUN;
        }
        return INode.STATE.SUCCESS;
    }
}
public class WaitNode : INode
{
    private float waitTime;
    private float startTime;

    public WaitNode(float waitTime)
    {
        this.waitTime = waitTime;
    }
    public INode.STATE Evaluate()
    {
        if (startTime == 0)
        {
            startTime = Time.time;
        }

        if (Time.time - startTime >= waitTime)
        {
            startTime = 0; 
            return INode.STATE.SUCCESS;
        }

        return INode.STATE.RUN;
    }
}

