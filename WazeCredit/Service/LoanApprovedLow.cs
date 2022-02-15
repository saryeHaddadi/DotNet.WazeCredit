using WazeCredit.Models;
using WazeCredit.Service.Interfaces;

namespace WazeCredit.Service;

public class LoanApprovedLow : ILoanApproved
{
	public double GetLoanApproved(LoanApplication loanApplication)
	{
		return loanApplication.Salary * 0.5;
	}
}
