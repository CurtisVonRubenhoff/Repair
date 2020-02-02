using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TextMaster: MonoBehaviour {
  [SerializeField]
  private Text MainTextBox;

  [SerializeField]
  private Text InteractionIndicator;

  [SerializeField]
  private Text SpeakerName;

  [SerializeField]
  private Image TextBackground;

  [SerializeField]
  private float letterpause;

  [SerializeField]
  private GameObject Choices;

  public Button AcceptButton;
  public Button DenyButton;

  public GameObject GodParent;
  public GameObject MainCloud;


  public static TextMaster instance;
  private void Start() {
    if (instance == null) instance = this;
  }

  public static void ShowText(Message message) {
    instance.StopAllCoroutines();
    ClearText();
    instance.TextBackground.enabled = true;
    instance.SpeakerName.text = message.speaker;
    if (message.speaker == "Yahweh") {
      instance.MainCloud.transform.rotation = Quaternion.Euler(Vector3.zero);
      instance.GodParent.SetActive(true);
    }
    instance.StartCoroutine(instance.typeText(message.message));
    if (message.isPrompt) EnableChoices();
  }

  public static void ClearText() {
    instance.StopAllCoroutines();
    instance.SpeakerName.text = "";
    instance.MainTextBox.text = "";
  }

  public static void IndicatorOn(string name) {
    if (name == "") return;
    instance.InteractionIndicator.text = string.Format("[E] {0}", name);
  }

  public static void IndicatorOff() {
    instance.InteractionIndicator.text = "";
  }

  public static void DisableChoices() {
    Cursor.visible = false;
    instance.Choices.SetActive(false);
  }

  public static void EnableChoices() {
    Cursor.visible = true;
    instance.Choices.SetActive(true);
  }

  public static void EndText() {
      IndicatorOff();
      ClearText();
      instance.GodParent.SetActive(false);
      instance.TextBackground.enabled = false;
  }

  private IEnumerator typeText(string s)
  {
    foreach (char letter in s.ToCharArray())
    {
      MainTextBox.text += letter;
      yield return new WaitForSeconds(letterpause);
    }
  }
}