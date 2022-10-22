using LovePdf.Model.Enums;
using LovePdf.Model.TaskParams.Sign.Elements;
using LovePdf.Model.TaskParams.Sign.Signers;
using System;
using System.Collections.Generic;
using System.Text;

namespace LovePdf.Model.TaskParams.Sign.Receivers
{
    /// <inheritdoc cref="SignSignerType.Viewer"/>
    public class Viewer : BaseSignSigner
    {
        public Viewer(string name, string email) : base(SignSignerType.Viewer,  name,  email)
        {
        }
    }
}
