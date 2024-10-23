using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemController : MonoBehaviour
{
    [SerializeField] public Sprite ItemImage;
    [SerializeField] public string ItemName;
    [SerializeField] public int dropChance;

    public ItemController(string ItemName, int dropChance)
    {
        this.ItemName = ItemName;
        this.dropChance = dropChance;
    }

    void Start()
    {
        int animation_id = Data.GetAniId(ItemName);
        print(animation_id);
        GetComponent<Animator>().SetInteger("Id", animation_id);
    }

}
