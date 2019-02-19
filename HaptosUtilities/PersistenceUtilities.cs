using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace HaptosUtilities
{
    public static class PersistenceUtilities
    {

        //Creates a folder for a player
        public static bool CreateFolderForPlayer(string playerName)
        {
            if(!Directory.Exists(Application.dataPath + "/" + playerName))
            {
                Directory.CreateDirectory(Application.dataPath + "/" + playerName);
                return true;
            }
            return false;
        }

        //Load a character
        public static Player LoadPlayer(string name)
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
        public static void SaveCurrentPlayer()
        {
            if (!Game.SuperPlayer)
            {
                string filePath = Application.dataPath + "/" + Game.CurrentPlayer.Name + "/" + Game.CurrentPlayer.Name + ".gd";
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Create(filePath);
                bf.Serialize(file, Game.CurrentPlayer);
                file.Close();
            }
        }

        //Get the list of saved games
        public static List<string> GetSavedGames()
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
        public static void DeleteSavedGame(string playerName)
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
        public static void SaveQuest(string questName,
                                float trainingTime,
                                float testingTime,
                                float percentCorrectScore,
                                string confusionMatrixLog,
                                string responseLog)
        {
            //Do not do anything if the superplayer is active
            if (Game.SuperPlayer)
                return;

            string dateStamp = DateTime.Now.ToString("MM-dd-yyyy-HH;mm;ss");

            //Save quest log data
            string fileQuest = Application.dataPath + "/" + Game.CurrentPlayer.Name + "/" + Game.CurrentPlayer.Name + "_" + questName + "_" +dateStamp+ "_qlog.csv";
            string questDataLog = "PercentCorrect,TrainingTime(min),TestingTime(min),TotalTrainingTime(min),TotalTestingTime(min)\n";
            questDataLog += "" + percentCorrectScore + "," + trainingTime + "," + testingTime + "," + Game.CurrentPlayer.TotalTrainingTime + "," + Game.CurrentPlayer.TotalTestingTime;
            SaveLog(fileQuest, questDataLog);

            //Save confusion matrix data
            string fileMatrix = Application.dataPath + "/" + Game.CurrentPlayer.Name + "/" + Game.CurrentPlayer.Name + "_" + questName + "_" + dateStamp + "_matrix.csv";
            SaveLog(fileMatrix, confusionMatrixLog);

            //Save response data
            string fileResponse = Application.dataPath + "/" + Game.CurrentPlayer.Name + "/" + Game.CurrentPlayer.Name + "_" + questName + "_" + dateStamp + "_responses.csv";
            SaveLog(fileResponse, responseLog);
        }

        //Save a quest log without a testing experiment
        public static void SaveQuest(string questName,
                                float trainingTime,
                                float testingTime)
        {
            //Do not do anything if the superplayer is active
            if (Game.SuperPlayer)
                return;

            string dateStamp = DateTime.Now.ToString("MM-dd-yyyy-HH;mm;ss");

            //Save quest log data
            string fileQuest = Application.dataPath + "/" + Game.CurrentPlayer.Name + "/" + Game.CurrentPlayer.Name + "_" + questName + "_" + dateStamp + "_qlog.csv";
            string questDataLog = "PercentCorrect,TrainingTime(min),TestingTime(min),TotalTrainingTime(min),TotalTestingTime(min)\n";
            questDataLog += "" + 0 + "," + trainingTime + "," + testingTime + "," + Game.CurrentPlayer.TotalTrainingTime + "," + Game.CurrentPlayer.TotalTestingTime;
            SaveLog(fileQuest, questDataLog);
        }

        //Save a string log to a file
        private static void SaveLog(string fileName, string log)
        {
            if (File.Exists(fileName))
                File.Delete(fileName);
            StreamWriter writer = File.CreateText(fileName);
            writer.Write(log);
            writer.Close();
        }

        //Save a general log file to the datapath as a csv file
        public static void SaveGeneralLog(string fileName, string log)
        {
            //Do not do anything if the superplayer is active
            if (Game.SuperPlayer)
                return;

            string dateStamp = DateTime.Now.ToString("MM-dd-yyyy-HH;mm;ss");
            string filePath = Application.dataPath + "/" + Game.CurrentPlayer.Name + "/" + Game.CurrentPlayer.Name + "_" + fileName + "_"+dateStamp+"_general.csv";
            if (File.Exists(filePath))
                File.Delete(filePath);
            StreamWriter writer = File.CreateText(filePath);
            writer.Write(log);
            writer.Close();
        }

        //Save a general log file to the datapath assuming the datestamp is included and the extension is provided
        public static void SaveGeneralLogWithDate(string fileName, string log)
        {
            //Do not do anything if the superplayer is active
            if (Game.SuperPlayer)
                return;

            string filePath = Application.dataPath + "/" + Game.CurrentPlayer.Name + "/" + Game.CurrentPlayer.Name + "_" + fileName;
            if (File.Exists(filePath))
                File.Delete(filePath);
            StreamWriter writer = File.CreateText(filePath);
            writer.Write(log);
            writer.Close();
        }

        //Searchs for a file with the name
        public static string FindFullPathOfFile(string fileName)
        {
            string path = Application.dataPath + "/" + Game.CurrentPlayer.Name + "/" + Game.CurrentPlayer.Name + "_" + fileName;
            return File.Exists(path) ? path : null;
        }

        //Append text to a file
        public static bool AppendToFile(string path, string text)
        {
            if (File.Exists(path))
            {
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine(text);
                }
                return true;
            }
            return false;
        }


    }
}
