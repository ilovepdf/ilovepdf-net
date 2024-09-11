using iLovePdf.Model.Enums;
using iLovePdf.Model.TaskParams.Sign.Signers;

namespace iLovePdf.Model.TaskParams.Sign.Signers
{
    /// <inheritdoc cref="SignSignerType.Validator"/>
    public class Validator : BaseSignSigner
    {
        public Validator(string name, string email) : base(SignSignerType.Validator, name, email)
        {
        }
    }
}
