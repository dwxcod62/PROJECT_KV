
using UnityEngine;


namespace Inventory.Model
{

    public class ItemController : MonoBehaviour
    {
        [field: SerializeField] public Sprite ItemImage;
        [field: SerializeField] public string ItemName;
        [field: SerializeField] public int dropChance;
        public int ID => GetInstanceID();

        [field: SerializeField] public int MaxStackSize = 99;

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
}

