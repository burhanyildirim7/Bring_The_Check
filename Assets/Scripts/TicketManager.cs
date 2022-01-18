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
		if(ticketCount < 14)
		{ 
			Tickets[ticketCount - 1].GetComponentInChildren<Renderer>().enabled = false;
			Tickets[ticketCount].GetComponentInChildren<Renderer>().enabled = true;
			//Tickets[ticketCount - 1].gameObject.SetActive(false);
			//Tickets[ticketCount].gameObject.SetActive(true);
		}
	}

	public void StartingEvents()
	{
		ticketCount = 0;
		for (int i = 0; i < Tickets.Count; i++)
		{
			Tickets[i].GetComponentInChildren<Renderer>().enabled = false;
			//Tickets[i].gameObject.SetActive(false);
		}
	}


}
