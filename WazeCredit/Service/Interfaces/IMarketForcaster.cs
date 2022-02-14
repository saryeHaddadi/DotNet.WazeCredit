using WazeCredit.Models;

namespace WazeCredit.Service.Interfaces;

public interface IMarketForcaster
{
	MarketResult GetMarketPrediction();
}