using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEngine : MonoBehaviour 
{
	[Header("Character Speed")]
	[SerializeField]
	private float speed;
	[SerializeField]
	private float withoutStaminaSpeed;
	[SerializeField]
	private float crouchSpeed;
	[SerializeField]
	private float runSpeed;

	[Header("Character Jump")]
	[SerializeField]
	private float jumpForce;
	[SerializeField]
	private float crouchJumpForce;
	[SerializeField]
	private float withoutStaminaJumpForce;

	[Header("Mouse Sensitivity")]
	[SerializeField]
	private float sensitivity;
	[SerializeField]
	private float gravity;

    public GameObject cam;


	private float moveFB; //Move Front/Back
	private float moveLR; //Move Left/Right
	private float rotHor; //Rotattion Horizontal
	private float rotVer; //Rotation Vertical

	private float directionY;

	private bool crouched;
	private bool jump;

	[Header("RealTime Amount")]
	public float activeSpeed;
	public float activeJumpForce;

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
		_rb.velocity= new Vector3(movement.x, movement.y, movement.z);

		CameraRotation(cam, rotHor, rotVer);


		//Debug
		GroundDetector();
	}

	void CameraRotation(GameObject cam, float rotHor, float rotVer)
	{
		transform.Rotate (0, rotHor * Time.fixedDeltaTime, 0);
		cam.transform.Rotate (-rotVer * Time.fixedDeltaTime, 0, 0);

		if (Mathf.Abs (cam.transform.localRotation.x) > 0.7) 
		{
			float clamped = 0.7f * Mathf.Sign(cam.transform.localRotation.x); 

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
				cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y - 0.5f, cam.transform.position.z);

				activeSpeed = crouchSpeed;
				activeJumpForce = crouchJumpForce;

				crouched = true;
			}
			else
			{
				cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y + 0.5f, cam.transform.position.z);

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

	void SetRun(bool run)
    {
		if (!crouched && GroundCheckerManager.isGrounded && StaminaManager.currentStamina >= 0)
		{
			if (run)
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

	void GroundDetector()
	{
		if (GroundCheckerManager.isGrounded)
		{
			_renderer.material = matGrounded;
		}
		else
		{
			_renderer.material = matNotGround;
		}
	}
}