namespace WazeCredit.Service.LifeTimeExamples;

public class ScopedService
{
	public Guid guid;

	public ScopedService()
	{
		guid = Guid.NewGuid();
	}

	public string GetGuidAsString() => guid.ToString();
}
