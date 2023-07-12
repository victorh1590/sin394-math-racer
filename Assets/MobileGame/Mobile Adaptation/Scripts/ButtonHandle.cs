using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
[RequireComponent(typeof(Button))]
public class ButtonHandle : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    public bool pressed = false;
    public bool down = false;
    bool downaux = false;
    void Update()
    {

        /*
        if (Application.isMobilePlatform)
        {
            if (down)
            {
                downaux = true;
            }
        }*/
        if (downaux)
        {
            down = false;
            downaux = false;
        }
        if (down)
        {
            downaux = true;
        }

    }
    //Do this when the mouse is clicked over the selectable object this script is attached to.
    public void OnPointerClick(PointerEventData eventData)
    {
        down = true;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        pressed = true;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        pressed = false;
    }
}