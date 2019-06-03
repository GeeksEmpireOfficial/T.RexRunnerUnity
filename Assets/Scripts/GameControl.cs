using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using GoogleMobileAds.Api;

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

    AdRequest adRequest;
    InterstitialAd interstitial;

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

        //AdUnitInterstitial ca-app-pub-2920235108594210/4068039384
        //AdUnitRewarded ca-app-pub-2920235108594210/1288946736
        #if UNITY_ANDROID
            string appId = "ca-app-pub-2920235108594210~9709242675";
        #endif

        MobileAds.Initialize(appId);

        this.interstitial = new InterstitialAd("ca-app-pub-2920235108594210/4068039384");
        adRequest = new AdRequest.Builder()
            .AddTestDevice("CDCAA1F20B5C9C948119E886B31681DE")
            .AddTestDevice("D101234A6C1CF51023EE5815ABC285BD")
            .AddTestDevice("65B5827710CBE90F4A99CE63099E524C")
            .AddTestDevice("DD428143B4772EC7AA87D1E2F9DA787C")
            .AddTestDevice("5901E5EE74F9B6652E05621140664A54")
            .Build();
        this.interstitial.LoadAd(adRequest);


        this.interstitial.OnAdLoaded += HandleOnAdLoaded;
        this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        this.interstitial.OnAdOpening += HandleOnAdOpened;
        this.interstitial.OnAdClosed += HandleOnAdClosed;
        this.interstitial.OnAdLeavingApplication += HandleOnAdLeavingApplication;
    }
	
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

        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
        }
        this.interstitial.LoadAd(adRequest);
    }

	void SpawnObstacle()
	{
		nextSpawn = Time.time + spawnRate;
		int randomObstacle = UnityEngine.Random.Range (0, obstacles.Length);
        
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

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        print("HandleAdLoaded event received");
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        print("HandleFailedToReceiveAd event received with message: "
                            + args.Message);
        this.interstitial.LoadAd(adRequest);
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        print("HandleAdOpened event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        print("HandleAdClosed event received");
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        print("HandleAdLeavingApplication event received");
    }
}
