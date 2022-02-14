using WazeCredit.Models;
using WazeCredit.Service.Interfaces;

namespace WazeCredit.Service;

public class MarketForcasterV2 : IMarketForcaster
{
	public MarketResult GetMarketPrediction()
	{
		return new MarketResult
		{
			MarketCondition = MarketCondition.StableDown
		};
	}
}
