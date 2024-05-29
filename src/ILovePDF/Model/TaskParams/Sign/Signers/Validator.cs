using LovePdf.Model.Enums;
using LovePdf.Model.TaskParams.Sign.Signers;

namespace LovePdf.Model.TaskParams.Sign.Signers
{
    /// <inheritdoc cref="SignSignerType.Validator"/>
    public class Validator : BaseSignSigner
    {
        public Validator(string name, string email) : base(SignSignerType.Validator, name, email)
        {
        }
    }
}
