using iLovePdf.Model.Enums;
using iLovePdf.Model.TaskParams.Sign.Signers;

namespace iLovePdf.Model.TaskParams.Sign.Signers
{
    /// <inheritdoc cref="SignSignerType.Viewer"/>
    public class Viewer : BaseSignSigner
    {
        public Viewer(string name, string email) : base(SignSignerType.Viewer, name, email)
        {
        }
    }
}
