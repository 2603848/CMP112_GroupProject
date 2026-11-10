using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthElement : MonoBehaviour
{
    private float _Health;
    private float _HealthDisplaying = 100;
 
    // Don't want to import a tweening library for just this one function
    private float DisplayTimer = 0;
    public float TimeToMoveToNumber = 0.25f;

    static HealthElement SingletonReference;

    public TextMeshProUGUI HealthTextElement;
    public Image BackImage;

    private float Health { 
        set {
            DisplayTimer = 0;
            _Health = value;

        } get => _Health; }
    
    public AnimationCurve DisplayTween;
    
    public static void SetHealthDisplayed(float hp)
    {
        SingletonReference.Health = hp;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SingletonReference = this;
    }

    // Update is called once per frame
    void Update()
    {
        DisplayTimer += Time.deltaTime;
        if (DisplayTimer < TimeToMoveToNumber)
        {
            // First evaluate the percent we're through the animation (DisplayTween.Evaluate(DisplayTimer / TimeToMoveToNumber)), looking up the value we're at on the tweening curve
            // And then get the amount we need to get the amount of health we need to update to (Health - _HealthDisplaying)
            _HealthDisplaying += DisplayTween.Evaluate(DisplayTimer / TimeToMoveToNumber) * (Health - _HealthDisplaying);
        }
        // if the animation is finished
        if (DisplayTimer > TimeToMoveToNumber)
        {
            DisplayTimer = TimeToMoveToNumber;
            _HealthDisplaying = Health;
        }
        // update elements
        HealthTextElement.text = (int)_HealthDisplaying + " %";
        BackImage.fillAmount = _HealthDisplaying / 100.0f;

    }
}
