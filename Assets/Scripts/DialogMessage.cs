using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogMessage : MonoBehaviour
{
    private static DialogMessage instance;
    public static DialogMessage Instance => instance;

    [SerializeField]
    public Transform CameraTrasform;

    [SerializeField]
    private TMP_Text MessageText;

    private Queue<string> messageQueue = new();
    private string currentMessage = null;

    public float LetterPerSecond = 0.1f;
    public float SecondsToLeave = 2f;
    private float t = 0;
    private int currentIndex = 1;

    public float DistanceFromCamera = 1.0f;
    public float HeightOffset = 0.0f;
    public float FollowSpeed = 3.0f;
    private Vector3 velocity = Vector3.zero;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (currentMessage != null)
        {
            t += Time.deltaTime;
            if (currentIndex <= currentMessage.Length)
            {
                if (t > LetterPerSecond)
                {
                    MessageText.text = currentMessage.Substring(0, currentIndex);
                    currentIndex++;
                    t = 0;
                }
            }
            else
            {
                if (t > SecondsToLeave)
                {
                    MessageText.text = "";
                    currentMessage = null;
                    currentIndex = 1;
                    t = 0;
                }
            }
        } 
        else // null
        {
            if (messageQueue.Count > 0) currentMessage = messageQueue.Dequeue();
        }
        followTransform();
    }

    public void AddMessage(string message)
    {
        messageQueue.Enqueue(message);
    }

    void followTransform()
    {
        // 목표 위치 계산
        Vector3 targetPosition = CameraTrasform.position + CameraTrasform.forward * DistanceFromCamera;
        targetPosition.y += HeightOffset;

        // 부드러운 위치 이동
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, FollowSpeed * Time.deltaTime);

        // 목표 회전 계산
        Quaternion targetRotation = Quaternion.LookRotation(transform.position - CameraTrasform.position);

        // 부드러운 회전
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, FollowSpeed * Time.deltaTime);
    }
}
