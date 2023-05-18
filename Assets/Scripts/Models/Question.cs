using System.Collections.Generic;
using Newtonsoft.Json;

namespace GameModels
{
  public class Question
  {
    public string Statement { get; set; }
    public List<string> Answers { get; set; }
    public string CorrectAnswer { get; set; }
    public string Tip { get; set; }
  }
}
