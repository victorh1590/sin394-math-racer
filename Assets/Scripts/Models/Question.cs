using System.Collections.Generic;
using Newtonsoft.Json;

namespace GameModels
{
  // public class Question
  // {
  //     [JsonProperty("Statement")]
  //     public string Statement { get; set; }

  //     [JsonProperty("Answers")]
  //     public List<string> Answers { get; set; }

  //     [JsonProperty("CorrectAnswer")]
  //     public string CorrectAnswer { get; set; }

  //     [JsonProperty("Tip")]
  //     public string Tip { get; set; }
  // }

  public class Question
  {
    public string Statement { get; set; }
    public List<string> Answers { get; set; }
    public string CorrectAnswer { get; set; }
    public string Tip { get; set; }
  }
}
