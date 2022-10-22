using LovePdf.Model.Enums;
using LovePdf.Model.TaskParams.Sign.Elements;
using LovePdf.Model.TaskParams.Sign.Signers;
using System;
using System.Collections.Generic;
using System.Text;

namespace LovePdf.Model.TaskParams.Sign.Receivers
{
    /// <inheritdoc cref="SignSignerType.Validator"/>
    public class Validator : BaseSignSigner
    {
        public Validator(string name, string email) : base(SignSignerType.Validator, name, email)
        {
        }
    }
}
