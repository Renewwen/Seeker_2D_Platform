using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour {

    [SerializeField] int playerLives = 3;
    [SerializeField] int score = 0;

    [SerializeField] Text livesText;
    [SerializeField] Text scoreText;
    [SerializeField] Text timerText;
    [SerializeField] float delayTimeAfterDeath = 2f;

    private float startTime;
    private Player player;
    public GameObject currentCheckpoint;

    /*
    private void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1){
            Destroy(gameObject);
            //Debug.Log("delete gamesession");
        }else{
            DontDestroyOnLoad(gameObject);
            //Debug.Log("Not delete gamesession");
        }
    }
    */

    // Use this for initialization
    void Start () {
        livesText.text = playerLives.ToString();
        scoreText.text = score.ToString();
        startTime = Time.time;

        player = FindObjectOfType<Player>();
	}

    private void Update()
    {
        float t = Time.time - startTime;

        string minutes = ((int)t / 60).ToString();
        string seconds = (t % 60).ToString("f2");

        timerText.text = minutes + ":" + seconds;
    }

    public void AddToScore(int pointsToAdd){
        score += pointsToAdd;
        scoreText.text = score.ToString();
    }

    public void ProcessPlayerDeath(){
        if (playerLives > 0){
            Invoke("TakeLife", delayTimeAfterDeath);
        }else{
            Invoke("ResetGameSession", delayTimeAfterDeath);
        }
    }

    private void ResetGameSession()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    private void TakeLife()
    {
        playerLives++;
        //Use this method to reload the scene...
        //var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        //SceneManager.LoadScene(currentSceneIndex);
        livesText.text = playerLives.ToString();

        //Use this method for Checkpoint!!!
        player.Invoke("Respawn", 0.02f);
        player.transform.position = currentCheckpoint.transform.position;
        //player.Invoke("Respawn", 0.1f);
        //player.Respawn();

    }
}
