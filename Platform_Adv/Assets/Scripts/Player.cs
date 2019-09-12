using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {

    //Config
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(25f, 25f);

    //State
    bool isAlive = true;
    bool doubleJump;

    // Cached componet references
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeet;
    float gravitySacleAtStart;

	// Start
	void Start () {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeet = GetComponent<BoxCollider2D>();
        gravitySacleAtStart = myRigidBody.gravityScale;
	}
	
	// Update
	void Update () {
        if (!isAlive) { return; }

        Run();
        Jump();
        ClimbLadder();
        FlipSprite();
        Die();
	}

    private void Run (){
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal"); // value is between -1 to 1
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("Running", playerHasHorizontalSpeed);
    }

    private void ClimbLadder(){
        if(!myFeet.IsTouchingLayers(LayerMask.GetMask("Climbing"))){
            myAnimator.SetBool("Climbing", false);
            myRigidBody.gravityScale = gravitySacleAtStart;
            return;
        }

        float controlThrow = CrossPlatformInputManager.GetAxis("Vertical");
        Vector2 climbVelocity = new Vector2(myRigidBody.velocity.x, controlThrow * climbSpeed);
        myRigidBody.velocity = climbVelocity;

        myRigidBody.gravityScale = 0f;

        bool playerHasVerticalSpeed = Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("Climbing", playerHasVerticalSpeed);

    }

    // Jump & DoubleJump
    private void Jump (){
        if(!myFeet.IsTouchingLayers(LayerMask.GetMask("Ground")) && !doubleJump){
            return;
        }

        if (myFeet.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            doubleJump = true;
        }

        if(CrossPlatformInputManager.GetButtonDown("Jump") && doubleJump)
        {
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, 0);
            myRigidBody.velocity += jumpVelocityToAdd;

            doubleJump = false;
        }
    }

    // Player moving with platform
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "MovingPlatform")
        {
            transform.parent = other.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.transform.tag == "MovingPlatform")
        {
            transform.parent = null;
        }
    }

    // Player Die
    private void Die (){
        if(myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy")))
        {
            isAlive = false;
            myAnimator.SetBool("Dying", true);
            GetComponent<Rigidbody2D>().velocity = deathKick; 
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }

        if(myFeet.IsTouchingLayers(LayerMask.GetMask("Hazard")))
        {
            isAlive = false;
            myAnimator.SetBool("Dying", true);
            GetComponent<Rigidbody2D>().velocity = deathKick;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }

    public void Respawn(){
        myAnimator.SetBool("Dying", false);
        isAlive = true;
    }

    private void FlipSprite(){
        // change the sprite of player when it's moving horizontally
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        if(playerHasHorizontalSpeed){
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);
        }
    }

}
