/* Talkback Helper version 0.1 

Description: This script is used to show controls and other additional information to the user
while playing. Such controls would be F to use flashlight, E to use an object, Left click to drag, etc. 

This script is attatched to the player as we need to make use of the Update function. Which, according to Unity, 
can only be done if inheriting from Mono.

*/ 

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

namespace TalkbackHelper {

    public class ControlsAssist
    {
        [Tooltip("Chapter Is playinh?")]
        public bool chapterPlaying;
        public bool itemMessageDisplayed = false;

        public int ChapterNumber = 0;
        public List<string> ChapterDialog = new List<string>();

        // optional put in float delay...this will need to have a for loop and a yield return new wait for seconds
        public IEnumerator GameCharacterScript(int ChapterNumber)
        {
            chapterPlaying = true;
            Debug.Log("Character game script called");

            switch (ChapterNumber)
            {
                case 0:
                    ChapterDialog.Add("Where am I? What the hell is this place? \n I can't...I can't remember what happened.");
                    ChapterDialog.Add("Well, there's no point hanging around...\nthis place gives me the creeps...");
                break;

                case 1:
                    
                break;

            }

            for(int i = 0; i < ChapterDialog.Count; i++)
            {
                //canvas_textObject.text = ChapterDialog[i].ToString();
                Debug.Log("Character Dialog Call: "+ChapterDialog[i].ToString()+"");
                //canvas_textObject.CrossFadeAlpha(0, 4.5f, false);
                yield return new WaitForSeconds(6f);
                //canvas_textObject.CrossFadeAlpha(1, 0, false);
            }

            //canvas_textObject.text = "";
            //canvas_textObject.canvasRenderer.SetAlpha(0);
            ChapterDialog.Clear();
            chapterPlaying = false;
        }

        
        public void ObjectiveReminder()
        {
            // If chapter not playing, continue else return control back
            if (chapterPlaying)
            {
                return;
            }
            else
            {
                TaskManager taskManager = GameObject.FindGameObjectWithTag("Player").GetComponent<TaskManager>();

                Text canvas_textObject;
                Image _background;

                canvas_textObject = GameObject.Find("_talkbackHelper").GetComponent<Text>();
                _background = GameObject.Find("_backgroundSprite").GetComponent<Image>();


                canvas_textObject.enabled = true;
                _background.enabled = true;

                canvas_textObject.canvasRenderer.SetAlpha(1);
                _background.canvasRenderer.SetAlpha(1);

                canvas_textObject.text = taskManager.taskList[0].taskDesc.ToString();

                canvas_textObject.CrossFadeAlpha(0, 2.5f, false);
                _background.CrossFadeAlpha(0, 2.5f, false);
            }
        }

        public void ItemLookAtMessage(string itemName)
        {
            if(itemMessageDisplayed == false)
            {
                /*string[] optionalWords = new string[] { "What's this? It looks to be the ",
                                                        "This must be the "};

                Text canvas_textObject;

                canvas_textObject = GameObject.Find("_talkbackHelper").GetComponent<Text>();

                canvas_textObject.enabled = true;

                canvas_textObject.canvasRenderer.SetAlpha(1);

                canvas_textObject.text = optionalWords[Random.Range(0, optionalWords.Length)] + "<color=red>" + itemName + "</color>";

                canvas_textObject.CrossFadeAlpha(0, 2.5f, false);*/

                
            }
        }

        public void CheckInventoryItems()
        {
            string[] optionalWords = new string[] { "Hmm, let's see here... ",
                                                    "So far, I've managed to find the "};

            CharacterScript character;
            character = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterScript>();

            string temp = "";

            Text canvas_textObject;

            canvas_textObject = GameObject.Find("_talkbackHelper").GetComponent<Text>();

            canvas_textObject.enabled = true;

            canvas_textObject.canvasRenderer.SetAlpha(1);
            if (character.inventoryItems.Count <= 0)
            {
                canvas_textObject.text = "Hmm, I don't have any items yet.";
            }
            else
            {
                for (int i = 0; i < character.inventoryItems.Count; i++)
                {
                    temp = temp + character.inventoryItems[i].name + ", ";

                    if (i >= character.inventoryItems.Count)
                    {
                        temp = temp + character.inventoryItems[i].name + ".";
                    }

                }
                canvas_textObject.text = optionalWords[Random.Range(0, optionalWords.Length)] + "<color=red>" + temp + "</color>";
            }

           

            canvas_textObject.CrossFadeAlpha(0, 2.5f, false);
        }

    }

}
