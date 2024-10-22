using UnityEngine;
using UnityEngine.EventSystems;

public class testUi : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        print("click");
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
