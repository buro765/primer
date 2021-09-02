using DocumentFormat.OpenXml.Office.CustomUI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

[Serializable]
public class RootObject
{
    
    [Newtonsoft.Json.JsonProperty("data")]
    public Class1[] data { get; set; }
    [Newtonsoft.Json.JsonProperty("error")]
    public Object error { get; set; }
    [Newtonsoft.Json.JsonProperty("metadata")]
    public Object metadata { get; set; }

}

[Serializable]
public class Class1
{
    [Newtonsoft.Json.JsonProperty("sys_key")]
    public string sys_key { get; set; }
    [Newtonsoft.Json.JsonProperty("jjywjc")]
    public string jjywjc { get; set; }
    [Newtonsoft.Json.JsonProperty("ssrq")]
    public string ssrq { get; set; }
    [Newtonsoft.Json.JsonProperty("dqgm")]
    public string dqgm { get; set; }
}
