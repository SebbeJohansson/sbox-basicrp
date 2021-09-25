using Sandbox;
using System;
partial class BasicRPPlayer
{
	[Net]
	public int Salary { get; set; }

  RealTimeUntil TimeUntilSalary;

	public void SalaryInit() {
    if (IsClient) return;
		Salary = 60;
    TimeUntilSalary = 120.0f;
    CheckToGiveSalary();
	}

  [Event( "server.tick" )]
  private void CheckToGiveSalary(){
    // TimeUntil has passed so lets give salary and recharge salary timer.
    if(TimeUntilSalary < 0){
      Money += Salary;
      TimeUntilSalary = 120.0f;
    }
  }

}