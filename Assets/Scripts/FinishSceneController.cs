using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class FinishSceneController : MonoBehaviour
{
    [SerializeField] private Sprite _botonSprite;

    void Awake()
    {
        TextMeshProUGUI msgText = GameObject.Find("MessageText")?.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI timeText = GameObject.Find("TimeText")?.GetComponent<TextMeshProUGUI>();
        Button btn = GameObject.Find("MenuButton")?.GetComponent<Button>();

        if (msgText == null || timeText == null || btn == null)
        {
            GameObject canvasGO = new GameObject("FinishCanvas");
            Canvas canvas = canvasGO.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasGO.AddComponent<CanvasScaler>();
            canvasGO.AddComponent<GraphicRaycaster>();

            if (FindObjectOfType<UnityEngine.EventSystems.EventSystem>() == null)
            {
                GameObject esGO = new GameObject("EventSystem");
                esGO.AddComponent<UnityEngine.EventSystems.EventSystem>();
                esGO.AddComponent<UnityEngine.EventSystems.StandaloneInputModule>();
            }

            msgText = CreateText(canvasGO, "MessageText", "Felicidades! Has llegado al final", 80, new Vector2(0, 0.6f), new Vector2(1, 0.8f));
            timeText = CreateText(canvasGO, "TimeText", "Tiempo: 0.00 segundos", 50, new Vector2(0, 0.4f), new Vector2(1, 0.55f));

            btn = CreateButton(canvasGO);
        }
        else
        {
            msgText.text = "Felicidades! Has llegado al final";
            timeText.text = "Tiempo: 0.00 segundos";
            btn.onClick.RemoveAllListeners();
        }

        btn.onClick.AddListener(() => SceneManager.LoadScene("MainMenu"));
    }

    private TextMeshProUGUI CreateText(GameObject parent, string name, string text, float fontSize, Vector2 anchorMin, Vector2 anchorMax)
    {
        GameObject go = new GameObject(name);
        go.transform.SetParent(parent.transform, false);
        TextMeshProUGUI tmp = go.AddComponent<TextMeshProUGUI>();
        tmp.text = text;
        tmp.fontSize = fontSize;
        tmp.alignment = TextAlignmentOptions.Center;
        tmp.color = Color.white;
        RectTransform rt = go.GetComponent<RectTransform>();
        rt.anchorMin = anchorMin;
        rt.anchorMax = anchorMax;
        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.zero;
        return tmp;
    }

    private Button CreateButton(GameObject parent)
    {
        GameObject btnGO = new GameObject("MenuButton");
        btnGO.transform.SetParent(parent.transform, false);
        Button btn = btnGO.AddComponent<Button>();
        Image img = btnGO.AddComponent<Image>();
        if (_botonSprite != null)
            img.sprite = _botonSprite;
        else
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

        RectTransform btnRT = btnGO.GetComponent<RectTransform>();
        btnRT.anchorMin = new Vector2(0.35f, 0.25f);
        btnRT.anchorMax = new Vector2(0.65f, 0.35f);
        btnRT.offsetMin = Vector2.zero;
        btnRT.offsetMax = Vector2.zero;

        return btn;
    }
}
