using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

	//movement & turning
	public float speed = 6f;	
	public float camRaylengty = 100f;
	Rigidbody playerRigidBody;
	Vector3 moveMent ;
	Animator anim;
	int floorMask;

	void Awake() {	//不管脚本是否激活，就能够在游戏之初进行调用
		anim = GetComponent<Animator> ();
		playerRigidBody = GetComponent<Rigidbody> ();
		floorMask = LayerMask.GetMask ("Floor");
//		Debug.LogError("给点反应老大");
	}

	void FixedUpdate() {
		float h = Input.GetAxisRaw("Horizontal");
		float v = Input.GetAxisRaw("Vertical");		//return a value of 0, -1, 1
		Move(h,v);
		Turn ();
		Animator(h,v);

	}

	void Move(float h, float v) {
		moveMent.Set (h, 0, v);	//y keep 0 while moving
		moveMent = moveMent.normalized * speed * Time.deltaTime;
		playerRigidBody.MovePosition(transform.position + moveMent);		//we want a stable speed value, so Normalize it 
	}

	void Turn() {
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit floorHit;	// point on floor
		if(Physics.Raycast(camRay, out floorHit, camRaylengty, floorMask)){	//out ref
			Vector3 playertoMouse = floorHit.point - transform.position;
			playertoMouse.y = 0f;
			Quaternion newrotation = Quaternion.LookRotation (playertoMouse);
			playerRigidBody.MoveRotation (newrotation);

		}
	}

	void Animator(float h, float v){
		bool isWalking = (h != 0f || v != 0f);

		anim.SetBool ("IsWalking", isWalking);

//		if (Input.GetKeyDown(KeyCode.A)) {
//			GetComponent<AudioSource> ().Play();
//			Debug.Log ("播放");
//		}
//
//		if (Input.GetKeyDown(KeyCode.P)) {
//			GetComponent<AudioSource> ().loop = false;
//			Debug.Log ("--------");
//		}

	}

}
