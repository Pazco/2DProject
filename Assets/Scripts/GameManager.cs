using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private string _finishMessage = "Felicidades! Has llegado al final";

    private float _startTime;
    private float _finishTime;
    private bool _finished;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void StartTimer()
    {
        _startTime = Time.time;
        _finished = false;
    }

    public void FinishGame()
    {
        if (_finished) return;
        _finished = true;
        _finishTime = Time.time;
        StartCoroutine(LoadSceneNextFrame("FinishScene"));
    }

    private IEnumerator LoadSceneNextFrame(string sceneName)
    {
        yield return null;
        SceneManager.LoadScene(sceneName);
    }

    public string GetFormattedTime()
    {
        float elapsed = _finishTime - _startTime;
        return elapsed.ToString("F2") + " segundos";
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "FinishScene")
        {
            CreateFinishUI();
        }
    }

    void CreateFinishUI()
    {
        GameObject timeGO = GameObject.Find("TimeText");
        if (timeGO != null)
        {
            timeGO.GetComponent<TextMeshProUGUI>().text = "Tiempo: " + GetFormattedTime();
            return;
        }

        GameObject canvasGO = new GameObject("FinishCanvas");
        Canvas canvas = canvasGO.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasGO.AddComponent<CanvasScaler>();
        canvasGO.AddComponent<GraphicRaycaster>();

        if (FindObjectOfType<EventSystem>() == null)
        {
            GameObject esGO = new GameObject("EventSystem");
            esGO.AddComponent<EventSystem>();
            esGO.AddComponent<StandaloneInputModule>();
        }

        GameObject msgGO = new GameObject("MessageText");
        msgGO.transform.SetParent(canvasGO.transform, false);
        TextMeshProUGUI msgText = msgGO.AddComponent<TextMeshProUGUI>();
        msgText.text = _finishMessage;
        msgText.fontSize = 80;
        msgText.alignment = TextAlignmentOptions.Center;
        msgText.color = Color.white;
        RectTransform msgRT = msgGO.GetComponent<RectTransform>();
        msgRT.anchorMin = new Vector2(0, 0.6f);
        msgRT.anchorMax = new Vector2(1, 0.8f);
        msgRT.offsetMin = Vector2.zero;
        msgRT.offsetMax = Vector2.zero;

        timeGO = new GameObject("TimeText");
        timeGO.transform.SetParent(canvasGO.transform, false);
        TextMeshProUGUI timeText = timeGO.AddComponent<TextMeshProUGUI>();
        timeText.text = "Tiempo: " + GetFormattedTime();
        timeText.fontSize = 50;
        timeText.alignment = TextAlignmentOptions.Center;
        timeText.color = Color.white;
        RectTransform timeRT = timeGO.GetComponent<RectTransform>();
        timeRT.anchorMin = new Vector2(0, 0.4f);
        timeRT.anchorMax = new Vector2(1, 0.55f);
        timeRT.offsetMin = Vector2.zero;
        timeRT.offsetMax = Vector2.zero;

        GameObject btnGO = new GameObject("MenuButton");
        btnGO.transform.SetParent(canvasGO.transform, false);
        Button btn = btnGO.AddComponent<Button>();
        Image img = btnGO.AddComponent<Image>();
        img.color = new Color(0.2f, 0.2f, 0.2f, 0.8f);

        GameObject btnTextGO = new GameObject("ButtonText");
        btnTextGO.transform.SetParent(btnGO.transform, false);
        TextMeshProUGUI btnText = btnTextGO.AddComponent<TextMeshProUGUI>();
        btnText.text = "Volver al Menu";
        btnText.fontSize = 32;
        btnText.alignment = TextAlignmentOptions.Center;
        btnText.color = Color.white;
        RectTransform btnTextRT = btnTextGO.GetComponent<RectTransform>();
        btnTextRT.anchorMin = Vector2.zero;
        btnTextRT.anchorMax = Vector2.one;
        btnTextRT.offsetMin = Vector2.zero;
        btnTextRT.offsetMax = Vector2.zero;

        btn.onClick.AddListener(GoToMainMenu);

        RectTransform btnRT = btnGO.GetComponent<RectTransform>();
        btnRT.anchorMin = new Vector2(0.35f, 0.15f);
        btnRT.anchorMax = new Vector2(0.65f, 0.25f);
        btnRT.offsetMin = Vector2.zero;
        btnRT.offsetMax = Vector2.zero;
    }

    void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
