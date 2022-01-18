using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public Animator playerAnimator,paraAnim;
    [SerializeField] private GameObject _karakterPaketi;
    public Material ticketMat;
    public GameObject finalTicketPacket,ticketCylinder,sonPosCihazi,ilkPosCihazi,paraEfekti;
    public bool isFinalCube;


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
            other.GetComponent<Collider>().enabled = false;
            TicketManager.instance.IncreaseTicketCount();
           // GameManager.instance.disabledObjects.Add(other.gameObject);
            GameManager.instance.IncreaseScore();
            //other.gameObject.SetActive(false);
            paraAnim.SetTrigger("para");
            StopCoroutine(UIController.instance.SliderDecrease());
            StartCoroutine(UIController.instance.SliderIncrease());
            StartCoroutine(ActivateCollider(other.gameObject));
            other.transform.parent.transform.GetChild(0).gameObject.SetActive(true);
            StartCoroutine(DeactivateEffect(other.transform.parent.transform.GetChild(0).gameObject));
            
        }
        else if(other.CompareTag("ENGEL"))
		{
            // ENGELELRE CARPINCA YAPILACAKLAR.... 
            StopCoroutine(UIController.instance.SliderIncrease());
            StartCoroutine(UIController.instance.SliderDecrease());
            //GameManager.instance.disabledObjects.Add(other.gameObject);
            GameManager.instance.DecreaseScore();
            GameObject effect = Instantiate(paraEfekti);
            effect.transform.SetParent(transform);
            effect.transform.localPosition = new Vector3(0, 1.2f, 0);
            PlayerHitAnim();
           // other.gameObject.SetActive(false);
		}
        else if (other.CompareTag("Finish"))
		{
            // FINISH NOKTASINA GELINCE YAPILACAKLAR
            other.transform.parent.transform.GetChild(0).gameObject.SetActive(true);
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

    public void PlayerHitAnim()
	{
        playerAnimator.ResetTrigger("idle");
        playerAnimator.ResetTrigger("idleFixedArm");
        playerAnimator.ResetTrigger("skate");
        playerAnimator.SetTrigger("hit");
    }

    public void PlayerSkateAnim()
	{
        playerAnimator.ResetTrigger("hit");
        playerAnimator.ResetTrigger("idle");
        playerAnimator.ResetTrigger("idleFixedArm");
        playerAnimator.SetTrigger("skate");
	}

    public void PlayerIdleAnim()
	{
        playerAnimator.ResetTrigger("hit");
        playerAnimator.ResetTrigger("skate");
        playerAnimator.ResetTrigger("idleFixedArm");
        playerAnimator.SetTrigger("idle");
    }

    public void PlayerIdleFixedArmAnim()
	{
        playerAnimator.ResetTrigger("hit");
        playerAnimator.ResetTrigger("skate");
        playerAnimator.ResetTrigger("idle");
        playerAnimator.SetTrigger("idleFixedArm");
    }

    public void StartingEvents()
    {
        PlayerIdleAnim();
        transform.parent.transform.rotation = Quaternion.Euler(0, 0, 0);
        transform.parent.transform.position = Vector3.zero;
        sonPosCihazi.SetActive(false);
        ilkPosCihazi.SetActive(true);
        CameraManager.instance.SetCameraForStart();
        GameManager.instance.isContinue = false;
        GameManager.instance.score = 0;
        UIController.instance.playerSlider.value = 0;
        isFinalCube = false;
        ticketMat.SetTextureOffset("_MainTex", Vector2.zero);
        finalTicketPacket.transform.position = new Vector3(0,0,-2f);
        ticketCylinder.transform.localScale = new Vector3(1.5f,0.45f,1.5f);
        transform.position = new Vector3(0,transform.position.y,0);
    }
    

    public void FinishEvents()
	{
        
        if(GameManager.instance.score > 5)
		{
            // baþarýlý bir bitiriþten sonra...
            StartCoroutine(RollingTicketFinal());
        }
		else
		{
            // baþarýsýz bir bitiriþten sonra..
            UIController.instance.ActivateLooseScreen();
		}
        
	}

    public IEnumerator RollingTicketFinal()
	{
        yield return new WaitForSeconds(1.5f);
        ilkPosCihazi.SetActive(false);
        sonPosCihazi.SetActive(true);
        CameraManager.instance.SetCameraForFinal();
        transform.parent.transform.rotation = Quaternion.Euler(0, 180, 0);
        yield return new WaitForSeconds(0.5f);
        bool control = true;
        float counter = 0;
        float localScaleMult = .05f / TicketManager.instance.ticketCount;
		while (control && !isFinalCube)
		{
            counter += 0.05f;
            if(TicketManager.instance.ticketCount < counter)
			{
               // control = false;
			}
            float x = ticketMat.GetTextureOffset("_MainTex").x - 0.08f;
            ticketMat.SetTextureOffset("_MainTex", new Vector2(x, 0));
            finalTicketPacket.transform.position = new Vector3(
                finalTicketPacket.transform.position.x,finalTicketPacket.transform.position.y-0.16f,finalTicketPacket.transform.position.z);
            float s = ticketCylinder.transform.localScale.x-localScaleMult;
            ticketCylinder.transform.localScale = new Vector3(s, ticketCylinder.transform.localScale.y,s);
            yield return new WaitForSeconds(0.01f);
            if (s < .2f) control = false;
        }
        SuccessfullFinishEvents();
        
	}

    public void SuccessfullFinishEvents()
	{
        GameManager.instance.FinalScoreMultiply();
        UIController.instance.ActivateWinScreen();
	}

    IEnumerator ActivateCollider(GameObject obj)
	{
        yield return new WaitForSeconds(1);
        obj.GetComponent<Collider>().enabled = true;
	}

    IEnumerator DeactivateEffect(GameObject obj)
    {
        yield return new WaitForSeconds(1);
        obj.SetActive(false);
    }






}
