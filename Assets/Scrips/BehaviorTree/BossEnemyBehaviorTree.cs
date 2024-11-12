using UnityEngine;
using System.Collections;

public class EnemyBehaviorTree : MonoBehaviour
{
    private SelectorNode _rootNode;
    private BaseEnemy baseEnemy;
    private EnemyDetector detector;
    private EnemyMove enemyMove;
    private BossSkill bossSkill;
    private Animator anim;
    private float lowHp;
    private bool hasHealed;
    private bool hasEscaped;
    private bool isAttacked;
    
    private float skillCooldown = 10f;
    private float lastSkillTime = -1f;

    Vector3 originPos;
    private void Awake()
    {
        baseEnemy = GetComponent<BaseEnemy>();
        detector = GetComponent<EnemyDetector>();
        enemyMove = GetComponent<EnemyMove>();
        bossSkill = GetComponent<BossSkill>();
        anim = GetComponent<Animator>();
        originPos = transform.position;
    }
    private void Start()
    {
        lowHp = baseEnemy.GetEnemyMaxHP() * 0.3f;
        SetBehaviorTree();
    }
    private void SetBehaviorTree()
    {
        // ���� ���� ������
        SelectorNode crisisManagementSelector = new SelectorNode();
        crisisManagementSelector.Add(new ActionNode(HealIfLowHP));
        crisisManagementSelector.Add(new ActionNode(Escape));

        // ���� ������
        SequenceNode stateSequence = new SequenceNode();
        stateSequence.Add(new ActionNode(CheckHP));
        stateSequence.Add(crisisManagementSelector);

        // ���� ��� ������
        SelectorNode attackTypeSelector = new SelectorNode();
        attackTypeSelector.Add(new ActionNode(() => CanUseSkill() ? SkillAttack() : INode.STATE.FAIL));
        attackTypeSelector.Add(new ActionNode(NormalAttack));

        // ���� ������
        SequenceNode attackSequence = new SequenceNode();
        attackSequence.Add(new ActionNode(CheckInAttackRange));
        attackSequence.Add(attackTypeSelector);

        // Ž�� ������
        SequenceNode defectiveSequence = new SequenceNode();
        defectiveSequence.Add(new ActionNode(CheckInDetectiveRange));
        defectiveSequence.Add(new ActionNode(TraceTarget));

        // ��ȯ �׼�
        var returnAction = new ActionNode(ReturnToOrigin);

        // ��� �׼�
        var idleAction = new ActionNode(Idle);

        // ��Ʈ ���
        _rootNode = new SelectorNode();
        _rootNode.Add(stateSequence);
        _rootNode.Add(attackSequence);
        _rootNode.Add(defectiveSequence);
        _rootNode.Add(returnAction);
        _rootNode.Add(idleAction);
    }
    private INode.STATE CheckHP()
    {
        return baseEnemy.GetEnemyCurrentHp() < lowHp ? INode.STATE.SUCCESS : INode.STATE.FAIL;
    }
    private INode.STATE HealIfLowHP()
    {
        if (baseEnemy.GetEnemyCurrentHp() < lowHp && !hasHealed)
        {
          //  Debug.Log("HP ȸ�� ��...");
            anim.SetTrigger("DoHeal");
            hasHealed = true;
            return INode.STATE.RUN;
        }
        return INode.STATE.FAIL;
    }
    private INode.STATE Escape()
    {
        if (baseEnemy.GetEnemyCurrentHp() < lowHp && !hasEscaped)
        {
            enemyMove.enabled = true;
          //  Debug.LogError("���� ��...");
            anim.SetBool("IsRun", true);
            if (enemyMove.enabled)
            {
                enemyMove.MoveEnemy(originPos);
                enemyMove.SetSpeed();
            }
            hasEscaped = true;
            return INode.STATE.RUN;
        }
        enemyMove.ResetMoveSpeed();
        anim.SetBool("IsRun", false);
        return INode.STATE.FAIL;
    }
    private INode.STATE CheckInAttackRange()
    {
        if (detector.isInPossibleAttackRange)
        {
            anim.SetBool("IsWalk", false);
            return INode.STATE.SUCCESS;
        }
        else
        {
            anim.SetBool("IsAttack", false);
            return INode.STATE.FAIL;
        }
    }
    private INode.STATE SkillAttack()
    {
        if (Time.time - lastSkillTime < skillCooldown)
        {
            return INode.STATE.FAIL;
        }
      //  Debug.LogError("��ų ���� ��");
        bossSkill.ExcuteSkill(detector.detectedTarget);
        enemyMove.enabled = false; 
        anim.SetTrigger("DoSkill");
        lastSkillTime = Time.time;
        StopAllCoroutines();
        StartCoroutine(EnableEnemyMoveAfterAttack(3));

        return INode.STATE.RUN;
    }
    private INode.STATE NormalAttack()
    {
       // Debug.Log("�Ϲ� ���� ��");
        anim.SetBool("IsAttack", true);
        enemyMove.enabled = false;
        StopAllCoroutines();
        StartCoroutine(EnableEnemyMoveAfterAttack(1));
        return INode.STATE.RUN;
    }
    private IEnumerator EnableEnemyMoveAfterAttack(float duration)
    {
        yield return new WaitForSeconds(duration);
        enemyMove.enabled = true;
    }
    private bool CanUseSkill()
    {
        return Time.time - lastSkillTime >= skillCooldown;
    }
    private INode.STATE CheckInDetectiveRange()
    {
        return detector.isDetectedTarget ? INode.STATE.SUCCESS : INode.STATE.FAIL;
    }
    private INode.STATE TraceTarget()
    {
        if (detector.isDetectedTarget && detector.detectedTarget != null)
        {
            if (enemyMove.enabled)
            {
              //  Debug.Log("���� ��");
                anim.SetBool("IsAttack", false);
                anim.SetBool("IsWalk", true);
                enemyMove.PossibleMove();
                enemyMove.MoveEnemy(GameManager.instance.player.transform.position);
            }
            return INode.STATE.RUN;
        }
        anim.SetBool("IsWalk", false);
        return INode.STATE.FAIL;
    }
    private INode.STATE ReturnToOrigin()
    {
        if (!enemyMove.isOriginPos)
        {
           // Debug.Log("���� ��");
            anim.SetBool("IsWalk", true);
            if (enemyMove.enabled)
            {
                enemyMove.MoveEnemy(originPos);
            }
            return INode.STATE.RUN;
        }
        anim.SetBool("IsWalk", false);
        return INode.STATE.FAIL;
    }
    private INode.STATE Idle()
    {
        //Debug.Log("��� ��");
        return INode.STATE.RUN;
    }
    private void Update()
    {
       // Debug.Log("EnemyMove : " + enemyMove.enabled);
        _rootNode.Evaluate();
    }
}
