using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingFlickerTitle : MonoBehaviour
{
    public Light Overhead;
    public Light Spotlight;
    public AudioSource lightSound;
    private bool isFlashing;

    private void Start()
    {
        StartCoroutine(FlickerCorotine());
        StartCoroutine(BeginFlicker());
    }

    private void Update()
    {
        if(isFlashing == true)
        {
            if (Random.value > 0.97)
            {
                if (Overhead.enabled == true)
                {
                    Overhead.enabled = false;
                    Spotlight.enabled = false;
                    lightSound.Stop();
                }
                else
                {
                    Overhead.enabled = true;
                    Spotlight.enabled = true;
                    lightSound.Play();
                }
            }
        }
    }

    IEnumerator BeginFlicker()
    {
        isFlashing = true;

        yield return new WaitForSeconds(10f);
        StartCoroutine(BeginFlicker());
    }

    IEnumerator FlickerCorotine()
    {
        isFlashing = false;
        Overhead.enabled = true;
        Spotlight.enabled = true;
        lightSound.Play();

        yield return new WaitForSeconds(3f);
        StartCoroutine(FlickerCorotine());
    }
}
