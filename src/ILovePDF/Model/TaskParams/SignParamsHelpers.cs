using LovePdf.Core.Sign;
using LovePdf.Model.Enums;
using LovePdf.Model.TaskParams.Sign.Elements;
using LovePdf.Model.TaskParams.Sign.Signers;

namespace LovePdf.Model.TaskParams
{
    /// <summary>
    ///  
    /// </summary>
    public partial class SignParams
    {
        /// <summary> 
        /// Use one of this signer types: 
        /// <list type="bullet"> 
        /// <item> <see cref="SignSignerType.Signer" /></item> 
        /// <item><see cref="SignSignerType.Validator" /></item> 
        /// <item><see cref="SignSignerType.Viewer" /></item>  
        /// </list>
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public ISignSigner AddSigner(ISignSigner element)
        {
            Signers.Add(element);
            return element;
        }

        /// <inheritdoc cref="SignSignerType.Signer"/>
        /// <param name="name">Receiver full name.</param>
        /// <param name="email">Receiver email.</param>
        public Signer AddSigner(string name, string email)
        {
            var signer = new Signer(name, email);
            Signers.Add(signer);
            return signer;
        }

        /// <inheritdoc cref="SignSignerType.Validator"/>
        /// <param name="name">Receiver full name.</param>
        /// <param name="email">Receiver email.</param>
        public Validator AddValidator(string name, string email)
        {
            var signer = new Validator(name, email);
            Signers.Add(signer);
            return signer;
        }

        /// <inheritdoc cref="SignSignerType.Viewer"/>
        /// <param name="name">Receiver full name.</param>
        /// <param name="email">Receiver email.</param>
        public Viewer AddViewer(string name, string email)
        {
            var signer = new Viewer(name, email);
            Signers.Add(signer);
            return signer;
        }

        public void ClearSigners()
        {
            Signers.Clear();
        }
    }
}