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
        // 공격 방식 셀렉터
        SelectorNode attackTypeSelector = new SelectorNode();
        attackTypeSelector.Add(new ActionNode(() => CanUseSkill() ? SkillAttack() : INode.STATE.FAIL)); // 스킬 공격
        attackTypeSelector.Add(new ActionNode(NormalAttack)); // 일반 공격

        // 공격 시퀀스
        SequenceNode attackSequence = new SequenceNode();
        attackSequence.Add(new ActionNode(CheckInAttackRange));
        attackSequence.Add(attackTypeSelector);

        // 타겟 설정 셀렉터
        SelectorNode targetSettingSelector = new SelectorNode();
        targetSettingSelector.Add(new ActionNode(SetCloseRangeTarget)); 
        targetSettingSelector.Add(new ActionNode(SetLongRangeTarget)); 

        // 탐지 시퀀스
        SequenceNode defectiveSequence = new SequenceNode();
        defectiveSequence.Add(new ActionNode(CheckInDetectiveRange));
        defectiveSequence.Add(targetSettingSelector); 
        defectiveSequence.Add(new ActionNode(TraceTarget));

        // 귀환 액션
        var returnAction = new ActionNode(ReturnToOrigin);

        // 대기 액션
        var idleAction = new ActionNode(Idle);

        // 루트 노드
        _rootNode = new SelectorNode();
        _rootNode.Add(attackSequence);
        _rootNode.Add(defectiveSequence);
        _rootNode.Add(returnAction);
        _rootNode.Add(idleAction);
    }
    private INode.STATE NormalAttack()
    {
        Debug.Log("일반 공격 중");
        return INode.STATE.RUN;
    }
    private INode.STATE SkillAttack()
    {
        Debug.Log("스킬 공격 중");
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

        Debug.Log("추적 중");
        MoveTowards(_target.position);
        return INode.STATE.RUN;
    }
    private INode.STATE CheckInDetectiveRange()
    {
        Collider[] detectedObjects = Physics.OverlapSphere(transform.position, defectiveRange, 1 << 8);
        if (detectedObjects.Length > 0)
        {
            _target = detectedObjects[0].transform;
            Debug.Log("탐지 됨");
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
                Debug.Log("근거리 타겟 설정");
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
                Debug.Log("원거리 타겟 설정");
                return INode.STATE.SUCCESS;
            }
        }
        return INode.STATE.FAIL;
    }
    private INode.STATE ReturnToOrigin()
    {
        if (Vector3.Distance(transform.position, _originPosition) >= 0.1f)
        {
            Debug.Log("귀환 중");
            MoveTowards(_originPosition);
            return INode.STATE.RUN;
        }
        return INode.STATE.FAIL;
    }
    private INode.STATE Idle()
    {
        Debug.Log("대기 중");
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
