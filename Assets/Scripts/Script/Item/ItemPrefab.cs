using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPrefab : MonoBehaviour
{
    private Loot loot;

    public void setData(Loot loot)
    {
        this.loot = loot;
    }

}
