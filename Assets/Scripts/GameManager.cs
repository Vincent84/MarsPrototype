using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject[] players;        // Players' array
    private int activePlayer;            // The active player

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {

        // Change player with tab
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            int playerNumber = activePlayer;
            ChangePlayer(++playerNumber);

        }

        // Change player with numbers
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangePlayer(0); // Number 1
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			ChangePlayer(1); // Number 2
		}
        if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			ChangePlayer(2); // Number 3
		}
        if (Input.GetKeyDown(KeyCode.Alpha4))
		{
			ChangePlayer(3); // Number 4
		}
		
	}

    /// <summary>
    /// Changes the active player.
    /// </summary>
    /// <param name="playerNumber">The player to activate.</param>
    void ChangePlayer(int playerNumber)
    {
        // Get the active player and deactivate it. 
        PlayerControl playerManager = players[activePlayer].GetComponent<PlayerControl>();
        playerManager.active = false;

        // Check if the player number is greater than array's length
        if(playerNumber >= players.Length)
        {
            playerNumber = 0;
        }

        // Activate the new player
        activePlayer = playerNumber;
        players[activePlayer].GetComponent<PlayerControl>().active = true;

        // Set the camera position to active player's position
        CameraController cameraManager = Camera.main.GetComponent<CameraController>();
        cameraManager.player_1 = players[activePlayer].transform;
        cameraManager.pivot.transform.position = cameraManager.player_1.transform.position;
        cameraManager.pivot.transform.parent = cameraManager.player_1.transform;

    }
}
