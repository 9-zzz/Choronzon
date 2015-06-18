using UnityEngine;
using System.Collections;

public class TP_Controller : MonoBehaviour 
{

  public static CharacterController CharacterController;
  public static TP_Controller Instance;
  // Use this for initialization
  void Awake() 
  {
    CharacterController = GetComponent("CharacterController") as CharacterController;
    Instance = this;
  }

  // Update is called once per frame
  void Update() 
  {
    if (Camera.main == null)
      return;

    GetMovementInput();
    HandleActionInput();//called in every update as long as camera exists

    TP_Motor.Instance.UpdateMotor();//after set vector
  }


  void GetMovementInput()
  {
    var deadZone = 0.001f;

    TP_Motor.Instance.VerticalVelocity = TP_Motor.Instance.MoveVector.y;
    TP_Motor.Instance.MoveVector = Vector3.zero;

    if (Input.GetAxis("Vertical") > deadZone || Input.GetAxis("Vertical") < -deadZone)
      TP_Motor.Instance.MoveVector += new Vector3(0, 0, Input.GetAxis("Vertical"));

    if (Input.GetAxis("Horizontal") > deadZone || Input.GetAxis("Horizontal") < -deadZone)
      TP_Motor.Instance.MoveVector += new Vector3(Input.GetAxis("Horizontal"), 0, 0);

    TP_Animator.Instance.DetermineCurrentMoveDirection();//specifically at the end of this function since we have movevector
  }

  void HandleActionInput()
  {

    if (Input.GetButtonDown("Jump"))
    //if (Input.GetButtonDown("Jump"))
    {

      Jump();

    }

  }

  void Jump()//opening up a lot of possibilities to what to do , anything linked to jump
  {

    //or sound effect, everytime jump happens . do something here
    TP_Motor.Instance.Jump();//tp controller is the brain. need this for animation and stuff

  }

}
