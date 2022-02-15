using WazeCredit.Models;
using WazeCredit.Service.Interfaces;

namespace WazeCredit.Service;

public class LoanValidator : ILoanValidator
{
	private readonly IEnumerable<IValidationChecker> _validationCheckers;

	public LoanValidator(IEnumerable<IValidationChecker> validationCheckers)
	{
		_validationCheckers = validationCheckers;
	}

	public async Task<(bool, IEnumerable<string>)> PassAllValidations(LoanApplication model)
	{
		var validationPassed = true;
		var errorMessages = new List<string>();

		foreach (var checker in _validationCheckers)
		{
			if (!checker.ValidateLogic(model))
			{
				errorMessages.Add(checker.ErrorMessage);
				validationPassed = false;
			}
		}

		return (validationPassed, errorMessages);
	}
}
