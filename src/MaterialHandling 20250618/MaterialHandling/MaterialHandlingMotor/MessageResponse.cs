using Newtonsoft.Json;
using System.Collections.Generic;

// 这个类用来接收最外层的 JSON
public class RawApiResponse
{
    // Data 属性现在是 string 类型，以匹配响应
    [JsonProperty("data")]
    public string Data { get; set; }

    [JsonProperty("message")]
    public string Message { get; set; }

    [JsonProperty("status")]
    public string Status { get; set; }
}