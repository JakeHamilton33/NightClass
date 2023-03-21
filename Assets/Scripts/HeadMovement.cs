using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadMovement : MonoBehaviour
{

    #region Stored Coordinates
    /* 
    Head:
        Head Paper Rotation : { Quaternion(0.202, 0.0, 0.0, 0.979) }
        Head Phone Rotation : { Quaternion(0.315, 0.159, -0.054, 0.934) }
        Head Zhang Rotation : { Quaternion(0.0, -0.165, 0.0, 0.986) }
        Head Class Rotation : { Quaternion(0.086, -0.544, 0.063, 0.833) }
    Phone:
        Phone Default Position: { Vector3(1.6, 0.88, 0.18) }
        Phone Up Position: {Vector3(0.452, 1.585, 0.099) }

        Phone Default Rotation: { Quaternion(-0.036, -0.29, -0.34, 0.9) }
        Phone Up Rotation: { Quaternion(0.186, -0.549, -0.261, 0.772) }
    */
    private Quaternion headPaperRotation = new Quaternion(0.202f, 0.0f, 0.0f, 0.979f);
    private Quaternion headPhoneRotation = new Quaternion(0.315f, 0.159f, -0.054f, 0.934f);
    private Quaternion headZhangRotation = new Quaternion(0.0f, -0.165f, 0.0f, 0.986f);
    private Quaternion headClassRotation = new Quaternion(0.086f, -0.544f, 0.063f, 0.833f);

    private Vector3 phoneDownPos = new Vector3(1.6f, 0.88f, 0.18f);
    private Quaternion phoneDownRot = new Quaternion(-0.036f, -0.29f, -0.34f, 0.9f);

    private Vector3 phoneUpPos = new Vector3(0.452f, 1.585f, 0.099f);
    private Quaternion phoneUpRot = new Quaternion(0.186f, -0.549f, -0.261f, 0.772f);

    #endregion

    #region Declarations
    enum Position { Paper, Professor, Phone, Classmate, Moving}
    Position _position = Position.Paper;

    public GameObject player, phone, mainCamera;
    private float duration = 0.6f;

    #endregion

    #region Methods
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
            yield return null;
            while (Time.time < endTime)
            {
                float progress = (Time.time - startTime) / duration;

                this.transform.rotation = Quaternion.Slerp(headPaperRotation, headPhoneRotation, progress);

                phone.transform.rotation = Quaternion.Slerp(phoneDownRot, phoneUpRot, progress);
                phone.transform.position = Vector3.Slerp(phoneDownPos, phoneUpPos, progress);
                yield return null;
            }
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
            yield return null;
            while (Time.time < endTime)
            {
                float progress = (Time.time - startTime) / duration;
                this.transform.rotation = Quaternion.Slerp(headPaperRotation, headZhangRotation, progress);
                yield return null;
            }
            _position = Position.Professor;
        }

    }
    IEnumerator lookAtPaperCoroutine()
    {
        if (_position == Position.Phone)
        {
            _position = Position.Moving;
            float startTime = Time.time;
            float endTime = startTime + duration;
            yield return null;
            while (Time.time < endTime)
            {
                float progress = (Time.time - startTime) / duration;

                this.transform.rotation = Quaternion.Slerp(headPhoneRotation, headPaperRotation, progress);

                phone.transform.rotation = Quaternion.Slerp(phoneUpRot, phoneDownRot, progress);
                phone.transform.position = Vector3.Slerp(phoneUpPos, phoneDownPos, progress);
                yield return null;
            }
            _position = Position.Paper;
        }

        if (_position == Position.Classmate)
        {
            _position = Position.Moving;
            float startTime = Time.time;
            float endTime = startTime + duration;
            yield return null;
            while (Time.time < endTime)
            {
                float progress = (Time.time - startTime) / duration;
                this.transform.rotation = Quaternion.Slerp(headClassRotation, headPaperRotation, progress);
                yield return null;
            }
            _position = Position.Paper;
        }

        if (_position == Position.Professor)
        {
            _position = Position.Moving;
            float startTime = Time.time;
            float endTime = startTime + duration;
            yield return null;
            while (Time.time < endTime)
            {
                float progress = (Time.time - startTime) / duration;
                this.transform.rotation = Quaternion.Slerp(headZhangRotation, headPaperRotation, progress);
                yield return null;
            }
            _position = Position.Paper;
        }

    }
    IEnumerator lookAtClassmateCoroutine()
    {
        if (_position == Position.Paper)
        {
            _position = Position.Moving;
            float startTime = Time.time;
            float endTime = startTime + duration;
            yield return null;
            while (Time.time < endTime)
            {
                float progress = (Time.time - startTime) / duration;
                this.transform.rotation = Quaternion.Slerp(headPaperRotation, headClassRotation, progress);
                yield return null;
            }
            _position = Position.Classmate;
        }
    }

    #endregion

}
