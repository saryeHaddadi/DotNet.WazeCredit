namespace WazeCredit.Service.LifeTimeExamples;

public class TransientService
{
	public Guid guid;

	public TransientService()
	{
		guid = Guid.NewGuid();
	}

	public string GetGuidAsString() => guid.ToString();
}
