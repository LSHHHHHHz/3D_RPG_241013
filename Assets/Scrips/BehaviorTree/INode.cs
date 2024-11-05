using System;
using System.Collections.Generic;

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
// ActionNode: 최하위 노드
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
// SelectorNode: 자식 노드 중 하나가 성공할 때까지 순서확인
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
// SequenceNode: 모든 자식 노드가 성공해야 반환
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
