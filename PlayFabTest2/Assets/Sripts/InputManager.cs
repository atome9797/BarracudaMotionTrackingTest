using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using TMPro;
using PlayFab.AdminModels;
using PlayFab.Json;

[SerializeField]
public class testData
{
    public int test_id;
    public string test_name;
}


public class InputManager : MonoBehaviour
{
    public TMP_InputField Input1, Input2, Input3;
    public Text UserName;
    public static List<string> FeedLikeList = new List<string>();

    void Start()
    {
        GetUserinfo();
    }

    //해당 데이터 불러오기
    public static void getLikeList()
    {
        var request = new ExecuteCloudScriptRequest
        {
            FunctionName = "GetUserInternalDataTest"
        };

        PlayFabClientAPI.ExecuteCloudScript(request, OngetLikeListSuccess, OngetLikeListError);
    }

    public static void OngetLikeListSuccess(PlayFab.ClientModels.ExecuteCloudScriptResult result)
    {
        if (result.FunctionResult.ToString() != "ResponseFail")
        {
            JsonArray jsonArray = new JsonArray();
            jsonArray = (JsonArray)result.FunctionResult;
            for (int i = 0; i < jsonArray.Count; i++)
            {
                Debug.Log(jsonArray[i].ToString());
            }
        }else
        {
            Debug.Log("데이터 못불러옴");
        }


    }


    public static void OngetLikeListError(PlayFabError error)
    {
        Debug.Log("초기화 실패");
    }


    //클라우드 스크립트 테스트
    public static void setTestData()
    {
        var request = new ExecuteCloudScriptRequest
        {
            FunctionName = "setTestData"
        };

        PlayFabClientAPI.ExecuteCloudScript(request, OntestDataSuccess, OntestDataError);
    }

    public static void OntestDataSuccess(PlayFab.ClientModels.ExecuteCloudScriptResult result)
    {
        Debug.Log(result.ToString());

    }

    /// <summary>
    /// 비디오 데이터 초기화 실패시 콜백함수
    /// </summary>
    /// <param name="error"></param>
    public static void OntestDataError(PlayFabError error)
    {
        Debug.Log("초기화 실패");

    }

/*    public static void UpdateTestData()
    {
        var request = new ExecuteCloudScriptRequest
        {
            FunctionName = "UpdateUserInternalDataTest"
        };

        PlayFabClientAPI.ExecuteCloudScript(request, OnUpdateTestDataSuccess, OnUpdateTestDataError);
    }

    public static void OnUpdateTestDataSuccess(PlayFab.ClientModels.ExecuteCloudScriptResult result)
    {
        Debug.Log(result.ToString());

    }*/




    public static void getTestData()
    {
        var request = new ExecuteCloudScriptRequest
        {
            FunctionName = "UserLikeData"
        };

        PlayFabClientAPI.ExecuteCloudScript(request, OngetTestDataSuccess2, OngetTestDataError2);
    }

    public static void OngetTestDataSuccess2(PlayFab.ClientModels.ExecuteCloudScriptResult result)
    {
        if (result.FunctionResult.ToString() != "ResponseFail")
        {
            JsonObject jsonObject = (JsonObject)result.FunctionResult;

            foreach (var eachData in jsonObject)
            {
                FeedLikeList.Add(eachData.Key);
            }
        }
        else
        {
            Debug.Log("데이터 못불러옴");
        }

    }

    public static void OngetTestDataError2(PlayFabError error)
    {
        Debug.Log("초기화 실패");

    }


    /// <summary>
    /// 비디오 데이터 초기화 실패시 콜백함수
    /// </summary>
    /// <param name="error"></param>
    public static void OngetTestDataError(PlayFabError error)
    {
        Debug.Log("초기화 실패");

    }

