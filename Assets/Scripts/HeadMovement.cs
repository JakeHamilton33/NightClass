using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadMovement : MonoBehaviour
{
    #region Stored Coordinates
    /* 
    Head:
        Head Paper Rotation : { Quaternion(0.202, 0.0, 0.0, 0.979) }
        Head Phone Rotation : { Quaternion(0.318, 0.234, -0.028, 0.918) }
        Head Zhang Rotation : { Quaternion(0.0, -0.165, 0.0, 0.986) }
        Head Class Rotation : { Quaternion(0.086, -0.544, 0.063, 0.833) }
        Head Cauht Rotation : { Quaternion(-0.037, -0.159, -0.006, 0.987) }
    Phone:
        Phone Default Position: { Vector3(1.6, 0.88, 0.18) }
        Phone Up Position: {Vector3(0.702, 1.585, 0.012) }

        Phone Default Rotation: { Quaternion(-0.036, -0.29, -0.34, 0.9) }
        Phone Up Rotation: { Quaternion(0.205, -0.49, -0.246, 0.811) }
    */
    private Quaternion headPaperRotation = new Quaternion(0.202f, 0.0f, 0.0f, 0.979f);
    private Quaternion headPhoneRotation = new Quaternion(0.318f, 0.234f, -0.028f, 0.918f);
    private Quaternion headZhangRotation = new Quaternion(0.0f, -0.083f, 0.0f, 0.986f);
    private Quaternion headClassRotation = new Quaternion(0.086f, -0.544f, 0.063f, 0.833f);
    private Quaternion headCaughtRotation = new Quaternion(-0.037f, -0.159f, -0.006f, 0.987f);

    private Vector3 phoneDownPos = new Vector3(1.6f, 0.88f, 0.18f);
    private Quaternion phoneDownRot = new Quaternion(-0.036f, -0.29f, -0.34f, 0.9f);

    private Vector3 phoneUpPos = new Vector3(0.702f, 1.585f, 0.012f);
    private Quaternion phoneUpRot = new Quaternion(0.205f, -0.49f, -0.246f, 0.811f);

    #endregion

    #region Declarations
    //Made _position public static so it can be shared between scripts
    public enum Position { Paper, Professor, Phone, Classmate, Moving, Website, Caught}
    public static Position _position;

    public GameObject player, phone, mainCamera;
    private float duration = 0.7f;

    #endregion

    #region Methods
    private void Awake()
    {
        _position = Position.Paper;
    }
    public void lookDown()
    {
        if(_position == Position.Professor)
        {
            StartCoroutine(lookAtPaperCoroutine());
        }
        
        if(_position == Position.Paper)
        {
            StartCoroutine(lookAtPhoneCoroutine());
        }
    }
    public void lookLeft()
    {
        if(_position == Position.Paper)
        {
            StartCoroutine(lookAtClassmateCoroutine());
        }
    }
    public void lookRight()
    {
        if(_position == Position.Classmate)
        {
            StartCoroutine(lookAtPaperCoroutine());
        }
    }
    public void lookUp()
    {
        if(_position == Position.Phone)
        {
            StartCoroutine(lookAtPaperCoroutine());
        }
        if(_position == Position.Paper)
        {
            StartCoroutine(LookAtProfessorCoroutine());
        }
        if(_position == Position.Website)
        {
            StartCoroutine(lookAtPaperCoroutine());
        }
    }

    public Quaternion getCurrentPosition()
    {
        switch (_position)
        {
            case Position.Phone:
                {
                    return headPhoneRotation;
                }
            case Position.Classmate:
                {
                    return headClassRotation;
                }
        }
        return new Quaternion(0f, 0f, 0f, 0f);
    }

    public void Caught()
    {
        StartCoroutine(CaughtCoroutine());
    }

    #endregion

    #region Coroutines
    IEnumerator lookAtPhoneCoroutine()
    {
        if(_position == Position.Paper)
        {
            _position = Position.Moving;
            float startTime = Time.time;
            float endTime = startTime + duration;

            while (Time.time <= endTime)
            {
                float progress = (Time.time - startTime) / duration;

                transform.rotation = Quaternion.Slerp(headPaperRotation, headPhoneRotation, progress);

                phone.transform.rotation = Quaternion.Slerp(phoneDownRot, phoneUpRot, progress);
                phone.transform.position = Vector3.Slerp(phoneDownPos, phoneUpPos, progress);

                yield return null;
            }

            //Fixes bug with slerp that prevents completion every 1/25 times
            transform.rotation = headPhoneRotation;

            phone.transform.rotation = phoneUpRot;
            phone.transform.position = phoneUpPos;

            yield return null;
            _position = Position.Phone;
        }

    }
    IEnumerator LookAtProfessorCoroutine()
    {
        if (_position == Position.Paper)
        {
            _position = Position.Moving;
            float startTime = Time.time;
            float endTime = startTime + duration;

            while (Time.time <= endTime)
            {
                float progress = (Time.time - startTime) / duration;

                transform.rotation = Quaternion.Slerp(headPaperRotation, headZhangRotation, progress);

                yield return null;
            }

            //Fixes bug with slerp that prevents completion every 1/25 times
            transform.rotation = headZhangRotation;

            yield return null;
            _position = Position.Professor;
        }

    }
    IEnumerator lookAtPaperCoroutine()
    {
        #region From Phone
        if(_position == Position.Website)
        {
            PhoneScript.instance.SendBack();
            _position = Position.Phone;
        }

        if (_position == Position.Phone)
        {
            _position = Position.Moving;
            float startTime = Time.time;
            float endTime = startTime + duration;
            yield return null;
            while (Time.time <= endTime)
            {
                float progress = (Time.time - startTime) / duration;

                transform.rotation = Quaternion.Slerp(headPhoneRotation, headPaperRotation, progress);

                phone.transform.rotation = Quaternion.Slerp(phoneUpRot, phoneDownRot, progress);
                phone.transform.position = Vector3.Slerp(phoneUpPos, phoneDownPos, progress);

                yield return null;
            }

            //Fixes bug with slerp that prevents completion every 1/25 times
            transform.rotation = headPaperRotation;

            phone.transform.rotation = phoneDownRot;
            phone.transform.position = phoneDownPos;

            yield return null;
            _position = Position.Paper;
        }
        #endregion

        #region From Classmate
        if (_position == Position.Classmate)
        {
            _position = Position.Moving;
            float startTime = Time.time;
            float endTime = startTime + duration;
            yield return null;
            while (Time.time <= endTime)
            {
                float progress = (Time.time - startTime) / duration;
                transform.rotation = Quaternion.Slerp(headClassRotation, headPaperRotation, progress);
                yield return null;
            }

            //Fixes bug with slerp that prevents completion every 1/25 times
            transform.rotation = headPaperRotation;

            yield return null;
            _position = Position.Paper;
        }
        #endregion

        #region From Professor
        if (_position == Position.Professor)
        {
            _position = Position.Moving;
            float startTime = Time.time;
            float endTime = startTime + duration;
            yield return null;
            while (Time.time <= endTime)
            {
                float progress = (Time.time - startTime) / duration;
                transform.rotation = Quaternion.Slerp(headZhangRotation, headPaperRotation, progress);
                yield return null;
            }

            //Fixes bug with slerp that prevents completion every 1/25 times
            transform.rotation = headPaperRotation;

            yield return null;
            _position = Position.Paper;
        }
        #endregion

    }
    IEnumerator lookAtClassmateCoroutine()
    {
        if (_position == Position.Paper)
        {
            _position = Position.Moving;
            float startTime = Time.time;
            float endTime = startTime + duration;
            yield return null;
            while (Time.time <= endTime)
            {
                float progress = (Time.time - startTime) / duration;
                transform.rotation = Quaternion.Slerp(headPaperRotation, headClassRotation, progress);
                yield return null;
            }

            //Fixes bug with slerp that prevents completion every 1/25 times
            transform.rotation = headClassRotation;

            yield return null;
            _position = Position.Classmate;
        }
    }

    IEnumerator CaughtCoroutine()
    {
        Quaternion currentPosition = getCurrentPosition();
        _position = Position.Moving;
        float startTime = Time.time;
        float endTime = startTime + duration;
        yield return null;
        while (Time.time <= endTime)
        {
            float progress = (Time.time - startTime) / duration;
            transform.rotation = Quaternion.Slerp(currentPosition, headCaughtRotation, progress);
            yield return null;
        }

        transform.rotation = headCaughtRotation;
        yield return null;
        _position = Position.Caught;

        PhoneScript.instance.EndGame();
    }

    #endregion

}
