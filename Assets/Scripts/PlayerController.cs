using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public Animator playerAnimator;
    [SerializeField] private GameObject _karakterPaketi;
    public Material ticketMat;
    public GameObject finalTicketPacket,ticketCylinder,sonPosCihazi,ilkPosCihazi;


    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }


    void Start()
    {
        StartingEvents();
    }




    private void OnTriggerEnter(Collider other)
    {
  
        if (other.CompareTag("musteri"))
        {
            // MUSTERIYE CARPINCA YAPILACAKLAR... 

            TicketManager.instance.IncreaseTicketCount();
            GameManager.instance.disabledObjects.Add(other.gameObject);
            other.gameObject.SetActive(false);
        }
        else if(other.CompareTag("ENGEL"))
		{
            // ENGELELRE CARPINCA YAPILACAKLAR.... 

            GameManager.instance.disabledObjects.Add(other.gameObject);
            other.gameObject.SetActive(false);
		}
        else if (other.CompareTag("Finish"))
		{
            // FINISH NOKTASINA GELINCE YAPILACAKLAR
            GameManager.instance.isContinue = false;
            PlayerIdleFixedArmAnim();
            if (TicketManager.instance.ticketCount > 0)
			{
                // BASARILI ISE....
                FinishEvents();
			}
			else
			{
                // BASARILI DEGILSE...
                UIController.instance.ActivateLooseScreen();
			}

        }

    }


    public void PlayerSkateAnim()
	{
        playerAnimator.ResetTrigger("idle");
        playerAnimator.ResetTrigger("idleFixedArm");
        playerAnimator.SetTrigger("skate");
	}

    public void PlayerIdleAnim()
	{
        playerAnimator.ResetTrigger("skate");
        playerAnimator.ResetTrigger("idleFixedArm");
        playerAnimator.SetTrigger("idle");
    }

    public void PlayerIdleFixedArmAnim()
	{
        playerAnimator.ResetTrigger("skate");
        playerAnimator.ResetTrigger("idle");
        playerAnimator.SetTrigger("idleFixedArm");
    }

    public void StartingEvents()
    {
        PlayerIdleAnim();
    }
    

    public void FinishEvents()
	{
        ilkPosCihazi.SetActive(false);
        sonPosCihazi.SetActive(true);
        CameraManager.instance.SetCameraForFinal();
        transform.parent.transform.rotation = Quaternion.Euler(0,180,0);
        StartCoroutine(RollingTicketFinal());
	}

    IEnumerator RollingTicketFinal()
	{
        yield return new WaitForSeconds(0.5f);
        bool control = true;
        float counter = 0;
		while (control)
		{
            counter += 0.05f;
            if(TicketManager.instance.ticketCount < counter)
			{
               // control = false;
			}
            float x = ticketMat.GetTextureOffset("_MainTex").x - 0.04f;
            ticketMat.SetTextureOffset("_MainTex", new Vector2(x, 0));
            finalTicketPacket.transform.position = new Vector3(
                finalTicketPacket.transform.position.x,finalTicketPacket.transform.position.y-0.08f,finalTicketPacket.transform.position.z);
            float s = ticketCylinder.transform.localScale.x-0.001f;
            ticketCylinder.transform.localScale = new Vector3(s, .6f,s);
            yield return new WaitForSeconds(0.01f);
            if (s < .2f) control = false;
        }
        
	}

    


}
