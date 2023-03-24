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
            GameObject clickedObject = getClickedObject(out _);

            if(clickedObject == apps[0])
            {
                PhoneScript._website = PhoneScript.WebsiteState.Website1;
                PhoneScript.instance.StateChange();

                HeadMovement._position = HeadMovement.Position.Website;
            }
            if (clickedObject == apps[1])
            {
                PhoneScript._website = PhoneScript.WebsiteState.Website2;
                PhoneScript.instance.StateChange();

                HeadMovement._position = HeadMovement.Position.Website;
            }
            if (clickedObject == apps[2])
            {
                PhoneScript._website = PhoneScript.WebsiteState.Website3;
                PhoneScript.instance.StateChange();

                HeadMovement._position = HeadMovement.Position.Website;
            }
            if (clickedObject == apps[3])
            {
                PhoneScript._website = PhoneScript.WebsiteState.Website4;
                PhoneScript.instance.StateChange();

                HeadMovement._position = HeadMovement.Position.Website;
            }
            if (clickedObject == apps[4])
            {
                PhoneScript._website = PhoneScript.WebsiteState.Website5;
                PhoneScript.instance.StateChange();

                HeadMovement._position = HeadMovement.Position.Website;
            }
            if (clickedObject == apps[5])
            {
                PhoneScript._website = PhoneScript.WebsiteState.Website6;
                PhoneScript.instance.StateChange();

                HeadMovement._position = HeadMovement.Position.Website;
            }
            if (clickedObject == apps[6])
            {
                PhoneScript._website = PhoneScript.WebsiteState.Website7;
                PhoneScript.instance.StateChange();

                HeadMovement._position = HeadMovement.Position.Website;
            }
            if (clickedObject == apps[7])
            {
                PhoneScript._website = PhoneScript.WebsiteState.Website8;
                PhoneScript.instance.StateChange();

                HeadMovement._position = HeadMovement.Position.Website;
            }
            if (clickedObject == apps[8])
            {
                PhoneScript._website = PhoneScript.WebsiteState.Website9;
                PhoneScript.instance.StateChange();

                HeadMovement._position = HeadMovement.Position.Website;
            }
            if (clickedObject == apps[9])
            {
                PhoneScript._website = PhoneScript.WebsiteState.Selecting;
                PhoneScript.instance.StateChange();

                HeadMovement._position = HeadMovement.Position.Phone;
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
