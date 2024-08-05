using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroConversation : MonoBehaviour
{
    // Start is called before the first frame update
    public List<string> conversationListName;
    public GameEvent gameEvent;

    public bool shouldStartIntroConversation = false;
    public int currentClip = 0;
    public int currentIndex = 0;
    private void OnEnable()
    {
        EventManager.Instance.StartListening(gameEvent, OnLevelStart);
        EventManager.Instance.StartListening(GameEvent.EmptyClicked, OnEmptyClicked);
    }

    private void OnDisable()
    {
        EventManager.Instance.StopListening(gameEvent, OnLevelStart);
        EventManager.Instance.StopListening(GameEvent.EmptyClicked, OnEmptyClicked);

    }

    private void OnLevelStart()
    {
        shouldStartIntroConversation = true;
    }    
    
    private void OnEmptyClicked()
    {
        if (shouldStartIntroConversation)
        {
            var script = this.transform.Find("Conversation").GetComponent<ConversationController>();
            if (script == null)
            {
                Debug.LogError("No such dialogue");
            }
            else
            {
               if(currentClip == 0)
               {
                   script.PlayDialogue(conversationListName[currentIndex]);
                   currentClip = 1;
               }
               else if(currentClip == 1)
               {
                   script.DisableDialogue(conversationListName[currentIndex]);
                   currentClip = 0;
                   currentIndex++;
                   if (currentIndex >= conversationListName.Count)
                   {
                       shouldStartIntroConversation = false;
                   }
               }
            }
        } 
        

    }
}
