using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

public class CreditsScript : MonoBehaviour {

    public float heagingOffset;
    public float nameOffset;
    public float moveSpeed;
    public GUIStyle guiStyle;
    private Rect rectPosition;
    private bool creditsFinished;
	// Use this for initialization
	void Start () {
        Debug.Log(Screen.height);
	}
	
	// Update is called once per frame
	void Update () {
        rectPosition = new Rect(Screen.width / 2 - 250, Screen.height - (moveSpeed * Time.time), 500, 800);
        if(rectPosition.y <= -800)
        {
            creditsFinished = true;
        }
    }

    void OnGUI()
    {
        GUILayout.BeginArea(rectPosition, "", guiStyle);
        GUILayout.BeginVertical();
        GUILayout.Label("<color=yellow>Programmers</color>", guiStyle);
        GUILayout.Label("_______________", guiStyle);
        GUILayout.Space(nameOffset);
        GUILayout.Label("Daniel Nelson", guiStyle);
        GUILayout.Space(nameOffset);
        GUILayout.Label("Tareq Ahmed", guiStyle);
        GUILayout.Space(heagingOffset);
        GUILayout.Label("<color=yellow>3D Artist</color>", guiStyle);
        GUILayout.Label("__________________", guiStyle);
        GUILayout.Space(nameOffset);
        GUILayout.Label("Niall Doherty", guiStyle);
        GUILayout.Space(heagingOffset);
        GUILayout.Label("<color=yellow>Audio Artist</color>", guiStyle);
        GUILayout.Label("__________________", guiStyle);
        GUILayout.Space(nameOffset);
        GUILayout.Label("Thomas Engebretsen", guiStyle);
        GUILayout.Space(heagingOffset);
        GUILayout.Label("<color=yellow>3D Model Credits</color>", guiStyle);
        GUILayout.Label("__________________", guiStyle);
        GUILayout.Space(nameOffset);
        GUILayout.Label("TripleBrick", guiStyle);
        GUILayout.Space(heagingOffset);
        GUILayout.Label("<color=yellow>Special Mentions</color>", guiStyle);
        GUILayout.Label("__________________", guiStyle);
        GUILayout.Space(nameOffset);
        GUILayout.Label("Oliver Brown", guiStyle);
        GUILayout.Space(nameOffset);
        GUILayout.Label("David Ayres", guiStyle);
        GUILayout.Space(heagingOffset);
        GUILayout.Label("<size=40><color=yellow>Made with Unity©</color></size>", guiStyle);
        GUILayout.EndVertical();
        GUILayout.EndArea();

        if(creditsFinished)
        {
            GUI.color = new Color(1, 1, 1, Mathf.Lerp(0, 1, 0.05f * Time.time));
            GUILayout.BeginArea(new Rect(Screen.width / 2 - 250, Screen.height / 2, 500, 500), "");
            if(GUILayout.Button("Return to Main Menu"))
            {
                SceneManager.LoadScene("MainMenu");
            }
            if(GUILayout.Button("Find us on Facebook"))
            {
                Application.OpenURL("https://www.facebook.com/Midnight-1501059023532088/");
            }
            //if(GUILayout.Button("Send Developer Feedback"))
            //{
            //    SendEmail();
            //}
            GUILayout.EndArea();
        }
    }

    //void SendEmail()
    //{
    //    MailMessage mail = new MailMessage();

    //    mail.From = new MailAddress("test@gmail.com");
    //    mail.To.Add("1991.danielnelson@gmail.com");
    //    mail.Subject = "Test Mail";
    //    mail.Body = "This is for testing SMTP mail from GMAIL";

    //    SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
    //    smtpServer.Port = 587;
    //    smtpServer.Credentials = new System.Net.NetworkCredential("1991.danielnelson@gmail.com", "koudelka3214") as ICredentialsByHost;
    //    smtpServer.EnableSsl = true;
    //    ServicePointManager.ServerCertificateValidationCallback =
    //        delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
    //        { return true; };
    //    smtpServer.Send(mail);
    //    Debug.Log("success");
    //}
}
