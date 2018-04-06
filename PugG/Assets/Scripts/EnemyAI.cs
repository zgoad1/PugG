using System.Collections;
using UnityStandardAssets._2D;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

	private float speed;									// Horizontal movement speed
	[SerializeField] private float walkSpeed = 0.02f;
	[SerializeField] private float runSpeed = 0.03f;
	[SerializeField] private int damage = 20;               // HP to take from player upon hit
	[SerializeField] private EnemyAIGroundCheck GroundCheckLeft;	// Left ground check, determines whether we can go left
	[SerializeField] private EnemyAIGroundCheck GroundCheckRight;	// same but right
	private float waitTime;                                 // How long to wait until the next move
	private float wanderTime;                               // How long to move
	private int direction = 1;							    // Direction of movement (-1 for left, 1 for right)
	private Vector3 movement = Vector3.zero;                // Add this to the position to determine movement
	private SpriteRenderer sprite;
	private PlatformerCharacter2D player;
	private bool chasing = false;
	private IEnumerator wait;
	private Health PlayerHP;

	public bool wandering = false;							// Controls whether the object is moving or not
	public bool canMove = false;							// True if at least one GroundCheck is hitting the ground
	public float sightRadius = 2.5f;						// Max distance the player can be without chasing them

	private void SetWaitTime() {
		waitTime = Random.Range(1f, 3f);
	}

	private void SetWanderTime() {
		wanderTime = Random.Range(1f, 3f);
	}

	private void SetDirection() {
		SetDirection((int)Mathf.Sign(Random.Range(-1, 1)));
		bool goingLeft = direction == -1 && GroundCheckLeft.onGround;
		bool goingRight = direction == 1 && GroundCheckRight.onGround;
		bool canGo = goingLeft || goingRight;
		if(!canGo) {
			SetDirection(-direction);
		}
	}

	private void SetDirection(int d) {
		direction = d;
		if(direction == -1) {
			sprite.flipX = false;
		} else {
			sprite.flipX = true;
		}
		movement.x = speed * direction;
	}

	// Check if the left (-1) or right (1) GroundCheck is on the ground
	private bool GroundCheck(int d) {
		if(d == -1) return GroundCheckLeft.onGround;
		if(d == 1) return GroundCheckRight.onGround;
		return false;
	}

	// Use this for initialization
	void Start () {
		speed = walkSpeed;
		sprite = GetComponent<SpriteRenderer>();
		player = FindObjectOfType<PlatformerCharacter2D>();
		PlayerHP = player.GetComponent<Health>();
		SetDirection();
		SetWaitTime();
		StartCoroutine("Wait", waitTime);
	}
	
	// Update is called once per frame
	void Update () {

		// Check if we can see the player
		float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
		int pDirec = (int)Mathf.Sign(player.transform.position.x - transform.position.x);
		// bool canSee: The player is close enough, we won't fall into a pit, and we're facing them or already chasing them
		bool canSee = Mathf.Abs(distanceToPlayer) < sightRadius && GroundCheck(pDirec) && (direction > 0 && pDirec > 0 || direction < 0 && pDirec < 0 || pDirec == 0 || chasing);
		if(canSee) {
			// If we can, start chasing them
			wandering = false;
			StopCoroutine("Wait");
			chasing = true;
			speed = runSpeed;
		} else if(chasing) {
			// Else go back to wandering
			chasing = false;
			speed = walkSpeed;
			SetWaitTime();
			StartCoroutine("Wait", waitTime);
		}

		if(wandering) {
			//Debug.Log("AI: Moving " + movement.x);
			transform.position += movement;
		} else if(chasing) {
			// follow player
			SetDirection(pDirec);
			// Don't pass up the player or else the sprite will turn to the other direction every frame
			movement.x = pDirec * Mathf.Min(Mathf.Abs(movement.x), Mathf.Abs(player.transform.position.x - transform.position.x));
			transform.position += movement;
		}
	}

	// Wait some amount of time, then change between waiting and wandering; repeat.
	public IEnumerator Wait(float time) {
		//Debug.Log("AI: Waiting " + time + " seconds");
		yield return new WaitForSeconds(time);
		if(canMove) {
			wandering = !wandering;
			if(wandering) {
				SetDirection();
				SetWanderTime();
				StartCoroutine("Wait", wanderTime);
			} else {
				SetWaitTime();
				StartCoroutine("Wait", waitTime);
			}
		} else {
			wandering = false;
			Debug.Log("AI: Can't move");
		}
	}

	// This method is called by child GroundCheck objects.
	// Detects when we're about to go off an edge.
	public void DontFall() {
		StopCoroutine("Wait");
		wandering = false;
		chasing = false;
		speed = walkSpeed;
		SetWaitTime();
		StartCoroutine("Wait", waitTime);
	}

	void OnTriggerStay2D(Collider2D collision) {
		var hit = collision.gameObject;
		PlatformerCharacter2D p = hit.gameObject.GetComponent<PlatformerCharacter2D>();
		if(p != null) {
			p.PushForce = new Vector2(20f * (hit.transform.position.x - transform.position.x), Mathf.Min(hit.transform.position.y - transform.position.y, 0.4f));
			if(PlayerHP != null) {
				PlayerHP.TakeDamage(damage);
			}
		}
	}
}
