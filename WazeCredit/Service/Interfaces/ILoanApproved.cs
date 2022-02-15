using WazeCredit.Models;

namespace WazeCredit.Service.Interfaces;

public interface ILoanApproved
{
	double GetLoanApproved(LoanApplication loanApplication);
}
