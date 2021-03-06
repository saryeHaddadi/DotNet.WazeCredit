using WazeCredit.Models;
using WazeCredit.Service.Interfaces;

namespace WazeCredit.Service;

public class MarketForcaster : IMarketForcaster
{
	public MarketResult GetMarketPrediction()
	{
		return new MarketResult
		{
			MarketCondition = MarketCondition.StableUp
		};
	}
}
