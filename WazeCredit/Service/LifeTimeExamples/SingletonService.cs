namespace WazeCredit.Service.LifeTimeExamples;

public class SingletonService
{
	public Guid guid;

	public SingletonService()
	{
		guid = Guid.NewGuid();
	}

	public string GetGuidAsString() => guid.ToString();
}
