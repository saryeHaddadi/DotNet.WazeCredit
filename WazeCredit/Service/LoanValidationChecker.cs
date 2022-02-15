using WazeCredit.Models;
using WazeCredit.Service.Interfaces;

namespace WazeCredit.Service;

public class LoanValidationChecker : IValidationChecker
{
	public string ErrorMessage => "You did not meet Age/Salary/Loan requirements.";

	public bool ValidateLogic(LoanApplication model)
	{
		if(DateTime.Now.AddYears(-18) < model.DOB)
		{
			return false;
		}
		if(model.Salary < 10000)
		{
			return false;
		}
		return true;
	}
}
