using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TP_Motor : MonoBehaviour
{

  public static TP_Motor Instance;

  public bool InfiniteJumps = false;
  private bool currentlyDashing = false;

  public float dashBar = 100;
  public float dashRechargeRate = 0.5f;
  public float ForwardSpeed = 10f;
  public float BackwardSpeed = 2f;
  public float StrafingSpeed = 5f;
  public float SlideSpeed = 10f;
  public float JumpSpeed = 6f;
  public float Gravity = 31f;
  public float TerminalVelocity = 20f;
  public float SlideThreshold = 0.6f;
  public float MaxControllableSlideMagnitude = 0.4f;

  public int numOfJumps;
  public int baseNumOfJumps;

  private Vector3 slideDirection;

  public Vector3 MoveVector { get; set; }
  public float VerticalVelocity { get; set; }

  void Awake()
  {
    Instance = this;
  }

  public void UpdateMotor()
  {
    if (!currentlyDashing)
      rechargeDashBar();

    SnapAllignCharacterWithCamera();
    ProcessMotion();

    if (TP_Controller.CharacterController.isGrounded)
      numOfJumps = baseNumOfJumps;
  }

  void ProcessMotion()
  {
    //Transform movevector into worldspace relative to our character;s rotation
    MoveVector = transform.TransformDirection(MoveVector);

    //then normalize our movevector if our magnitude is greater than 0, fixes diagonals
    if (MoveVector.magnitude > 1)
      MoveVector = Vector3.Normalize(MoveVector);

    //Apply sliding important placement
    //ApplySlide();

    //Multiply movevector by movespeed
    MoveVector *= MoveSpeed();// now a method, transmission

    //multiply  movevector by delta.time for value per second than per frame
    //MoveVector *= Time.deltaTime;//moved down, was for clarification

    //Reapply Vertical Velocity.y
    MoveVector = new Vector3(MoveVector.x, VerticalVelocity, MoveVector.z);

    //Apply gravity
    ApplyGravity();

    //move the character in worldspace
    TP_Controller.CharacterController.Move(MoveVector * Time.deltaTime);
  }

  void ApplyGravity()
  {
    if (MoveVector.y > -TerminalVelocity)
      MoveVector = new Vector3(MoveVector.x, MoveVector.y - Gravity * Time.deltaTime, MoveVector.z);

    if (TP_Controller.CharacterController.isGrounded && MoveVector.y < -1)
      MoveVector = new Vector3(MoveVector.x, -1, MoveVector.z);
  }

  void ApplySlide()
  {
    if (!TP_Controller.CharacterController.isGrounded)//if not grounded do nothing
      return;

    slideDirection = Vector3.zero;

    RaycastHit hitInfo;

    //cast from 0,1,0 to 0,-1,0 and put out into hitInfo
    if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out hitInfo))
    {
      Debug.DrawLine(transform.position + Vector3.up, Vector3.down);

      if (hitInfo.normal.y < SlideThreshold)
        slideDirection = new Vector3(hitInfo.normal.x, -hitInfo.normal.y, hitInfo.normal.z);
    }

    if (slideDirection.magnitude < MaxControllableSlideMagnitude)
    {
      MoveVector += slideDirection;
    }
    else
    {
      MoveVector = slideDirection;
    }

  }

  public void Jump()
  {
    if (numOfJumps > 0)
    {
      if (InfiniteJumps == false)
        numOfJumps--;

      VerticalVelocity = JumpSpeed;
    }
  }

  void SnapAllignCharacterWithCamera()
  {
    if (MoveVector.x != 0 || MoveVector.z != 0)
    {
      transform.rotation = Quaternion.Euler(transform.eulerAngles.x,
    Camera.main.transform.eulerAngles.y,
    transform.eulerAngles.z);
    }
  }

  float MoveSpeed()
  {
    var moveSpeed = 0f;//local var, need f

    switch (TP_Animator.Instance.MoveDirection)
    {
      case TP_Animator.Direction.Stationary:
        //moveSpeed /= 1.09f;
        moveSpeed = 0;
        currentlyDashing = false;
        break;

      case TP_Animator.Direction.Forward:

        //if (Input.GetKey(KeyCode.LeftShift) || (Input.GetButton("Dash"))) // For XBOX controller
        if (Input.GetKey(KeyCode.LeftShift))
        {
          moveSpeed = doDash(ForwardSpeed);
        }
        else
        {
          moveSpeed = ForwardSpeed;
          currentlyDashing = false;
        }
        break;

      case TP_Animator.Direction.Backward:

        if (Input.GetKey(KeyCode.LeftShift))
        {
          moveSpeed = doDash(BackwardSpeed);
        }
        else
        {
          moveSpeed = BackwardSpeed;
          currentlyDashing = false;
        }

        break;

      case TP_Animator.Direction.Left:

        if (Input.GetKey(KeyCode.LeftShift))
        {
          moveSpeed = doDash(StrafingSpeed);
        }
        else
        {
          moveSpeed = StrafingSpeed;
          currentlyDashing = false;
        }

        break;

      case TP_Animator.Direction.Right:

        if (Input.GetKey(KeyCode.LeftShift))
        {
          moveSpeed = doDash(StrafingSpeed);
        }
        else
        {
          moveSpeed = StrafingSpeed;
          currentlyDashing = false;
        }

        break;

      case TP_Animator.Direction.LeftForward:

        if (Input.GetKey(KeyCode.LeftShift))
        {
          moveSpeed = doDash(ForwardSpeed);
        }
        else
        {
          moveSpeed = ForwardSpeed;
          currentlyDashing = false;
        }
        break;

      case TP_Animator.Direction.RightForward:

        if (Input.GetKey(KeyCode.LeftShift))
        {
          moveSpeed = doDash(ForwardSpeed);
        }
        else
        {
          moveSpeed = ForwardSpeed;
          currentlyDashing = false;
        }
        break;


      case TP_Animator.Direction.LeftBackward:

        if (Input.GetKey(KeyCode.LeftShift))
        {
          moveSpeed = doDash(BackwardSpeed);
        }
        else
        {
          moveSpeed = ForwardSpeed;
          currentlyDashing = false;
        }
        break;

      case TP_Animator.Direction.RightBackward:

        if (Input.GetKey(KeyCode.LeftShift))
        {
          moveSpeed = doDash(BackwardSpeed);
        }
        else
        {
          moveSpeed = ForwardSpeed;
          currentlyDashing = false;
        }
        break;

    }

    if (slideDirection.magnitude > 0)
      moveSpeed = SlideSpeed;

    return moveSpeed;

  }

  void rechargeDashBar()
  {
    if (dashBar <= 100)
    {
      dashBar += dashRechargeRate;//when not dashing
    }
  }

  public float doDash(float speedToIncrease)
  {
    if (dashBar > 0)
    {
      currentlyDashing = true;
      speedToIncrease *= 3;
      dashBar--;
      return speedToIncrease;
    }
    else
    {
      return speedToIncrease;
    }
  }

}//End of class