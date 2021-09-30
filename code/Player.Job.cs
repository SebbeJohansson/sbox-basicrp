using Sandbox;
using System;
using Jobs;
partial class BasicRPPlayer
{
	[Net]
	public Job Job { get; set; }

  RealTimeUntil TimeUntilSalary;

	public void  JobInit() {
    if (IsClient) return;
    if ( Game.Current is not BasicRPGame game ) return;

    JobData data = (JobData)game?.jobHandler.GetRandomJob();
    Job = new();
    Job.Name = data.Name;
    Job.Salary = data.Salary;
    TimeUntilSalary = 120.0f;
    CheckToGiveSalary();
	}

  [Event( "server.tick" )]
  private void CheckToGiveSalary(){
    if (IsClient) return;
    // TimeUntil has passed so lets give salary and recharge salary timer.
    if(TimeUntilSalary < 0){
      Money += Job.Salary;
      TimeUntilSalary = 120.0f;
    }
  }

}