using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

	// Use this for initialization
	private Animator anim;
	private Rigidbody2D rb2d;

	public Transform posPe;
    public Transform weaponBarrel;
    [HideInInspector] public bool isTouchingTheFloor = false;
        

	public float Velocidade;
	public float ForcaPulo = 1000f;
	[HideInInspector] public bool isPointingRight = true;

	public Image vida;
	private MensagemControle MC;

    public Weapon currentWeapon;
    [HideInInspector] public List<Weapon> inventory = new List<Weapon>();

	void Start () {
		anim = GetComponent<Animator> ();
		rb2d = GetComponent<Rigidbody2D> ();

        this.currentWeapon.setCurrentWeapon();
        inventory.Add(currentWeapon);

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
        else if (shouldAttack())
        {
            currentWeapon.Attack(this.weaponBarrel);
        }
        else if (shouldChangeWeapon())
        {
            cycleWeapons();
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

        anim.SetTrigger(getAnimation(isRunning(translationX) ? "run" : "stand"));
        

        if (shouldTurn(translationX))
        {
            Flip();
        }
    }

	public void SubtraiVida()
	{
		vida.fillAmount-=0.1f;
		if (vida.fillAmount <= 0) {
			MC.GameOver();
			Destroy(gameObject);
		}
	}

    void cycleWeapons()
    {
        int weaponIndex = this.inventory.IndexOf(this.currentWeapon) + 1;
        if (weaponIndex > (this.inventory.Count - 1))
            weaponIndex = 0;
        this.currentWeapon = this.inventory[weaponIndex];
        this.currentWeapon.setCurrentWeapon();
    }

    string getAnimation(string prefix)
    {
        switch (this.currentWeapon.id)
        {
            case WEAPON.PlasmaPistol:
                return prefix + "_gun";
            case WEAPON.RayPistol:
                return prefix + "_gun_ray";
            case WEAPON.Fists:
            default:
                return prefix;
        }
    }

    bool shouldAttack()
    {
        return this.currentWeapon != null && Input.GetKeyDown(KeyCode.LeftControl);
    }

    bool shouldJump()
    {
        return this.isTouchingTheFloor && Input.GetKeyDown(KeyCode.Space);
    }

    bool shouldChangeWeapon()
    {
        return this.inventory.Count > 1 && Input.GetKeyDown(KeyCode.LeftShift);
    }

    bool isRunning(float translationX)
    {
        return translationX != 0 && this.isTouchingTheFloor;
    }

    bool shouldTurn(float translationX)
    {
        return ((translationX > 0 && !this.isPointingRight) || (translationX < 0 && this.isPointingRight));
    }
}
