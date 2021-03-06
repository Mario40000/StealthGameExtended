using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    public SimpleHealthBar healthBar;
    public GameObject damagePopUp;
    public Material playerColor;
    public float colorTime;

    private VibrateController controller = new VibrateController();

    private Transform player;

    // How much health the player has left.
    const  float maxHealth = 300f;
    public float health = 100f;	
	// How much time from the player dying to the level reseting.
	public float resetAfterDeathTime = 5f;
    // A bool to show if the player is dead or not.
    
    [HideInInspector]
    public bool playerDead;		

	// Reference to the animator component.
	Animator anim;			
	// Reference to the HashIDs.
	HashIDs hash;	
	// Reference to the SceneFadeInOut script.
	SceneFadeInOut sceneFadeInOut;	
	// Reference to the LastPlayerSighting script.
	LastPlayerSighting lastPlayerSighting;
	// A timer for counting to the reset of the level once the player is dead.
    float timer;					
	
	void Awake() {
        // Setting up the references.
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
		hash = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<HashIDs>();
		sceneFadeInOut = GameObject.FindGameObjectWithTag(Tags.fader).GetComponent<SceneFadeInOut>();
		lastPlayerSighting = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<LastPlayerSighting>();
        
        
    }
	
    void Update() {
		// If health is less than or equal to 0...
		if (health <= 0f) {
			// ... and if the player is not yet dead...
			if (!playerDead) {
				// ... call the PlayerDying function.
				PlayerDying();
			}
			else {
				// Otherwise, if the player is dead, call the PlayerDead and LevelReset functions.
				PlayerDead();
				LevelReset();
			}
		}
        healthBar.UpdateBar(health,maxHealth);
    }

    IEnumerator PlayerHitColor ()
    {
        playerColor.SetColor("_Color", Color.red);
        yield return new WaitForSeconds(colorTime);
        playerColor.SetColor("_Color", Color.white);
    }
	
	void PlayerDying() {
		// The player is now dead.
		playerDead = true;
		
		// Set the animator's dead parameter to true also.
		anim.SetBool(hash.deadBool, playerDead);
	}
	
	void PlayerDead() {
        // Disable the collider and rigidbody so we don't get kicked around by the enemies.
        GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<Rigidbody>().useGravity = false;

		// If the player is in the dying state then reset the dead parameter.
		if (anim.GetCurrentAnimatorStateInfo(0).fullPathHash == hash.dyingState) {
			anim.SetBool(hash.deadBool, false);
		}

		// Reset the player sighting to turn off the alarms.
		lastPlayerSighting.lastPlayerPosition = lastPlayerSighting.resetPosition;
	}
	
	void LevelReset() {
		// Increment the timer.
		timer += Time.deltaTime;
		
		//If the timer is greater than or equal to the time before the level resets.
		if (timer >= resetAfterDeathTime) {
			// Reset the level.
			sceneFadeInOut.EndScene();
		}
	}
	
	public void TakeDamage(float amount) {
        // Decrement the player's health by amount.
        StartCoroutine(PlayerHitColor());
        StartCoroutine(Rumble());
        health -= amount;
        PopUpInstancier(Mathf.Round(amount));
    }

    //Vibracion del mando al recibir da�o
    IEnumerator Rumble()
    {
        controller.Rumble(0, 1f,1f);
        yield return new WaitForSeconds(0.3f);
        controller.Rumble(0, 0.0f, 0.0f);
    }
    //Metodo para hacer aparecer el popup del da�o
    void PopUpInstancier(float damage)
    {
        Instantiate(damagePopUp, player.position, Quaternion.identity);
        GameObject damageAmount = GameObject.FindGameObjectWithTag("PopUp");
        damageAmount.GetComponentInChildren<Text>().text = damage+"";
    }
}
