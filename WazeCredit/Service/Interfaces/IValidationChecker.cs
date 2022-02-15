using WazeCredit.Models;

namespace WazeCredit.Service.Interfaces;

public interface IValidationChecker
{
	bool ValidationLogic(LoanApplication model);
	string ErrorMessage { get; }

}
