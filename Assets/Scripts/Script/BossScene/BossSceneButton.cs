using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScene_Button : MonoBehaviour
{
    private GameObject selectedBoss;

    public void Spawn()
    {
        selectedBoss = GetComponentInParent<GUIBossScene>().selectedBoss;

        if (selectedBoss != null)
        {
            var a = Instantiate(selectedBoss, Vector3.zero, Quaternion.identity);
            GetComponentInParent<GUIBossScene>().setObject(a);
        }
    }

    public void Change()
    {
        GetComponentInParent<GUIBossScene>().ChangeBoss();
    }

    public void Move()
    {
        selectedBoss = GetComponentInParent<GUIBossScene>().selectedBoss;
        IBossCharacter boss = selectedBoss.GetComponent<IBossCharacter>();
        boss.SetMoveAnimation();
    }

    public void Skill_1()
    {
        selectedBoss = GetComponentInParent<GUIBossScene>().selectedBoss;
        IBossCharacter boss = selectedBoss.GetComponent<IBossCharacter>();
        boss.Attack(1);
    }

    public void Skill_2()
    {
        selectedBoss = GetComponentInParent<GUIBossScene>().selectedBoss;
        IBossCharacter boss = selectedBoss.GetComponent<IBossCharacter>();
        boss.Attack(2);
    }

    public void Skill_3()
    {
        selectedBoss = GetComponentInParent<GUIBossScene>().selectedBoss;
        IBossCharacter boss = selectedBoss.GetComponent<IBossCharacter>();
        boss.Attack(3);
    }

}
