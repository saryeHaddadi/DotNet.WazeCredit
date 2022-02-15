using WazeCredit.Models;
using WazeCredit.Service.Interfaces;

namespace WazeCredit.Service;

public class LoanApprovedHigh : ILoanApproved
{
	public double GetLoanApproved(LoanApplication loanApplication)
	{
		return loanApplication.Salary * 0.3;
	}
}
