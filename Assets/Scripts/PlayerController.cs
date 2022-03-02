using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; set; }

    #region Camera Data
    Transform cameraMainTransform;
    #endregion
    //reference to movement actions
    //visible outside class - not protected
    #region Input Controllers
    [SerializeField] InputActionReference moveControl;
    [SerializeField] InputActionReference jumpControl;
    #endregion
    //c# perfers doubles - unity perfers floats
    //private
    #region Movement Data
    [SerializeField] float moveSpeed = 10.0f;
    Vector3 moveDirection = Vector3.zero;
    [SerializeField] float jumpHeight = 3.0f;
    Vector3 jumpVelocity = Vector3.zero;
    [SerializeField] float gravityScale = 5.75f;
    [SerializeField] float rotateSpeed = 5.0f;
    #endregion

    #region Player Data
    CharacterController myController;
    [SerializeField] Transform myModelTransform;
    Animator myAnimator;
    #endregion

    #region Ground Checking
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance = 4.0f;
    public LayerMask groundMask;
    public bool isGrounded = true; 
    #endregion

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        cameraMainTransform = Camera.main.transform;
        myController = GetComponent<CharacterController>();
        myModelTransform = GetComponentInChildren<Transform>();
        myAnimator = GetComponentInChildren<Animator>();
        isGrounded = true; 

    }

    // Update is called once per frame
    void Update()
    {
        CheckGround(); 
        Debug.Log(Time.deltaTime);
        Move();
        Jump();
        Rotate();
    }

    void CheckGround()
    {
        isGrounded = Physics.CheckSphere(
            groundCheck.position,
            groundDistance,
            groundMask
            );
        //isGrounded = transform.position.y < 0.05f;
        //if (gameObject.tag == "ground")
        //{
        //    isGrounded = true;
        //}

  
        myAnimator.SetBool("Grounded", isGrounded);
    }

    //private void OnCollisionEnter(Collision col)
    //{
    //    Debug.Log("collision");
    //    if (col.gameObject.CompareTag("Ground"))
    //    {
    //        Debug.Log("you hit " + "ground");
    //    }
    //}



    void Move()
    {
        //left and right
        moveDirection.x = moveControl.action.ReadValue<Vector2>().x;
        moveDirection.y = 0.0f;
        //forward backward
        moveDirection.z = moveControl.action.ReadValue<Vector2>().y;

        moveDirection = moveDirection.x * transform.right + moveDirection.z * transform.forward;
        moveDirection.Normalize();
        //move using transform
        //transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        myController.Move(moveDirection * moveSpeed * Time.deltaTime);
        float speed = Mathf.Abs(moveDirection.x) + Mathf.Abs(moveDirection.z); 
        myAnimator.SetFloat("Speed", speed);
    }

    void Jump()
    {
        if (jumpControl.action.triggered && isGrounded)
        {
            jumpVelocity.y = Mathf.Sqrt(-3.0f * jumpHeight * gravityScale * Physics.gravity.y);
        }
        else
        {
            if (isGrounded)
            {
                jumpVelocity = Vector3.zero;
            }
            else
            {
                jumpVelocity.y += gravityScale * Physics.gravity.y * Time.deltaTime;
            }
        }

       
        //transform.Translate(jumpVelocity * Time.deltaTime);

        myController.Move(jumpVelocity * Time.deltaTime);
        myAnimator.SetBool("Grounded", isGrounded);
    }

    void Rotate()
    {
        if (!(moveDirection.x.Equals(0.0f) && moveDirection.x.Equals(0.0f)))
        {
            transform.rotation = Quaternion.Euler(0.0f, cameraMainTransform.eulerAngles.y, 0.0f);
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0.0f, moveDirection.z));
            myModelTransform.rotation = Quaternion.Slerp(myModelTransform.rotation, newRotation, rotateSpeed * Time.deltaTime);
        }
    }

    void OnEnable()
    {
        moveControl.action.Enable();
        jumpControl.action.Enable();
    }

    void OnDisable()
    {
        moveControl.action.Disable();
        jumpControl.action.Disable();
    }

}