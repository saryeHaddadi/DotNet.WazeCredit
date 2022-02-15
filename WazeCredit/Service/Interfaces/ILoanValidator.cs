using System.Threading.Tasks;
using WazeCredit.Models;

namespace WazeCredit.Service.Interfaces;

public interface ILoanValidator
{
	Task<(bool, IEnumerable<string>)> PassAllValidations(LoanApplication model);
}
