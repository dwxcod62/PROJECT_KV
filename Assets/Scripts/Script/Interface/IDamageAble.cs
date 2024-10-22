
using UnityEngine;

public interface IDamageAble
{
    public float Health { set; get; }

    public float MaxHealth { get; }
    public bool TargetAble { set; get; }

    void OnHit(float damage, Vector2 knockValue);
    void OnHit(float damage);

}