using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellHitBox : MonoBehaviour
{

    public enum SpellType
    {
        None,
        Fire,
        Water,
        Wind,
        Ice
    }

    void OnGUI()
    {
        GUILayout.Label("Select a spell:");

        if (GUILayout.Toggle(selectedSpell == SpellType.Fire, "Fire"))
        {
            selectedSpell = SpellType.Fire;
        }
        if (GUILayout.Toggle(selectedSpell == SpellType.Water, "Water"))
        {
            selectedSpell = SpellType.Water;
        }
        if (GUILayout.Toggle(selectedSpell == SpellType.Wind, "Wind"))
        {
            selectedSpell = SpellType.Wind;
        }
        if (GUILayout.Toggle(selectedSpell == SpellType.Ice, "Ice"))
        {
            selectedSpell = SpellType.Ice;
        }
    }

    public SpellType selectedSpell = SpellType.None;

    void Start()
    {

    }

    void Update()
    {

    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (selectedSpell == SpellType.Fire)
        {
            FireDamage(col);
        }
    }


    private void FireDamage(Collider2D col)
    {
        IDamageAble damageAble = col.GetComponent<IDamageAble>();
        damageAble?.OnHit(10);
    }
}
