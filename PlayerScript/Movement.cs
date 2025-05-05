using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : Singleton<Movement>
{
    private PlayerInputActions playerInputActions;

    [SerializeField] public float movingSpeed = 4f;
    private float normalSpeed = 4f;

    [SerializeField] private Rigidbody2D rb;
    private Animator animator;
    private Vector2 direction;

    [SerializeField] private Transform AttackCollider;
    [SerializeField] private Transform DashCollider;

    private EnemyKnockback knockback;
    private SkillPointManager skillPointManager;
    private TrailRenderer myTrailRenderer;
    private PlayerHealth playerHealth;

    private bool isDashing = false;
    private bool canSpeedUp = true;

    [SerializeField] private GameObject dashIndicator;
    public float dashCD = 3f;

    public bool hasDestroyedPrison = false;

    protected override void Awake() {
        base.Awake();
        playerInputActions = new PlayerInputActions();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        knockback = GetComponent<EnemyKnockback>();
        skillPointManager = GetComponent<SkillPointManager>();
        myTrailRenderer = GetComponent<TrailRenderer>();
        playerHealth = GetComponent<PlayerHealth>();
    }
    private void OnEnable()
    {
        playerInputActions.Enable();
    }
    private void Update()
    {
        PlayerInput();
        RotateAttackCollider();
    }
    private void FixedUpdate()
    {
        PlayerMovement();
        SpeedUp();
    }
    private void PlayerInput()
    {
        direction = playerInputActions.Player.Movement.ReadValue<Vector2>().normalized;

        if (direction != Vector2.zero)
        {
            animator.SetFloat("Horizontal", direction.x);
            animator.SetFloat("Vertical", direction.y);
        }
        animator.SetFloat("Speed", direction.sqrMagnitude);
    }
    private void PlayerMovement()
    {
        rb.MovePosition(rb.position + direction * (movingSpeed * Time.fixedDeltaTime));
    }
    private void SpeedUp()
    {
        if (Input.GetKey(KeyCode.LeftShift) && canSpeedUp)
        {
            movingSpeed = 8;
        }
        else if (!isDashing)
        {
            movingSpeed = normalSpeed;
        }
    }
    public void AttackState()
    {
        animator.SetTrigger("Attack");
        AttackCollider.gameObject.SetActive(true);
    }
    public void DoneAttackState()
    {
        AttackCollider.gameObject.SetActive(false);
    }
    public void DashState()
    {
        if (!isDashing)
        {
            playerHealth.currentHealth -= 1;
            canSpeedUp = false;
            Physics2D.IgnoreLayerCollision(6, 7, true);
            isDashing = true;
            myTrailRenderer.emitting = true;
            animator.SetBool("IsDashing", true);
            DashCollider.gameObject.SetActive(true);
            dashIndicator.gameObject.SetActive(false);
            StartCoroutine(DashRoutine());
        }
    }
    private IEnumerator DashRoutine()
    {
        float dashSpeed = 30f;
        float dashTime = 0.2f;
        movingSpeed = dashSpeed;
        yield return new WaitForSeconds(dashTime);
        DashCollider.gameObject.SetActive(false);
        animator.SetBool("IsDashing", false);
        myTrailRenderer.emitting = false;
        movingSpeed = normalSpeed;
        canSpeedUp = true;
        yield return new WaitForSeconds(dashCD);
        Physics2D.IgnoreLayerCollision(6, 7, false);
        isDashing = false;
        dashIndicator.gameObject.SetActive(true);
    }
    private void RotateAttackCollider()
    {
        if (direction != Vector2.zero)
        {
            if (direction.x == 0 && direction.y == -1 || direction.x == 0 && direction.y == 0)
            {
                AttackCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
                AttackCollider.transform.localPosition = new Vector3(0, 0, 0);

                DashCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
                DashCollider.transform.localPosition = new Vector3(0, 0.5f, 0);
            }
            if (direction.x == 0 && direction.y == 1)
            {
                AttackCollider.transform.rotation = Quaternion.Euler(180, 0, 0);
                AttackCollider.transform.localPosition = new Vector3(0, 1, 0);

                DashCollider.transform.rotation = Quaternion.Euler(180, 0, 0);
                DashCollider.transform.localPosition = new Vector3(0, 0.5f, 0);
            }
            if (direction.x == -1 && direction.y == 0)
            {
                AttackCollider.transform.rotation = Quaternion.Euler(0, 0, -90);
                AttackCollider.transform.localPosition = new Vector3(-0.5f, 0.6f, 0);

                DashCollider.transform.rotation = Quaternion.Euler(0, 0, -90);
                DashCollider.transform.localPosition = new Vector3(0, 0.5f, 0);
            }
            if (direction.x == 1 && direction.y == 0)
            {
                AttackCollider.transform.rotation = Quaternion.Euler(0, 0, 90);
                AttackCollider.transform.localPosition = new Vector3(0.6f, 0.5f, 0);

                DashCollider.transform.rotation = Quaternion.Euler(0, 0, 90);
                DashCollider.transform.localPosition = new Vector3(0, 0.5f, 0);
            }
        }
    }
    //helo my name is loem krutoi what :D
    //plese donate me
    //i develope r i ned yogurt i love yogurt
}