using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Message {
  public bool isPrompt;
  public string message;
  public string speaker;

  public Message(bool ip, string m, string s) {
    isPrompt = ip;
    message = m;
    speaker = s;
  }
}