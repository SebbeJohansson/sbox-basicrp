using Sandbox;

namespace Jobs {
  public partial class Job : BaseNetworkable {

    [Net]
    public string Name{ get; set; }

    [Net]
    public int Salary{ get; set; }

    public Job() {
    }
  }
}