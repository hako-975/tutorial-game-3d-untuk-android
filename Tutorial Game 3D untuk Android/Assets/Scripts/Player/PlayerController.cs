using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // joystick
    Joystick joystick;
    JoyButton joyButton;

    public float turnSmoothTime = 0.1f;
    public float movementSpeed = 4f;
    public float jumpHeight = 2f;
    public float gravity = -9.81f;
    public float groundDistance = 0.25f;
    public float maxFallZone = -100f;

    public LayerMask groundMask;


    CharacterController characterController;

    PlayerStats playerStats;

    Animator animator;

    GameObject cam;
    GameObject groundCheck;
    GameObject spawnPoint;

    Vector3 move;
    Vector3 velocity;

    float turnSmoothVelocity;
    float canJump = 0f;
    float horizontal;
    float vertical;

    bool isGrounded;
    bool isRunning;

    [HideInInspector]
    public bool isCombat;

    [HideInInspector]
    public bool isAttack = false;

    [HideInInspector]
    public bool isDying = false;

    [HideInInspector]
    public bool isGetHit = false;

    [HideInInspector]
    public float currentTransformY;

    [HideInInspector]
    public bool isChangeJacket = false;

    public GetHitSensorPlayer sensorPlayer;

    JacketsScript jacketsScript;
    GameObject playerJacket;


    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerStats = GetComponent<PlayerStats>();

        animator = GetComponentInChildren<Animator>();

        cam = GameObject.FindGameObjectWithTag("MainCamera");
        groundCheck = GameObject.FindGameObjectWithTag("GroundCheck");
        spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint");

        characterController.enabled = false;
        characterController.transform.position = spawnPoint.transform.position;
        characterController.enabled = true;


        joystick = FindObjectOfType<Joystick>();
        joyButton = FindObjectOfType<JoyButton>();

        jacketsScript = FindObjectOfType<JacketsScript>();
        playerJacket = GameObject.FindGameObjectWithTag("PlayerJacket");
        if (playerJacket != null && jacketsScript != null)
        {
            for (int i = 0; i < jacketsScript.jackets.Length; i++)
            {
                if (PlayerPrefsManager.instance.GetCurrentJacket() == jacketsScript.jackets[i].nameJacket)
                {
                    playerJacket.GetComponent<SkinnedMeshRenderer>().material = jacketsScript.jackets[i].materialJacket;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // if change jacket
        if (isChangeJacket)
        {
            for (int i = 0; i < jacketsScript.jackets.Length; i++)
            {
                if (PlayerPrefsManager.instance.GetCurrentJacket() == jacketsScript.jackets[i].nameJacket)
                {
                    playerJacket.GetComponent<SkinnedMeshRenderer>().material = jacketsScript.jackets[i].materialJacket;
                }
            }

            isChangeJacket = false;
        }

        // if player fall
        if (characterController.transform.position.y < maxFallZone)
        {
            characterController.enabled = false;
            characterController.transform.position = spawnPoint.transform.position;
            characterController.enabled = true;
        }

        // jika player pingsan, kunci rotasi
        if (playerStats.isDying)
        {
            transform.rotation = Quaternion.Euler(0f, currentTransformY, 0f);
        }
        else
        {
            currentTransformY = GetComponent<Transform>().transform.eulerAngles.y;
            horizontal = Input.GetAxisRaw("Horizontal") + joystick.Horizontal;
            vertical = Input.GetAxisRaw("Vertical") + joystick.Vertical;
        }

        isGrounded = Physics.CheckSphere(groundCheck.transform.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0f)
        {
            velocity.y = -2f;
        }

        move = new Vector3(horizontal, 0f, vertical).normalized;

        // movement
        if (move.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            characterController.Move(moveDirection.normalized * movementSpeed * Time.deltaTime);
        }

        // bool run animator
        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        isRunning = hasHorizontalInput || hasVerticalInput;


        // jump
        if ((Input.GetKey(KeyCode.Space) || joyButton.pressed) && isGrounded && Time.time > canJump)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            canJump = Time.time + 1f;
        }

        velocity.y += gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);

        // animator
        animator.SetBool("IsRunning", isRunning);
        animator.SetBool("IsGrounded", isGrounded);

        if (isCombat == true)
        {
            animator.SetBool("IsCombat", true);
        }

        if (isCombat == false)
        {
            animator.SetBool("IsCombat", false);
        }

        if (isGetHit == true)
        {
            animator.SetBool("IsGetHit", true);
            isGetHit = false;
            // damage
            int damage = sensorPlayer.nPCStats.attack;
            playerStats.TakeDamage(damage);
        }

        if (isGetHit == false)
        {
            animator.SetBool("IsGetHit", false);
        }

        if (animator.GetBool("IsRunning") == true)
        {
            animator.SetBool("IsAttack", false);
            isAttack = false;
        }
    }

    public void Attack()
    {
        if (isAttack == false)
        {
            isAttack = true;
            animator.SetBool("IsAttack", true);
            StartCoroutine(WaitNextAttack());
        }
    }

    IEnumerator WaitNextAttack()
    {
        float duration = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(duration);
        animator.SetBool("IsAttack", false);
        isAttack = false;
    }
}
