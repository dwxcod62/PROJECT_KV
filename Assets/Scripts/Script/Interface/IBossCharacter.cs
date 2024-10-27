using UnityEngine;

public interface IBossCharacter
{
    public void Attack(int skill);
    public void SetMoveAnimation();

    public void CastThirdSkill();
    public void CastFirstSkill();
    public void CastSecondSkill();

    public void SetLocalTarget(GameObject target);

}
