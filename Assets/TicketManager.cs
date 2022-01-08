using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicketManager : MonoBehaviour
{
    public static TicketManager instance;

	public int ticketCount = 0;
	public List<GameObject> Tickets = new List<GameObject>();

	private void Awake()
	{
		if (instance == null) instance = this;
		else Destroy(this);
	}

	public void IncreaseTicketCount()
	{
		ticketCount++;

		Tickets[ticketCount - 1].gameObject.SetActive(false);
		Tickets[ticketCount].gameObject.SetActive(true);
	}


}
