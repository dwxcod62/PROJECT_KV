using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEffect
{
    void ApplyPoisonEffect();
    void ApplyPetrifyEffect();
    void ApplyShockEffect();
    void RemoveEffect();
}
