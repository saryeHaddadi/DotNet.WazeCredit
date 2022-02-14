using WazeCredit.Models;

namespace WazeCredit.Service;

public class MarketForcaster
{
	public MarketResult GetMarketPrediction()
	{
		return new MarketResult
		{
			MarketCondition = MarketCondition.StableUp
		};
	}
}
