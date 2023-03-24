using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickObject : MonoBehaviour
{
    public GameObject[] apps;

    // Update is called once per frame
    void Update()
    {
        #region Return Clicked App
        if (Input.GetMouseButtonDown(0))
        {
            if(apps[0] == getClickedObject(out RaycastHit hit1))
            {
                print("clicked app1");
            }
            if (apps[1] == getClickedObject(out RaycastHit hit2))
            {
                print("clicked app2");
            }
            if (apps[2] == getClickedObject(out RaycastHit hit3))
            {
                print("clicked app3");
            }
            if (apps[3] == getClickedObject(out RaycastHit hit4))
            {
                print("clicked app4");
            }
            if (apps[4] == getClickedObject(out RaycastHit hit5))
            {
                print("clicked app5");
            }
            if (apps[5] == getClickedObject(out RaycastHit hit6))
            {
                print("clicked app6");
            }
            if (apps[6] == getClickedObject(out RaycastHit hit7))
            {
                print("clicked app7");
            }
            if (apps[7] == getClickedObject(out RaycastHit hit8))
            {
                print("clicked app8");
            }
            if (apps[8] == getClickedObject(out RaycastHit hit9))
            {
                print("clicked app9");
            }
        }
        #endregion
    }

    GameObject getClickedObject(out RaycastHit hit)
    {
        GameObject target = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray.origin, ray.direction * 10, out hit))
        {
            if (!isPointerOverUIObject())
            {
                target = hit.collider.gameObject;
            }
        }
        return target;
    }

    private bool isPointerOverUIObject()
    {
        PointerEventData ped = new PointerEventData(EventSystem.current);
        ped.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(ped, results);
        return results.Count > 0;
    }
}
