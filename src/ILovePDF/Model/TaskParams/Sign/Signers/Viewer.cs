using LovePdf.Model.Enums;
using LovePdf.Model.TaskParams.Sign.Signers;

namespace LovePdf.Model.TaskParams.Sign.Signers
{
    /// <inheritdoc cref="SignSignerType.Viewer"/>
    public class Viewer : BaseSignSigner
    {
        public Viewer(string name, string email) : base(SignSignerType.Viewer, name, email)
        {
        }
    }
}