    public void DeleteTestData()
    {
        /*        var request = new SetTitleDataAndOverridesRequest();
                request.OverrideLabel = "post";
                var TitleDataKeyValueList = new List<TitleDataKeyValue>();
                var TitleDataKeyValue = new TitleDataKeyValue();
                TitleDataKeyValue.Key = "789798";
                TitleDataKeyValue.Value = null;
                TitleDataKeyValueList.Add(TitleDataKeyValue);
                request.KeyValues = TitleDataKeyValueList;

                PlayFabAdminAPI.SetTitleDataAndOverrides(request, OnGetsetTestData2Success, OnGetsetTestData2Failure);*/

        var TitleDataKeyValueList = new List<TitleDataKeyValue>();
        var TitleDataKeyValue = new TitleDataKeyValue();
        TitleDataKeyValue.Key = "761706272";
        TitleDataKeyValue.Value = null;
        TitleDataKeyValueList.Add(TitleDataKeyValue);

        var request = new SetTitleDataAndOverridesRequest()
        {
            OverrideLabel = "post",
            KeyValues = TitleDataKeyValueList
        };

        PlayFabAdminAPI.SetTitleDataAndOverrides(request, OnGetsetTestData2Success, OnGetsetTestData2Failure);
    }


    public void setTestData2()
    {

        testData _testData = new testData();
        _testData.test_id = 10;
        _testData.test_name = "김영훈";

        string json = JsonUtility.ToJson(_testData);

        var request = new SetTitleDataAndOverridesRequest();
        request.OverrideLabel = "post";
        var TitleDataKeyValueList = new List<TitleDataKeyValue>();
        var TitleDataKeyValue = new TitleDataKeyValue();
        TitleDataKeyValue.Key = "7897990";
        TitleDataKeyValue.Value = json;
        TitleDataKeyValueList.Add(TitleDataKeyValue);
        request.KeyValues = TitleDataKeyValueList;

        PlayFabAdminAPI.SetTitleDataAndOverrides(request, OnGetsetTestData2Success, OnGetsetTestData2Failure);
    }

    void OnGetsetTestData2Success(SetTitleDataAndOverridesResult result)
    {
        Debug.Log(result);
    }

    void OnGetsetTestData2Failure(PlayFabError error)
    {
        print("실패");
    }

    public void getTestData2()
    {
        var request = new PlayFab.ClientModels.GetTitleDataRequest() { OverrideLabel = "post"};
        PlayFabClientAPI.GetTitleData(request, OnGetTestData2Success, OnGetTestData2Failure);
    }

    void OnGetTestData2Success(PlayFab.ClientModels.GetTitleDataResult result)
    {
        Debug.Log(result);
    }

    void OnGetTestData2Failure(PlayFabError error)
    {
        print("실패");
    }



    //사용자 데이터 세팅
    public void SetStat()
    {
        var request = new PlayFab.ClientModels.UpdateUserDataRequest() { Data = new Dictionary<string, string>() { { "A", "AA" }, { "B", "BB" } } };
        PlayFabClientAPI.UpdateUserData(request, (result) => print("데이터 저장 성공" ), (error) => print("데이터 저장 실패"));
    }


    public void GetStat()
    {
        var request = new PlayFab.ClientModels.GetTitleDataRequest();
        PlayFabClientAPI.GetTitleData(request, (result) => {
            foreach (var eachData in result.Data)
                print(eachData.Key);
        }, (error) => print("데이터 불러오기 실패"));
    }

    //유저 정보 가져오기
    void GetUserinfo()
    {
        PlayFabClientAPI.GetAccountInfo(new GetAccountInfoRequest(), OnGetUserInfoSuccess, OnGetUserInfoFailure);
    }

    void OnGetUserInfoSuccess(GetAccountInfoResult result)
    {
        UserName.text = $" User : {result.AccountInfo.Username}";
        print("유저 정보 불러오기 성공");
    }

    void OnGetUserInfoFailure(PlayFabError error)
    {
        print("유저 정보 불러오기 실패");
    }

    

}
