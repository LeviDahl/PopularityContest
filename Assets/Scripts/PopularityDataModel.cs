using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PopularityDataModel  
{
   public int friendId;
   public string friendName;
   
   public PopularityDataModel(Dictionary<string, object> dict)
    {
        friendId = Convert.ToInt32(dict["id"]);
        friendName = (string)dict["name"];
        
    }
    public class FBPostModel
    {
        public string id;
        public string message;
        public List<string> likeUserIds;
        public int totalLikes;
        public DateTime created_date;
        public FBPostModel(Dictionary<string, object> dict)
        {
            likeUserIds = new List<string>();
            id = (string)dict["id"];
            Dictionary<string, object> tempLikeDictionary = (Dictionary<string,object>)dict["likes"];
            List<object> tempSingleId = (List<object>)tempLikeDictionary["data"];
            foreach (Dictionary<string, object>  tempidDict in tempSingleId)
            {
                likeUserIds.Add((string)tempidDict["id"]);
            }
            Dictionary<string, object> tempSummary = (Dictionary<string, object>)tempLikeDictionary["summary"];
            totalLikes = (Convert.ToInt32(tempSummary["total_count"]));
            Debug.Log("Total Count of Likes:"+totalLikes.ToString());
            if (dict.ContainsKey("message"))
            {
                message = (string)dict["message"];
                Debug.Log(message);
            }
            created_date = DateTime.Parse((string)dict["created_time"]);
            Debug.Log(created_date.ToString());
        }
        
    }
}

