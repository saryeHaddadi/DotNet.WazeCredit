using WazeCredit.Models;
using WazeCredit.Service.Interfaces;

namespace WazeCredit.Service;

public class AddressValidationChecker : IValidationChecker
{
	public string ErrorMessage => "Address validation failed";

	public bool ValidateLogic(LoanApplication model)
	{
		if(model.PostalCode <= 0 || model.PostalCode > 100000)
		{
			return false;
		}
		return true;
	}
}
