using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class FBProgressPanel : MonoBehaviour {
    public GameObject progressBarPrefab;
    public GameObject gridGameObject;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void SetupProgressPanels(List<PopularityDataModel.FBPostModel> posts)
    {
        int maxMessageLength = 90;
        foreach(PopularityDataModel.FBPostModel post in posts)
        {
            GameObject g = GameObject.Instantiate(progressBarPrefab);
            LikeCountProgressBar progressBarScript = g.GetComponent<LikeCountProgressBar>();
            g.transform.SetParent(gridGameObject.transform);
            progressBarScript.likeCount.text = "Likes: " + post.totalLikes.ToString();
            progressBarScript.progressBar.fillAmount = ((float)post.totalLikes/(float)posts[0].totalLikes);
            progressBarScript.postDate.text = post.created_date.ToString();
            if (post.message != null)
            {
                string message = post.message;
                
                if (message.Length > maxMessageLength) 
                    message = message.Substring(0, maxMessageLength)+"...";
    
                progressBarScript.postMessage.text = message;
               
            }
        }
    }
}
