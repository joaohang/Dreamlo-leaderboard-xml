using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Xml;
using System.Xml.Linq;
using Boo.Lang;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardManager : MonoBehaviour
{

    public string url = "http://dreamlo.com/lb/";
    public string myPrivateCode;
    public int numberLeaders;

    public Text[] leaderText;
    public InputField username;
    public InputField score;
    public Button refreshScore;
    public Button addScore;

    private void Start()
    {
        ButtonActions();
        DownloadAndPopulateLeaderboard();
    }
    
    private void ButtonActions()
    {
        refreshScore.onClick.AddListener(DownloadAndPopulateLeaderboard);
        addScore.onClick.AddListener(AddNewScore);
    }

    private void DownloadAndPopulateLeaderboard()
    {
        XmlDocument doc1 = new XmlDocument();
        doc1.Load(url + myPrivateCode + "/xml/" + numberLeaders.ToString());
        XmlElement root = doc1.DocumentElement;
        XmlNodeList nodes = root.SelectNodes("/dreamlo/leaderboard/entry");

        for (int i = 0; i < numberLeaders; i++)
        {
            XmlNode node = nodes[i];
            string name = "Name: " + node["name"].InnerText;
            string score = "- Score: " + node["score"].InnerText;

            leaderText[i].text = name + score;
        }
    }

    private void AddNewScore()
    {
        string username = this.username.text;
        string score = this.score.text;
        WWW www = new WWW(url + myPrivateCode + "/add/" + username + "/" + score);
    }
    
}
