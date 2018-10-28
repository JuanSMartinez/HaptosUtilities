using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace HaptosUtilities
{
    public class PersistenceUtilities
    {

        //Singleton instance
        private static PersistenceUtilities instance;

        public static PersistenceUtilities Instance
        {
            get
            {
                if (instance == null)
                    instance = new PersistenceUtilities();
                return instance;
            }
        }

        //Creates a folder for a player
        public bool CreateFolerForPlayer(string playerName)
        {
            if(!Directory.Exists(Application.dataPath + "/" + playerName))
            {
                Directory.CreateDirectory(Application.dataPath + "/" + playerName);
                return true;
            }
            return false;
        }

        //Load a character
        public Player LoadPlayer(string name)
        {
            string filePath = Application.dataPath + "/" + name + "/" + name + ".gd";
            if (File.Exists(filePath))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(filePath, FileMode.Open);
                Player player = (Player)bf.Deserialize(file);
                file.Close();
                return player;
            }
            else
                throw new FileLoadException("File for player not found");

        }

        //Save the current player
        public void SaveCurrentPlayer()
        {
            if (!Game.Instance.SuperPlayer)
            {
                string filePath = Application.dataPath + "/" + Game.CurrentPlayer.Name + "/" + Game.CurrentPlayer.Name + ".gd";
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Create(filePath);
                bf.Serialize(file, Game.CurrentPlayer);
                file.Close();
            }
        }

        //Get the list of saved games
        public List<string> GetSavedGames()
        {
            List<string> result = new List<string>();
            DirectoryInfo dir = new DirectoryInfo(Application.dataPath);
            DirectoryInfo[] directories = dir.GetDirectories();
            foreach (DirectoryInfo info in directories)
            {
                FileInfo[] infoFiles = info.GetFiles("*.gd");
                foreach (FileInfo information in infoFiles)
                {
                    result.Add(information.Name.Split("."[0])[0]);
                }
            }
            return result;
        }

        //Delete a saved game
        public void DeleteSavedGame(string playerName)
        {
            string filePath = Application.dataPath + "/" + playerName + "/" + playerName + ".gd";
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                Directory.Delete(Application.dataPath + "/" + playerName, true);
            }
            else
                throw new FileLoadException("The data for the player is deleted or corrupted");
        }

        //Save a quest log
        public void SaveQuest(string questName,
                                long trainingTime,
                                long testingTime,
                                float percentCorrectScore,
                                string confusionMatrixLog,
                                string responseTimesLog)
        {
            string dateStamp = DateTime.Now.ToString("MM-dd-yyyy-HH;mm;ss");

            //Save quest log data
            string fileQuest = Application.dataPath + "/" + Game.CurrentPlayer.Name + "/" + Game.CurrentPlayer.Name + "_" + questName + "_" + Game.CurrentPlayer.Name + ".qlog";
            string questDataLog = "" + percentCorrectScore + "," + trainingTime + "," + testingTime + "," + Game.CurrentPlayer.TotalTrainingTime + "," + Game.CurrentPlayer.TotalTestingTime;
            SaveLog(fileQuest, questDataLog);

            //Save confusion matrix data
            string fileMatrix = Application.dataPath + "/" + Game.CurrentPlayer.Name + "/" + Game.CurrentPlayer.Name + "_" + questName + "_" + dateStamp + "_matrix.csv";
            SaveLog(fileMatrix, confusionMatrixLog);

            //Save response times data
            string fileResponseTimes = Application.dataPath + "/" + Game.CurrentPlayer.Name + "/" + Game.CurrentPlayer.Name + "_" + questName + "_" + dateStamp + "_response_times.csv";
            SaveLog(fileResponseTimes, responseTimesLog);
        }

        //Save a quest log without a testing experiment
        public void SaveQuest(string questName,
                                long trainingTime,
                                long testingTime)
        {
            string dateStamp = DateTime.Now.ToString("MM-dd-yyyy-HH;mm;ss");

            //Save quest log data
            string fileQuest = Application.dataPath + "/" + Game.CurrentPlayer.Name + "/" + Game.CurrentPlayer.Name + "_" + questName + "_" + Game.CurrentPlayer.Name + ".qlog";
            string questDataLog = "" + 0 + "," + trainingTime + "," + testingTime + "," + Game.CurrentPlayer.TotalTrainingTime + "," + Game.CurrentPlayer.TotalTestingTime;
            SaveLog(fileQuest, questDataLog);
        }

        //Save a string log to a file
        private void SaveLog(string fileName, string log)
        {
            if (File.Exists(fileName))
                File.Delete(fileName);
            StreamWriter writer = File.CreateText(fileName);
            writer.Write(log);
            writer.Close();
        }

        


    }
}
