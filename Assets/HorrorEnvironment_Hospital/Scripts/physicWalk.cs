using UnityEngine;
using System.Collections;

public class physicWalk : MonoBehaviour {
	
	public static physicWalk instance;

	public GameObject myCamera;
	
	//
	public float speed  = 7f;
	public float maxSprintSpeed = 7f;
	public float maxWalkSpeed = 4f;
	public float force     = 8f;
	public float jumpSpeed = 5f;
	 
	public float footsFrequency = 0.7f;

	//
	public bool grounded = true;
	
	private float fallingForce = 0f;
	
	private CapsuleCollider collider;
	
	private bool canJump = true;
	private float canJumpCounter = 0f;

	public AudioClip footstepSound;
	public AudioClip fallSound;

	private float sprintTime = 0.0f;
	private float exhaustTime = 3.0f;

	void Start()
	{
		instance = this;
		
		collider = gameObject.GetComponent< CapsuleCollider >();
	}
	
	// Don't let the Physics Engine rotate this physics object so it doesn't fall over when running
	void Awake ()
	{ 
		GetComponent<Rigidbody>().freezeRotation = true;
		
		speed = maxWalkSpeed;
	}
	
	public virtual float jump
	{
		get 
		{
			if( Input.GetButton( "Jump" ) ) return 1f;
				else return 0f;
		}
	}
	 
	public virtual float horizontal
	{
		get
		{
			float v = Input.GetAxis( "Horizontal" );
			return v * force;
		} 
	} 
	public virtual float vertical
	{
		get
		{
			float v = Input.GetAxis( "Vertical" );
			return v * force;
		} 
	}

	float fr = 0f;
	void Update()
	{
		if( GetComponent<Rigidbody>().velocity.magnitude > 0f && grounded )
		{
			fr += Time.deltaTime;

			if( Input.GetButton( "Sprint" ) )
			{
				fr += Time.deltaTime*0.5f;
			}

			while( fr >= footsFrequency )
			{
				fr = 0f;

				playFootstepSound();
			}
		}

		if( GetComponent<Rigidbody>().IsSleeping() == true ) GetComponent<Rigidbody>().WakeUp();
		
		if( Input.GetButton( "Sprint" ) )
		{
			speed = maxSprintSpeed;
			sprintTime += Time.deltaTime;
			if (sprintTime > 3.0) {
				speed = maxWalkSpeed;
			}
		}
		else {
			speed = maxWalkSpeed;
			
			exhaustTime -= Time.deltaTime;
			if (exhaustTime < 0.0) {
				sprintTime = 0.0f;
				exhaustTime = 3.0f;
			}
		}
	}

	public void playFootstepSound()
	{
		GetComponent<AudioSource>().PlayOneShot( footstepSound );
	}

	void FixedUpdate ()
	{
		///Jump iteration
		if( !canJump )
		{
			canJumpCounter += Time.fixedDeltaTime;
			if( canJumpCounter >= 1f )
			{
				canJump = true;
				canJumpCounter = 0f;
			}
		}
		
		////Ground test
		RaycastHit hit;
		
		Vector3 tmpV = transform.position;
		tmpV.y += 0.1f;
    	if (Physics.Raycast( tmpV, -Vector3.up, out hit, transform.position.y *0.5f + 0.3f))
		{
        	if( hit.collider.tag == "GROUND" )
			{
				grounded = true;
			}
			else
			{
				grounded = false;
			}
		}
		else
		{
			grounded = false;
		}
		
		////
	 
	 	if( horizontal != 0f || vertical != 0f || jump != 0f || !grounded ) {
			GetComponent<Rigidbody>().drag = 0.5f;
			GetComponentInChildren<Animator>().SetBool("walking", true);
			Vector3 camPos = myCamera.transform.localPosition;
			myCamera.transform.localPosition = Vector3.Lerp(camPos, new Vector3(0.053f, 0.759f, 0.422f), 5 * Time.deltaTime);
			Vector3 oriCoidPos = GetComponent<CapsuleCollider>().center;
			GetComponent<CapsuleCollider>().center = new Vector3(oriCoidPos.x, oriCoidPos.y, 0.422f);
			
			
		} else{
			GetComponent<Rigidbody>().drag = 100f;
			GetComponentInChildren<Animator>().SetBool("walking", false);
			Vector3 camPos = myCamera.transform.localPosition;
			myCamera.transform.localPosition = Vector3.Lerp(camPos, new Vector3(0.053f, 1.247f, 0.181f), 5 * Time.deltaTime);
			Vector3 oriCoidPos = GetComponent<CapsuleCollider>().center;
			// GetComponent<CapsuleCollider>().center = new Vector3(oriCoidPos.x, oriCoidPos.y, 0.181f);
		}
		
		Rigidbody rb = GetComponent<Rigidbody>();

		if( GetComponent<Rigidbody>().velocity.magnitude <= speed && grounded == true )
		{
			Vector3 forceV = Vector3.Cross( hit.normal, Vector3.Cross( transform.forward, hit.normal ));
			forceV = forceV.normalized;
			
			if( vertical != 0f && horizontal != 0f ) {
				GetComponent<Rigidbody>().AddForce( ( forceV * vertical ) + ( transform.right * horizontal ), ForceMode.Force);
			}
			else GetComponent<Rigidbody>().AddForce(( forceV * vertical ) + ( transform.right * horizontal ));
		}
	 
		if( jump != 0f && grounded && canJump )
		{
			canJump = false;
			Vector3 tmp = Vector3.up * jumpSpeed + ( transform.forward * vertical * 0.1f );
			GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity + tmp;
		}
			
		if( !grounded )
		{

			fallingForce = fallingForce + Time.fixedDeltaTime * 5f;
			GetComponent<Rigidbody>().AddForce( -Vector3.up * 10f * fallingForce );
		}
		else
		{

			fallingForce -= (Time.fixedDeltaTime * 10f) + (fallingForce * 0.3f);
			if( fallingForce < 0f ) fallingForce = 0f;
		}

	 }

	void OnCollisionEnter ( Collision other )
	{
		if( other.collider.tag == "GROUND" )
		{
			if( other.relativeVelocity.y >= 2f )
			{
				physicWalk_MouseLook.instance.wobble( 0f, other.relativeVelocity.y * 2f, 0f, other.relativeVelocity.y * 2f );
				
				GetComponent<AudioSource>().PlayOneShot( fallSound );
				
				Vector3 tmpPosMod = Camera.main.transform.position;
				tmpPosMod.y -= other.relativeVelocity.y * 0.15f;
				physicWalk_MouseLook.instance._camPos.position = tmpPosMod;
			}
		}
	}
}