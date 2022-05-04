using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEngine : MonoBehaviour
{
	[SerializeField]
	private float speed;
	[SerializeField]
	private float withoutStaminaSpeed;
	[SerializeField]
	private float crouchSpeed;
	[SerializeField]
	private float runSpeed;
	[SerializeField]
	private float jumpForce;
	[SerializeField]
	private float crouchJumpForce;
	[SerializeField]
	private float withoutStaminaJumpForce;
	[SerializeField]
	private float sensitivity;
	[SerializeField]
	private float gravity;
	[SerializeField]
	private GameObject cam;
	[SerializeField]
	private float maxCamVerRot;
	[SerializeField]
	private GameObject[] CameraPositions;

	private float moveFB; //Move Front/Back
	private float moveLR; //Move Left/Right
	private float rotHor; //Rotattion Horizontal
	private float rotVer; //Rotation Vertical

	private float directionY;

	private bool crouched;
	private bool jump;
	private bool run;

	public static float activeSpeed;
	private float activeJumpForce;

	private InputSystemKeyboard _inputSystem;
	private Rigidbody _rb;

	//Debug
	private MeshRenderer _renderer;
	public Material matGrounded;
	public Material matNotGround;

	private void Awake()
	{
		_inputSystem = GetComponent<InputSystemKeyboard>();
		_rb = GetComponent<Rigidbody>();

		_renderer = GetComponent<MeshRenderer>();
	}

	private void OnEnable()
	{
		_inputSystem.OnJump += SetJump;
		_inputSystem.OnCrouch += SetCrouch;
		_inputSystem.OnRun += SetRun;
	}

	private void OnDisable()
	{
		_inputSystem.OnJump -= SetJump;
		_inputSystem.OnCrouch -= SetCrouch;
		_inputSystem.OnRun -= SetRun;
	}

	private void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		jump = false;
		crouched = false;

		SetNormalSpeeds();
	}

	void FixedUpdate()
	{
		moveFB = _inputSystem.axHor * activeSpeed;
		moveLR = _inputSystem.axVer * activeSpeed;

		rotHor = _inputSystem.moHor * sensitivity;
		rotVer = _inputSystem.moVer * sensitivity;

		Vector3 movement = new Vector3(moveFB, 0, moveLR);

		if (jump)
		{
			directionY = activeJumpForce;

			jump = false;
		}

		if (!GroundCheckerManager.isGrounded)
		{
			directionY -= gravity * Time.fixedDeltaTime;
		}

		movement.y = directionY;

		if (StaminaManager.currentStamina <= 0)
		{
			activeSpeed = withoutStaminaSpeed;
			activeJumpForce = withoutStaminaJumpForce;
		}

		movement = transform.rotation * movement;
		_rb.velocity = new Vector3(movement.x, movement.y, movement.z);

		CameraRotation(cam, rotHor, rotVer);


		//Debug
		//GroundDetector();
	}

	void CameraRotation(GameObject cam, float rotHor, float rotVer)
	{
		transform.Rotate(0, rotHor * Time.fixedDeltaTime, 0);
		cam.transform.Rotate(-rotVer * Time.fixedDeltaTime, 0, 0);

		if (Mathf.Abs(cam.transform.localRotation.x) > maxCamVerRot)
		{
			float clamped = maxCamVerRot * Mathf.Sign(cam.transform.localRotation.x);

			Quaternion adjustedRotation = new Quaternion(clamped, cam.transform.localRotation.y, cam.transform.localRotation.z, cam.transform.localRotation.w);
			cam.transform.localRotation = adjustedRotation;
		}
	}

	void SetCrouch()
	{
		if (GroundCheckerManager.isGrounded)
		{
			if (!crouched)
			{
				cam.transform.position = CameraPositions[1].transform.position;

				activeSpeed = crouchSpeed;
				activeJumpForce = crouchJumpForce;

				crouched = true;
			}
			else
			{
				cam.transform.position = CameraPositions[0].transform.position;

				SetNormalSpeeds();

				crouched = false;
			}
		}
	}

	void SetJump()
	{
		if (GroundCheckerManager.isGrounded)
		{
			jump = true;
		}
	}

	void SetRun(bool running)
	{
		if (!crouched && GroundCheckerManager.isGrounded && StaminaManager.currentStamina >= 0)
		{
			run = running;

			if (running)
			{
				activeSpeed = runSpeed;
			}
			else
			{
				SetNormalSpeeds();
			}
		}
	}

	void SetNormalSpeeds()
	{
		activeSpeed = speed;
		activeJumpForce = jumpForce;
	}

	/*void GroundDetector()
	{
		if (GroundCheckerManager.isGrounded)
		{
			_renderer.material = matGrounded;
		}
		else
		{
			_renderer.material = matNotGround;
		}
	}*/

	public bool ReturnCrouch()
    {
		return crouched;
    }

	public bool ReturnJump() 
	{
		return jump;
	}

	public bool ReturnRun()
    {
		return run;
    }
}