using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float jumpSpeed = 7f;
    public float desiredJumpHeight = 3f;
    public float gravity = -9.81f;
    public float slideThreshold = 30f;

    private CharacterController controller;
    private Animator animator;
    private Vector3 playerVelocity;
    private bool isGrounded;
    private bool allowGroundCheck = true;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (allowGroundCheck)
        {
            isGrounded = controller.isGrounded;
        }

        animator.SetBool("IsGrounded", isGrounded);

        if (isGrounded && playerVelocity.y < 0.1f)
        {
            playerVelocity.y = 0f;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            float jumpSpeedCalc = Mathf.Sqrt(2 * Mathf.Abs(gravity) * desiredJumpHeight);
            playerVelocity.y += jumpSpeedCalc;
            animator.SetTrigger("Jump");
            isGrounded = false;
            StopAllCoroutines();
            StartCoroutine(GroundCheckDelay(0.5f));
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            animator.SetBool("Sit", true);
        }

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) ||
             Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D)))
        {
            animator.SetBool("Sit", false);
        }

        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        IEnumerator GroundCheckDelay(float delay)
        {
            allowGroundCheck = false;
            yield return new WaitForSeconds(delay);
            // 在重新允许地面检测时，重新检查地面状态
            isGrounded = controller.isGrounded;
            allowGroundCheck = true;
        }

        // 检测WASD按键状态以决定是否触发走路或跑步动画
        bool wPressed = Input.GetKey(KeyCode.W);
        bool sPressed = Input.GetKey(KeyCode.S);
        bool aPressed = Input.GetKey(KeyCode.A);
        bool dPressed = Input.GetKey(KeyCode.D);
        bool shiftPressed = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        // 检测是否有方向键被按下，且相对方向键没有被按下
        bool isWalking = (wPressed && !sPressed) || (sPressed && !wPressed) ||
                        (aPressed && !dPressed) || (dPressed && !aPressed);

        // 设置走路和跑步状态
        animator.SetBool("IsWalking", isWalking && !shiftPressed);
        animator.SetBool("IsRunning", isWalking && shiftPressed);
    }
}