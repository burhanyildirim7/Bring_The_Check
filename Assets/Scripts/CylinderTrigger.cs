using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderTrigger : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("FinalX"))
		{
			// En sona uzamýþtýr ve olay biter....
			PlayerController.instance.isFinalCube = true;
			
		}
		else if (other.CompareTag("x10"))
		{
			
			GameManager.instance.lastX = 10;
		}else if(other.CompareTag("x9") && GameManager.instance.lastX < 9)
		{
			GameManager.instance.lastX = 9;
		}
		else if (other.CompareTag("x8") && GameManager.instance.lastX < 8)
		{
			GameManager.instance.lastX = 8;
		}
		else if (other.CompareTag("x7") && GameManager.instance.lastX < 7)
		{
			GameManager.instance.lastX = 7;
		}
		else if (other.CompareTag("x6") && GameManager.instance.lastX < 6)
		{
			GameManager.instance.lastX = 6;
		}
		else if (other.CompareTag("x5") && GameManager.instance.lastX < 5)
		{
			GameManager.instance.lastX = 5;
		}
		else if (other.CompareTag("x4") && GameManager.instance.lastX < 4)
		{
			GameManager.instance.lastX = 4;
		}
		else if (other.CompareTag("x3") && GameManager.instance.lastX < 3)
		{
			GameManager.instance.lastX = 3;
		}
		else if (other.CompareTag("x2") && GameManager.instance.lastX < 2)
		{
			GameManager.instance.lastX = 2;
		}
		else if (other.CompareTag("x1") && GameManager.instance.lastX < 1)
		{
			GameManager.instance.lastX = 1;
		}

	}
}
