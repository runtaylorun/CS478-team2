using System.Collections;
using UnityEngine;

public class bat : MonoBehaviour
{

    public float leftXBoundary;
    public float rightXBoundary;
    public float upperYBoundary;
    public float lowerYBoundary;
    public float horizontalMovementSpeed;
    public float verticalMovementSpeed;

    private Rigidbody2D batRigidBody;
    private Animator batAnimator;
    private bool movingLeft;
    private bool movingUp;
    private bool isColliding = false;
    private AudioManager audioManager;

    void Start()
    {
        batRigidBody = GetComponent<Rigidbody2D>();
        batAnimator = GetComponent<Animator>();
        audioManager = FindObjectOfType<AudioManager>();

        batRigidBody.velocity = new Vector2(horizontalMovementSpeed, verticalMovementSpeed);
        movingUp = verticalMovementSpeed > 0;
        movingLeft = horizontalMovementSpeed < 0;
    }

    void Update()
    {
        if((batRigidBody.transform.position.x <= leftXBoundary && movingLeft) || (batRigidBody.transform.position.x >= rightXBoundary && !movingLeft))
        {
            batRigidBody.velocity = new Vector2(horizontalMovementSpeed *= -1, verticalMovementSpeed);
            batRigidBody.transform.Rotate(0, 180, 0);
            movingLeft = !movingLeft;
        }

        if((batRigidBody.transform.position.y >= upperYBoundary && movingUp) || (batRigidBody.transform.position.y <= lowerYBoundary && !movingUp))
        {
            batRigidBody.velocity = new Vector2(horizontalMovementSpeed, verticalMovementSpeed *= -1);
            movingUp = !movingUp;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Arrow")
        {
            if(!isColliding)
            {
                audioManager.Play("enemyDeath");
                isColliding = true;
                GetComponent<BoxCollider2D>().enabled = false;
                Camera.main.BroadcastMessage("ApplyScore", 50);
                StartCoroutine(SetDestroyTimer());
            }
        }
    }

    private IEnumerator SetDestroyTimer()
    {
        batAnimator.SetTrigger("hit");
        batRigidBody.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(0.25f);

        Destroy(gameObject);
    }
}
