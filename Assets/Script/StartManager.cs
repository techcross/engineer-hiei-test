
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StartManager : MonoBehaviour
{
    [SerializeField] private GameObject Difficulty;
    [SerializeField] private GameObject ModeChoise;
    [SerializeField] private GameObject DoubleGame;
    [SerializeField] private backGoround back;
    [SerializeField] private Master master;
    private Sound sound;
    public GameState gameState;
    private GameObject bgm;
    // Start is called before the first frame update
    void Start()
    {
        bgm = GameObject.Find("SoundManager");
        sound = bgm.GetComponent<Sound>();;
        Instantiate(ModeChoise, new Vector3(0, 0, 0), Quaternion.identity);
        sound.PlayTitle();
        back.OnClickPlay();
        master.Difficulty = Master.Mode.normal;
    }

    // Update is called once per frame
    void Update()
    {
        switch (gameState)
        {
            case GameState.Start:
                break;
            case GameState.Shingle:
                var objects = GameObject.FindGameObjectsWithTag("Select");
                foreach (var obj in objects)
                {
                    Destroy(obj);
                }
                sound.playPush(2);
                Instantiate(Difficulty, new Vector3(0, 0, 0), Quaternion.identity);
                gameState = GameState.Wait;
                break;
            case GameState.Select:
                //シーン遷移
                gameState = GameState.Wait;
                gameState = GameState.Change;
                break;
            case GameState.Easy:
                master.Difficulty = Master.Mode.easy;
                gameState = GameState.Wait;
                break;
            case GameState.Normal:
                master.Difficulty = Master.Mode.normal;
                gameState = GameState.Wait;
                break;
            case GameState.Hard:
                master.Difficulty = Master.Mode.hard;
                gameState = GameState.Wait;
                break;
            case GameState.doubles:
                 objects = GameObject.FindGameObjectsWithTag("Select");
                 sound.playPush(2);
                foreach (var obj in objects)
                {
                    Destroy(obj);
                }
                Instantiate(DoubleGame, new Vector3(0, 0, 0), Quaternion.identity);
                gameState = GameState.doublesWait;
                break;
            case GameState.Return:
                objects = GameObject.FindGameObjectsWithTag("Select");
                foreach (var obj in objects)
                {
                    Destroy(obj);
                }
                Instantiate(ModeChoise, new Vector3(0, 0, 0), Quaternion.identity);
                gameState = GameState.Start;
                break;
            case GameState.doubleSelect:
                //シーン遷移
                master.Difficulty = Master.Mode.doubles;
                master.GetState(gameState);
                sound.playPush(3);
                //Loading.gameObject.SetActive(true);
                SceneManager.LoadScene("DoubleBattle");
                break;
            case GameState.Change:
                Invoke(nameof(SceneChange),0.5f);
                gameState = GameState.Wait;
                break;
        }

     
    }

    private void SceneChange()
    {
        SceneManager.LoadSceneAsync("Battle");
    }

}
