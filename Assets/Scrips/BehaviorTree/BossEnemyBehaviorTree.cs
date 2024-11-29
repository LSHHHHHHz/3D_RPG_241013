using UnityEngine;
using System.Collections;
using System.Security.Cryptography;

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
    private bool isLocked = false;

    private float skillCooldown = 10f;
    private float lastSkillTime = -1f;

    private float cooldownStartTime = 0f;
    private float cooldownDuration = 0f;

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
        var deathAction = new ActionNode(CheckDeath);

        SelectorNode crisisManagementSelector = new SelectorNode();
        crisisManagementSelector.Add(new ActionNode(Escape));
        crisisManagementSelector.Add(new ActionNode(HealIfLowHP));

        SequenceNode stateSequence = new SequenceNode();
        stateSequence.Add(new ActionNode(CheckHP));
        stateSequence.Add(crisisManagementSelector);

        SequenceNode skillSequence = new SequenceNode();
        skillSequence.Add(new ActionNode(() => CoolDown(3f)));
        skillSequence.Add(new ActionNode(() => CanUseSkill() ? SkillAttack() : INode.STATE.FAIL));

        SelectorNode attackTypeSelector = new SelectorNode();
        attackTypeSelector.Add(skillSequence);
        attackTypeSelector.Add(new ActionNode(NormalAttack));

        SequenceNode attackSequence = new SequenceNode();
        attackSequence.Add(new ActionNode(CheckInAttackRange));
        attackSequence.Add(attackTypeSelector);

        SequenceNode defectiveSequence = new SequenceNode();
        defectiveSequence.Add(new ActionNode(CheckInDetectiveRange));
        defectiveSequence.Add(new ActionNode(TraceTarget));

        var returnAction = new ActionNode(ReturnToOrigin);

        var idleAction = new ActionNode(Idle);

        _rootNode = new SelectorNode();
        _rootNode.Add(deathAction);
        _rootNode.Add(stateSequence);
        _rootNode.Add(attackSequence);
        _rootNode.Add(defectiveSequence);
        _rootNode.Add(returnAction);
        _rootNode.Add(idleAction);
    }

    private INode.STATE CheckDeath()
    {
        if (baseEnemy.GetEnemyCurrentHp() <= 0)
        {
            isLocked = true;
            anim.SetTrigger("DoDie");
            enemyMove.enabled = false;
            StartCoroutine(DelayedDeathActions(3.5f));
            return INode.STATE.SUCCESS;
        }
        return INode.STATE.FAIL;
    }
    private IEnumerator DelayedDeathActions(float delay)
    {
        yield return new WaitForSeconds(delay);
        baseEnemy.PerformDeathActions();
    }
    private INode.STATE CheckHP()
    {
        return baseEnemy.GetEnemyCurrentHp() < lowHp ? INode.STATE.SUCCESS : INode.STATE.FAIL;
    }
    private INode.STATE HealIfLowHP()
    {
        if (baseEnemy.GetEnemyCurrentHp() < lowHp && !hasHealed)
        {
            isLocked = true;
            StopAllCoroutines();
            baseEnemy.RecoverHP(200);
            enemyMove.enabled = false;
            StartCoroutine(EnableEnemyMoveAfterAttack(3));
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
            anim.SetBool("IsRun", true);

            if (enemyMove.enabled)
            {
                enemyMove.PossibleMove();
                enemyMove.MoveOrigin(originPos);
                if (Vector3.Distance(transform.position, originPos) < 0.1f)
                {
                    hasEscaped = true;
                    return INode.STATE.SUCCESS;
                }
            }
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
        bossSkill.ExcuteSkill(detector.detectedTarget);
        enemyMove.enabled = false;
        anim.SetTrigger("DoSkill");
        lastSkillTime = Time.time;

        isLocked = true;

        StopAllCoroutines();
        StartCoroutine(EnableEnemyMoveAfterAttack(3));

        return INode.STATE.RUN;
    }
    private INode.STATE NormalAttack()
    {
        Debug.Log("¾Æ¾Æ");
        anim.SetBool("IsAttack", true);
        enemyMove.StopMove();
        enemyMove.enabled = false;
        StopAllCoroutines();
        StartCoroutine(EnableEnemyMoveAfterAttack(1));
        return INode.STATE.RUN;
    }
    private INode.STATE CoolDown(float duration)
    {
        if (Time.time - lastSkillTime < skillCooldown)
        {
            return INode.STATE.FAIL;
        }
        if (cooldownStartTime == 0f)
        {
            cooldownStartTime = Time.time;
            cooldownDuration = duration;
        }
        if (Time.time - cooldownStartTime >= cooldownDuration)
        {
            cooldownStartTime = 0f;
            return INode.STATE.SUCCESS;
        }
        return INode.STATE.FAIL;
    }
    private IEnumerator EnableEnemyMoveAfterAttack(float duration)
    {
        yield return new WaitForSeconds(duration);
        enemyMove.enabled = true;
        enemyMove.ResetMoveSpeed();
        isLocked = false;
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
        return INode.STATE.RUN;
    }
    private void Update()
    {
        if (!isLocked)
        {
            _rootNode.Evaluate();
        }
    }
}