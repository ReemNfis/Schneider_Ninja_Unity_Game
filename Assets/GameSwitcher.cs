using UnityEngine;

public class GameSwitcher : MonoBehaviour
{
    public static GameSwitcher Instance;
    public SpriteRenderer background;
    public Sprite newBackground;
    public GameObject[] Cars;
    public AudioClip backgroundSound;
    public AudioSource audioSource;
    public int coinToSwitch = 2;
    public int currentPhase;

    private void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentPhase != 1)
        {
            if (LogicS.instance.coinScore >= coinToSwitch)
            {
                currentPhase = 1;
                background.sprite = newBackground;

                // استدعاء الوظيفة الجديدة هنا
                ResizeBackgroundToCamera();

                for (int i = 0; i < Cars.Length; i++)
                {
                    Cars[i].SetActive(false);
                }
                Cars[1].SetActive(true);
                audioSource.clip = backgroundSound;
                audioSource.Play();
            }
        }
    }

    void ResizeBackgroundToCamera()
    {
        // الحصول على أبعاد الكاميرا
        float cameraHeight = Camera.main.orthographicSize * 2f;
        float cameraWidth = cameraHeight * Camera.main.aspect;

        // الحصول على أبعاد الخلفية
        float backgroundWidth = background.sprite.bounds.size.x;
        float backgroundHeight = background.sprite.bounds.size.y;

        // حساب مقياس التحجيم (Scale) المطلوب
        float scaleX = cameraWidth / backgroundWidth;
        float scaleY = cameraHeight / backgroundHeight;

        // تطبيق التحجيم على الكائن
        background.transform.localScale = new Vector3(scaleX, scaleY, 1f);
    }
}