using WazeCredit.Models;

namespace WazeCredit.Service.Interfaces;

public interface IValidationChecker
{
	bool ValidateLogic(LoanApplication model);
	string ErrorMessage { get; }

}
