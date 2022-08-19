using System.Collections;
using UnityEngine;

public class CutThePlant : State
{
    private AttackState State = AttackState.Ready;

    private enum AttackState
    {
        Ready,
        WaitingAttackEnd,
    }

    protected override void Update()
    {
        TryAttack();
    }

    private void TryAttack()
    {
        if (State == AttackState.Ready)
        {
            if (_attack != null)
                StopCoroutine(_attack);

            _attack = StartCoroutine(Attack(_attackDelay));
        }
    }

    private IEnumerator Attack(float attackDelay)
    {
        _playerAnimatorController.Attack();
        ChangeAttackState(AttackState.WaitingAttackEnd);
        yield return new WaitForSeconds(attackDelay);
        ChangeAttackState(AttackState.Ready);
    }

    private void ChangeAttackState(AttackState changedState)
    {
        if (State != changedState)
        {
            State = changedState;
        }
    }
}
