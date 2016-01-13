using UnityEngine;
using System.Collections;
using Facebook.Unity;
using System.Collections.Generic;
using System.Linq;

public class FacebookMain : MonoBehaviour {
   
 List<string> perms = null;
 public GameObject FBUIPrefab;
 GameObject FBUIInstance;
 List<PopularityDataModel.FBPostModel> posts = new List<PopularityDataModel.FBPostModel>();
 bool hasDoneLogin = false;
	void Start () 
    {
        if (!FB.IsInitialized) 
        {
            FB.Init(OnFBInitComplete, OnHideUnity);
        } 
        else 
        {
            FB.ActivateApp();
            StartFBLogin();
        }
	}
	void Update () {
	
	}
    void OnFBInitComplete()
    {
        if (FB.IsInitialized) 
        {
            Debug.Log("Activating App");
            FB.ActivateApp();
        } 
        else 
        {
            Debug.Log("Failed to Initialize the Facebook SDK");
        }
    }
    void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown) 
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
    void StartFBLogin()
    {
       perms = new List<string>(){"public_profile", "email", "user_friends", "user_posts"};
       FB.LogInWithReadPermissions(perms, AuthCallback);
    }
    private void AuthCallback (ILoginResult result) 
    {
        if (FB.IsLoggedIn) 
        {
            AccessToken aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
            FBUIInstance = GameObject.Instantiate(FBUIPrefab, Vector3.zero, Quaternion.identity) as GameObject;
            FBUIInstance.transform.SetParent(MainUIAccessor.Instance.gameObject.transform, false);
            FBUIInstance.SetActive(true);
            GetFriendsList();
        } 
        else 
        {
            Debug.Log("Error: " + result.Error);
        }
    }
    void GetFriendsList()
    {
        Dictionary<string, string> nullDict = null;
        FB.API("me/posts?fields=likes.summary(true),message,created_time", HttpMethod.GET, OnGetFriendsList, nullDict);
      
    }
    void OnGetFriendsList(IGraphResult result)
    {
        List<object> l = (List<object>)result.ResultDictionary["data"];
        posts.Clear();
        for (int i = 0; i < l.Count; i++) 
        {
            posts.Add(new PopularityDataModel.FBPostModel((Dictionary<string, object>) l[i]));
        }
        posts.Sort((x, y) => y.totalLikes.CompareTo(x.totalLikes));
        FBUIInstance.GetComponent<FBProgressPanel>().SetupProgressPanels(posts);
    }
    void OnGUI()
    {
        if (!FB.IsLoggedIn && FB.IsInitialized && !hasDoneLogin)
        {
             Debug.Log("Attempting Login");
             hasDoneLogin = true;
             StartFBLogin();
        }
    }
}
