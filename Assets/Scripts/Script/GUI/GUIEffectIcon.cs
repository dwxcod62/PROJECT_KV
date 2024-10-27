using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIEffectIcon : MonoBehaviour
{

    [SerializeField] private RectTransform contentPanel;
    [SerializeField] private GUIIcon iconPrefab;

    void Start()
    {

    }

    public void RemoveIconBySprite(Sprite sprite)
    {
        for (int i = 0; i < contentPanel.childCount; i++)
        {
            Transform child = contentPanel.GetChild(i);
            GUIIcon guiIcon = child.GetComponent<GUIIcon>();

            if (guiIcon != null && guiIcon.GetSprite() == sprite)
            {
                Destroy(child.gameObject);
                break;
            }
        }
    }

    public void setEffectIcon(Sprite sprite)
    {
        GUIIcon a = Instantiate(iconPrefab, Vector2.zero, Quaternion.identity);
        a.transform.SetParent(contentPanel);
        a.setData(sprite);
        a.transform.localScale = Vector3.one;
    }

}
