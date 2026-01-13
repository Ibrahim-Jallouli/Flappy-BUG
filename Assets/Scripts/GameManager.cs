using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private Player player;
    [SerializeField] private Spawner spawner;
    [SerializeField] private Text scoreText;
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject BirdFemale;
    [SerializeField] private GameObject winImage;
    [SerializeField] private GameObject getReady;
    
    public int score { get; private set; } = 0;
    private Coroutine winCoroutine;
    
    private Camera mainCamera;
    private static readonly HashSet<Pipes> activePipes = new HashSet<Pipes>();
    private readonly WaitForSeconds winWait1 = new WaitForSeconds(1.3f);
    private readonly WaitForSeconds winWait2 = new WaitForSeconds(0.5f);
    private readonly WaitForSeconds winWait3 = new WaitForSeconds(1f);
    public static void RegisterPipe(Pipes pipe) => activePipes.Add(pipe);
    public static void UnregisterPipe(Pipes pipe) => activePipes.Remove(pipe);

    private void Awake()
    {
        BirdFemale.SetActive(false);
        if (Instance != null) {
            DestroyImmediate(gameObject);
        } else {
            Instance = this;
            mainCamera = Camera.main;
        }
    }

    private void OnDestroy()
    {
        if (Instance == this) {
            Instance = null;
        }
    }

    private void Start()
    {
        gameOver.SetActive(false);
        winImage.SetActive(false);
        getReady.SetActive(true);
        Pause();
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        player.enabled = false;
    }

    public void Play()
    {
        score = 0;
        spawner.spawnRate =1.25f;
        scoreText.text = score.ToString();
        playButton.SetActive(false);
        gameOver.SetActive(false);
        getReady.SetActive(false);
        BirdFemale.SetActive(false);
        winImage.SetActive(false);
        spawner.enabled = true;
        Vector3 centerPosition = mainCamera.transform.position;
        centerPosition.z = 0f;
        player.transform.position = centerPosition;
        Time.timeScale = 1f;
        player.enabled = true;

        foreach (var pipe in activePipes)
        {
            if (pipe != null)
                Destroy(pipe.gameObject);
        }
        activePipes.Clear();
    }

    public void GameOver()
    {
        if (winCoroutine != null)
        {
            StopCoroutine(winCoroutine);
            winCoroutine = null;
        }
        
        playButton.SetActive(true);
        gameOver.SetActive(true);

        Pause();
    }

    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
        if (score > 20)
        {
            spawner.spawnRate = 1f;
        }
        if( score > 30)
        {
            spawner.spawnRate = 0.94f;
        }
        if (score == 38)
        {
            spawner.enabled = false;  
            winCoroutine = StartCoroutine(WaitForAllPipesDestroyed());
        }
    }

    private IEnumerator WaitForAllPipesDestroyed()
    {
        while (activePipes.Count > 0)
        {
            yield return null;        }

        player.enabled = false;
        StartCoroutine(WinSequence());
    }

    private IEnumerator WinSequence()
    {
        // Move player to center of screen first
        yield return AnimatePlayerToCenter();
        
        yield return winWait1;

        yield return AnimateBirdFemaleEntry();
        yield return winWait2;

        yield return AnimatePlayerToMeetBird();
        yield return winWait3;

        ShowWinUI();
        yield return winWait2;

        Time.timeScale = 0f;
    }

    private IEnumerator AnimateBirdFemaleEntry()
    {
        BirdFemale.SetActive(true);
        BirdFemale.transform.localScale = Vector3.zero;

        Vector3 startPos = new Vector3(mainCamera.transform.position.x + 9f, player.transform.position.y, 0);
        Vector3 targetPos = player.transform.position + Vector3.right * 3f;
        Vector3 targetScale = new Vector3(-1f, 1f, 1f);

        BirdFemale.transform.position = startPos;

        yield return AnimateTransform(BirdFemale.transform, startPos, targetPos, Vector3.zero, targetScale, 2f);

        BirdFemale.transform.localScale = targetScale;
    }

    private IEnumerator AnimatePlayerToMeetBird()
    {
        Vector3 startPos = player.transform.position;
        Vector3 targetPos = BirdFemale.transform.position - Vector3.right * 1.2f;

        float t = 0f;
        const float duration = 1.5f;

        while (t < duration)
        {
            t += Time.deltaTime;
            float easeOut = 1f - Mathf.Pow(1f - Mathf.Clamp01(t / duration), 3f);
            player.transform.position = Vector3.Lerp(startPos, targetPos, easeOut);
            yield return null;
        }

        player.transform.position = targetPos;
    }

    private IEnumerator AnimatePlayerToCenter()
    {
        Vector3 startPos = player.transform.position;
        Vector3 centerPos = mainCamera.transform.position;
        centerPos.z = 0f;

        float t = 0f;
        const float duration = 0.8f;

        while (t < duration)
        {
            t += Time.deltaTime;
            float easeOut = 1f - Mathf.Pow(1f - Mathf.Clamp01(t / duration), 3f);
            player.transform.position = Vector3.Lerp(startPos, centerPos, easeOut);
            yield return null;
        }

        player.transform.position = centerPos;
    }

    private IEnumerator AnimateTransform(Transform target, Vector3 startPos, Vector3 endPos, Vector3 startScale, Vector3 endScale, float duration)
    {
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            float normalizedTime = Mathf.Clamp01(t / duration);

            target.position = Vector3.Lerp(startPos, endPos, normalizedTime);
            target.localScale = Vector3.Lerp(startScale, endScale, Mathf.Clamp01(normalizedTime * 2f));

            yield return null;
        }
    }

    private void ShowWinUI()
    {
        if (winImage != null)
            winImage.SetActive(true);

        playButton.SetActive(true);
    }


}
