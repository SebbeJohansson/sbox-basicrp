using System;
using Sandbox;

namespace Jobs {
  public struct JobData{
    public string Name {get;set;}
    public int Salary {get;set;}

    public bool Default {get;set;}

    public int Current {get;set;}
    public int Max {get; set;}

    public JobData(string name, int salary, bool defaultJob, int max){
      Name = name;
      Salary = salary;
      
      Default = defaultJob || false;

      Current = 0;
      Max = max;
    }
  }
  public partial class JobHandler : BaseNetworkable {

    [Net]
    public JobData[] AvailableJobs {get;set;}

    public JobHandler() {

      Log.Info("Jobhandler initatied");
      // Todo: Pull in available jobs from json
      AvailableJobs = FileSystem.Mounted.ReadJson<JobData[]>("jobs.json");
      Log.Info(AvailableJobs[0].ToString());


    }

    public JobData GetRandomJob() {
      Random rnd = new();
      int index = rnd.Next(AvailableJobs.Length);
      
      return AvailableJobs[index];

    }

  }
}