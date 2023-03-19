using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadMovement : MonoBehaviour
{
    public GameObject player;
    private float smooth = 30f;

    void Start()
    {
        player = this.gameObject;
    }

    public void lookDown()
    {
        StartCoroutine(lookDownCorotine());
    }

    public void lookUp()
    {
        StartCoroutine(lookUpCorotine());
    }

    IEnumerator lookDownCorotine()
    {
        for(int i = 0; i < 70; i++)
        {
            //Finds transform from axis
            float tiltAroundX = 43.63f;
            float tileAroundY = 27.32f;

            //Rotate to target position
            Quaternion target = Quaternion.Euler(tiltAroundX, tileAroundY, 0);

            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, target, Time.deltaTime * smooth);

            yield return new WaitForSeconds(.01f);
        }
    }

    IEnumerator lookUpCorotine()
    {
        for (int i = 0; i < 70; i++)
        {
            //Finds transform from axis
            float tiltAroundX = 0f;
            float tileAroundY = 0f;

            //Rotate to target position
            Quaternion target = Quaternion.Euler(tiltAroundX, tileAroundY, 0);

            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, target, Time.deltaTime * smooth);

            yield return new WaitForSeconds(.01f);
        }
    }
}
