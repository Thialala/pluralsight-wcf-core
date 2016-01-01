using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace GeoLib.Client.Contracts
{
    /// <summary>
    /// Added namespace matching in AssemblyInfo.cs
    /// Maybe useless in 4.5
    /// </summary>
    [ServiceContract]
    public interface IMessageService
    {
        [OperationContract(Name = "ShowMessage")]
        void ShowMsg(string message);
    }
}
