using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	// Use this for initialization
	private Animator anim;
	private Rigidbody2D rb2d;

	public Transform posPe;
	[HideInInspector] public bool isTouchingTheFloor = false;


	public float Velocidade;
	public float ForcaPulo = 1000f;
	[HideInInspector] public bool isPointingRight = true;

	public Image vida;
	private MensagemControle MC;
    private bool haveAGun = true;

	void Start () {
		anim = GetComponent<Animator> ();
		rb2d = GetComponent<Rigidbody2D> ();

		GameObject mensagemControleObject = GameObject.FindWithTag("MensagemControle");
		if (mensagemControleObject != null) {
			MC = mensagemControleObject.GetComponent<MensagemControle> ();
		}
	}
	
	// Update is called once per frame
	void Update () {
        isTouchingTheFloor = Physics2D.Linecast(transform.position, posPe.position, LayerMask.NameToLayer("chao"));

        if (shouldJump())
        {
            Jump();
        }
	}

	void FixedUpdate()
	{
        float translationY = 0;
        float translationX = Input.GetAxis("Horizontal") * Velocidade;

        trackRunningBehaviour(translationX, translationY);
	}
	void Flip()
	{
        isPointingRight = !isPointingRight;

        transform.Rotate(0f, 180f, 0f);
	}

    void Jump()
    {
        anim.SetTrigger("jump");
        rb2d.AddForce(transform.up * ForcaPulo);
    }

    float Move(float translationX)
    {
        return translationX * (isPointingRight ? 1 : -1);
    }

    void trackRunningBehaviour(float translationX, float translationY)
    {
        transform.Translate(Move(translationX), translationY, 0);

        if (isRunning(translationX))
        {
            anim.SetTrigger(this.haveAGun ? "run_gun" : "run");
        }
        else
        {
            anim.SetTrigger(this.haveAGun ? "stand_gun" : "stand");
        }

        if (shouldTurn(translationX))
        {
            Flip();
        }
    }

    bool shouldJump()
    {
        return this.isTouchingTheFloor && Input.GetKeyDown("space");
    }

    bool isRunning(float translationX)
    {
        return translationX != 0 && this.isTouchingTheFloor;
    }

    bool shouldTurn(float translationX)
    {
        return ((translationX > 0 && !this.isPointingRight) || (translationX < 0 && this.isPointingRight));
    }

	public void SubtraiVida()
	{
		vida.fillAmount-=0.1f;
		if (vida.fillAmount <= 0) {
			MC.GameOver();
			Destroy(gameObject);
		}
	}
	
}
