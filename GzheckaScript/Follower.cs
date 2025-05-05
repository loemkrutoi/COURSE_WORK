using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public GameObject followChar;
    private Vector2 direction;
    public Animator animator;
    public float followDistance = 1f;
    public List<Vector2> followCharPos = new List<Vector2>();
    public float allowableSampleDistance = 0f;
    private float sampleTimeDifference;
    float sampleTime;
    public float removeDistance = 1f;
    public float fastSpeed = 8f;
    public float normalSpeed = 4f;
    public float followSpeed = 4f;
    public float fastDistance = 2f;
    private void Awake()
    {
        followChar = GameObject.FindWithTag("Player");
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        sampleTime = Time.time;
        followCharPos.Add(followChar.transform.position);
        followSpeed = fastSpeed;
    }
    private void Update()
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
        animator.SetFloat("Speed", direction.sqrMagnitude);

        if (Time.time > sampleTime)
        {
            sampleTime = Time.time + sampleTimeDifference;

            if (Vector2.Distance(transform.position, followChar.transform.position) > followDistance &&
                Vector2.Distance(followChar.transform.position, followCharPos[followCharPos.Count - 1]) > allowableSampleDistance)
            {
                followCharPos.Add(followChar.transform.position);
            }
        }
        if (Vector2.Distance(transform.position, followChar.transform.position) > fastDistance) { followSpeed = fastSpeed; }
        else { followSpeed = normalSpeed; }
        if (Vector2.Distance(transform.position, followChar.transform.position) > followDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, followCharPos[0], Time.deltaTime * followSpeed);
            if (Vector2.Distance(transform.position, followCharPos[0]) < removeDistance)
            {
                if (followCharPos.Count > 1)
                {
                    followCharPos.RemoveAt(0);
                }
            }
        }
    }
}