using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickScript : MonoBehaviour {

	public int points; // Besar point yg dimiliki brick
	public int hitPoint; // Ketahanan brick

	// Fungsi damage
	public void BreakBrick ()
	{
		hitPoint--;
	}

}
