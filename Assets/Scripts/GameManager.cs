using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public enum GameState
    {
        EXPLORATION,
        BUILDING
    }

    public GameObject[] players;        // Players' array
    public Resource[] resources;
    public static GameState currentState;

    private int activePlayer;            // The active player
    private CameraControl cameraControl;

    void Awake()
    {
        cameraControl = Camera.main.GetComponent<CameraControl>();
        resources = ResourceManager.Instance.ResourcesAvailable;
        currentState = GameState.EXPLORATION;
    }

    void Start()
    {
        //resourceManager = new ResourceManager();

        /*ResourceManager.IncreasesResources("Metallo", 10);       
        ResourceManager.IncreasesResources("Argento", 5);       
        ResourceManager.IncreasesResources("Rame", 19);*/       
    }
	
	// Update is called once per frame
	void Update ()
    {

        if(Input.GetKeyDown(KeyCode.Z))
        {
            ResourceManager.Instance.Print();
        }

        if(currentState == GameState.EXPLORATION)
        {
            
            // Change player with tab
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                int playerNumber = activePlayer;
                ChangePlayer(++playerNumber);

            }

            // Change player with numbers
            if (Input.GetKeyDown(KeyCode.Alpha1))
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

        CameraControl cameraManager = Camera.main.GetComponent<CameraControl>();
        CameraBehaviour cameraBehaviour = cameraManager.cameraBehaviour;

        if (cameraBehaviour.cameraType == 0)
        {



            ThirdPersonCamera thirdPersonCamera = cameraBehaviour.gameObject.GetComponent<ThirdPersonCamera>();
            thirdPersonCamera.player_1 = players[activePlayer].transform;
            thirdPersonCamera.pivot.transform.position = thirdPersonCamera.player_1.transform.position;
            thirdPersonCamera.pivot.transform.parent = thirdPersonCamera.player_1.transform;
        }
        
        /*CameraController cameraManager = Camera.main.GetComponent<CameraController>();
        cameraManager.player_1 = players[activePlayer].transform;
        cameraManager.pivot.transform.position = cameraManager.player_1.transform.position;
        cameraManager.pivot.transform.parent = cameraManager.player_1.transform;*/

    }
}
