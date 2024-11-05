using UnityEngine;

public class EnemyBehaviorTree : MonoBehaviour
{
    public int defectiveRange;
    public int attackableRange;

    private SelectorNode _rootNode;
    private Transform _target;
    private Vector3 _originPosition;

    private void Start()
    {
        _originPosition = transform.position;
        SetBehaviorTree();
    }

    private void SetBehaviorTree()
    {
        // ���� ��� ������
        SelectorNode attackTypeSelector = new SelectorNode();
        attackTypeSelector.Add(new ActionNode(() => CanUseSkill() ? SkillAttack() : INode.STATE.FAIL)); // ��ų ����
        attackTypeSelector.Add(new ActionNode(NormalAttack)); // �Ϲ� ����

        // ���� ������
        SequenceNode attackSequence = new SequenceNode();
        attackSequence.Add(new ActionNode(CheckInAttackRange));
        attackSequence.Add(attackTypeSelector);

        // Ÿ�� ���� ������
        SelectorNode targetSettingSelector = new SelectorNode();
        targetSettingSelector.Add(new ActionNode(SetCloseRangeTarget)); 
        targetSettingSelector.Add(new ActionNode(SetLongRangeTarget)); 

        // Ž�� ������
        SequenceNode defectiveSequence = new SequenceNode();
        defectiveSequence.Add(new ActionNode(CheckInDetectiveRange));
        defectiveSequence.Add(targetSettingSelector); 
        defectiveSequence.Add(new ActionNode(TraceTarget));

        // ��ȯ �׼�
        var returnAction = new ActionNode(ReturnToOrigin);

        // ��� �׼�
        var idleAction = new ActionNode(Idle);

        // ��Ʈ ���
        _rootNode = new SelectorNode();
        _rootNode.Add(attackSequence);
        _rootNode.Add(defectiveSequence);
        _rootNode.Add(returnAction);
        _rootNode.Add(idleAction);
    }
    private INode.STATE NormalAttack()
    {
        Debug.Log("�Ϲ� ���� ��");
        return INode.STATE.RUN;
    }
    private INode.STATE SkillAttack()
    {
        Debug.Log("��ų ���� ��");
        return INode.STATE.RUN;
    }
    private bool CanUseSkill()
    {
        return true;
    }
    private INode.STATE CheckInAttackRange()
    {
        if (_target == null) return INode.STATE.FAIL;
        return Vector3.Distance(transform.position, _target.position) < attackableRange ? INode.STATE.SUCCESS : INode.STATE.FAIL;
    }
    private INode.STATE TraceTarget()
    {
        if (_target == null || Vector3.Distance(transform.position, _target.position) < 0.1f)
            return INode.STATE.FAIL;

        Debug.Log("���� ��");
        MoveTowards(_target.position);
        return INode.STATE.RUN;
    }
    private INode.STATE CheckInDetectiveRange()
    {
        Collider[] detectedObjects = Physics.OverlapSphere(transform.position, defectiveRange, 1 << 8);
        if (detectedObjects.Length > 0)
        {
            _target = detectedObjects[0].transform;
            Debug.Log("Ž�� ��");
            return INode.STATE.SUCCESS;
        }
        return INode.STATE.FAIL;
    }
    private INode.STATE SetCloseRangeTarget()
    {
        Collider[] closeTargets = Physics.OverlapSphere(transform.position, defectiveRange, 1 << 8);
        foreach (var target in closeTargets)
        {
            if (Vector3.Distance(transform.position, target.transform.position) < defectiveRange / 2)
            {
                _target = target.transform;
                Debug.Log("�ٰŸ� Ÿ�� ����");
                return INode.STATE.SUCCESS;
            }
        }
        return INode.STATE.FAIL;
    }
    private INode.STATE SetLongRangeTarget()
    {
        Collider[] longTargets = Physics.OverlapSphere(transform.position, defectiveRange, 1 << 8);
        foreach (var target in longTargets)
        {
            if (Vector3.Distance(transform.position, target.transform.position) >= defectiveRange / 2)
            {
                _target = target.transform;
                Debug.Log("���Ÿ� Ÿ�� ����");
                return INode.STATE.SUCCESS;
            }
        }
        return INode.STATE.FAIL;
    }
    private INode.STATE ReturnToOrigin()
    {
        if (Vector3.Distance(transform.position, _originPosition) >= 0.1f)
        {
            Debug.Log("��ȯ ��");
            MoveTowards(_originPosition);
            return INode.STATE.RUN;
        }
        return INode.STATE.FAIL;
    }
    private INode.STATE Idle()
    {
        Debug.Log("��� ��");
        return INode.STATE.RUN;
    }
    private void MoveTowards(Vector3 targetPosition)
    {
        transform.forward = (targetPosition - transform.position).normalized;
        transform.Translate(Vector3.forward * Time.deltaTime, Space.Self);
    }
    private void Update()
    {
        _rootNode.Evaluate();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, defectiveRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackableRange);
    }
}
