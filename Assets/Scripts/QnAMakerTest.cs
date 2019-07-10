using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using LitJson;
using UnityEngine.UI;
using Newtonsoft.Json;

public class QnAMakerTest : MonoBehaviour
{

 
     string host = "https://qnatest.azurewebsites.net/qnamaker";

     string endpoint_key = "ca10d722-7585-4b88-8591-9d2c350943e4";
    
    string Route = "/knowledgebases/d398de1e-f847-4c66-93ca-442cb9bef413/generateAnswer";

     string question = @"
{
    'question': '吃不吃西瓜',
    'top': 3
}
";
    public string GetQuestion(string str,int top)
    {
        string ques = "{\'question\':'"+str+"\'" +","+
            "\'top\':'"+top+"\'}";
        Debug.Log(ques);
        return ques;
    }
    

    public  string Post( string body)
    {
        string final = "";
        using (var client = new HttpClient())
        using (var request = new HttpRequestMessage())
        {
            request.Method = HttpMethod.Post;
            request.RequestUri = new Uri(host + Route);
            request.Content = new StringContent(body, Encoding.UTF8, "application/json");
            request.Headers.Add("Authorization", "EndpointKey " + endpoint_key);

            var response = client.SendAsync(request).Result;
            var jsonResponse = response.Content.ReadAsStringAsync().Result;
            Debug.Log(jsonResponse);

            JsonData jd = JsonMapper.ToObject(jsonResponse);
            Debug.Log(jd["answers"][0]["answer"]);
            final = (string) jd["answers"][0]["answer"];
        }
        return final;
    }
    
}

