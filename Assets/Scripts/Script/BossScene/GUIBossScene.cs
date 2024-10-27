using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GUIBossScene : MonoBehaviour
{
    [SerializeField] public List<GameObject> ListBoss;
    [SerializeField] private GameObject skillUI;

    public GameObject selectedBoss = null;
    string bossName;
    private int currentIndex = 0;
    void Start()
    {
        // selectedBoss = ListBoss[0];
    }

    void FixedUpdate()
    {
        if (selectedBoss == null)
        {
            skillUI.SetActive(false);
        }


    }

    public void setObject(GameObject gameObject)
    {
        selectedBoss = gameObject;
        skillUI.SetActive(true);
    }

    public void ChangeBoss()
    {
        if (ListBoss.Count > 0)
        {
            selectedBoss = ListBoss[currentIndex];

            currentIndex++;

            if (currentIndex >= ListBoss.Count)
            {
                currentIndex = 0;
            }
        }
        else
        {
            Debug.LogWarning("ListBoss is empty!");
        }
    }

}
