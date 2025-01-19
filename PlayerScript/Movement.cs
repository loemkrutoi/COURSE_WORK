using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float movingSpeed = 4f;
    [SerializeField] private float fastSpeed = 8f;
    //[SerializeField] private float dashSpeed = 10f;
    //[SerializeField] private TrailRenderer myTrailRenderer;
    private float normalSpeed = 4f;
    private float slowSpeed = 2f;

    private PlayerInputActions playerInputActions;
    public static Movement Instance;

    public Rigidbody2D rb;
    private Animator animator;
    private Vector2 direction;
    private SpriteRenderer mySpriteRenderer;

    //private bool isDashing = false;

    [SerializeField] private Transform AttackCollider;

    private void Awake()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();
    }
    private void Start()
    {
        playerInputActions.Player.Attack.started += _ => AttackState();
        //playerInputActions.Player.Dash.started += _ => DashState();
    }
    private void Update()
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
        animator.SetFloat("Speed", direction.sqrMagnitude);
    }
    private void FixedUpdate()
    {
        Vector2 inputVector = GetMovementVector();
        inputVector = inputVector.normalized;
        rb.MovePosition(rb.position + inputVector * (movingSpeed * Time.fixedDeltaTime));
        SpeedUp();
        RotateAttackCollider();
    }
    private Vector2 GetMovementVector()
    {
        Vector2 inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();
        return inputVector;
    }
    private void SpeedUp()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movingSpeed = fastSpeed;
        }
        else if (Input.GetKey(KeyCode.Mouse1))
        {
            movingSpeed = slowSpeed;
        }
        else
        {
            movingSpeed = normalSpeed;
        }
    }
    private void AttackState()
    {
        animator.SetTrigger("Attack");
        AttackCollider.gameObject.SetActive(true);
    }
    public void DoneAttackState()
    {
        AttackCollider.gameObject.SetActive(false);
    }
    private void RotateAttackCollider()
    {
        if (direction.x == 0 && direction.y == -1 || direction.x == 0 && direction.y == 0) //down
        {
            AttackCollider.transform.rotation = Quaternion.Euler(0,0,0);
            AttackCollider.transform.localPosition = new Vector3(0, 0, 0);
        }
        else if(direction.x == 0 && direction.y == 1) //up
        {
            AttackCollider.transform.rotation = Quaternion.Euler(180, 0, 0);
            AttackCollider.transform.localPosition = new Vector3(0, 1, 0);
        }
        else if (direction.x == -1 && direction.y == 0) //left
        {
            AttackCollider.transform.rotation = Quaternion.Euler(0, 0, -90);
            AttackCollider.transform.localPosition = new Vector3(-0.5f, 0.6f, 0);
        }
        else if(direction.x == 1 && direction.y == 0) //right
        {
            AttackCollider.transform.rotation = Quaternion.Euler(0, 0, 90);
            AttackCollider.transform.localPosition = new Vector3(0.6f, 0.5f, 0);
        }
    }
    //private void DashState()
    //{
    //    if (!isDashing)
    //    {
    //        isDashing = true;
    //        movingSpeed = dashSpeed;
    //        myTrailRenderer.emitting = true;
    //        StartCoroutine(EndDashRoutine());
    //    }
    //}
    //private IEnumerator EndDashRoutine()
    //{
    //    float dashTime = 0.2f;
    //    float dashCD = 0.25f;
    //    yield return new WaitForSeconds(dashTime);
    //    movingSpeed /= dashSpeed;
    //    myTrailRenderer.emitting = false;
    //    yield return new WaitForSeconds(dashCD);
    //    isDashing = false;
    //}
}
