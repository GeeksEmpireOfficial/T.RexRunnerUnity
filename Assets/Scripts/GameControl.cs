using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameControl : MonoBehaviour {

	public static GameControl instance = null;
    
	[SerializeField]
	GameObject restartButton;

	[SerializeField]
	Text highScoreText;

	[SerializeField]
	Text yourScoreText;

	[SerializeField]
	GameObject[] obstacles;

	[SerializeField]
	Transform spawnPointTree, spawnPointBird;
    
    [SerializeField]
	float spawnRate = 2f;
	float nextSpawn;

	[SerializeField]
	float timeToBoost = 5f;
	float nextBoost;

	int highScore = 0, yourScore = 0;

	public static bool gameStopped;

	float nextScoreIncrease = 0f;

    [SerializeField]
    GameObject Trex, Birds;

    [SerializeField]
    Sprite TrexDead;

	// Use this for initialization
	void Start () {
		
		if (instance == null) 
			instance = this;
		else if (instance != this)
			Destroy (gameObject);

		restartButton.SetActive (false);
		yourScore = 0;
		gameStopped = false;
		Time.timeScale = 1f;
		highScore = PlayerPrefs.GetInt ("highScore");
		nextSpawn = Time.time + spawnRate;
		nextBoost = Time.unscaledTime + timeToBoost;
	}
	
	// Update is called once per frame
	void Update () {
		if (!gameStopped)
			IncreaseYourScore ();

		highScoreText.text = "" + highScore;
		yourScoreText.text = "" + yourScore;

		if (Time.time > nextSpawn)
			SpawnObstacle ();

		if (Time.unscaledTime > nextBoost && !gameStopped)
			BoostTime ();
    }

	public void DinoHit()
	{
		if (yourScore > highScore)
        {
            PlayerPrefs.SetInt("highScore", yourScore);
            highScore = yourScore;
            highScoreText.text = "" + highScore;
        }

        Time.timeScale = 0;
		gameStopped = true;
		restartButton.SetActive (true);

        this.Trex.GetComponent<Animator>().enabled = false;
        this.Trex.GetComponent<SpriteRenderer>().sprite = TrexDead;
    }

	void SpawnObstacle()
	{
		nextSpawn = Time.time + spawnRate;
		int randomObstacle = Random.Range (0, obstacles.Length);
        
        if (randomObstacle == 0)//cactus
        {
            Instantiate(obstacles[randomObstacle], spawnPointTree.position, Quaternion.identity);
        } else if (randomObstacle == 1)//bird
        {
            Instantiate(obstacles[randomObstacle], spawnPointBird.position, Quaternion.identity);
        }
    }

	void BoostTime()
	{
		nextBoost = Time.unscaledTime + timeToBoost;
		Time.timeScale += 0.13f;
	}

	void IncreaseYourScore()
	{
		if (Time.unscaledTime > nextScoreIncrease) {
			yourScore += 1;
			nextScoreIncrease = Time.unscaledTime + 1;
		}
	}

	public void RestartGame()
	{
        if (SceneManager.GetActiveScene().name.Equals("GameScenePhone"))
        {
            SceneManager.LoadScene("GameScenePhone");
        } else if (SceneManager.GetActiveScene().name.Equals("GameScene"))
        {
            SceneManager.LoadScene("GameScene");
        }
	}
}
