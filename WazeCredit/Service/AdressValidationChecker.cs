using WazeCredit.Models;
using WazeCredit.Service.Interfaces;

namespace WazeCredit.Service;

public class AdressValidationChecker : IValidationChecker
{
	public string ErrorMessage => "Address validation failed";

	public bool ValidationLogic(LoanApplication model)
	{
		if(model.PostalCode <= 0 || model.PostalCode > 100000)
		{
			return false;
		}
		return true;
	}
}
