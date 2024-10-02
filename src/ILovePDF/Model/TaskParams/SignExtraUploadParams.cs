using System;
using System.Collections.Generic;
using System.Text;

namespace iLovePdf.Model.TaskParams
{
    public class SignExtraUploadParams : BaseExtraUploadParams
    {
        public SignExtraUploadParams SetPdfInfo(bool activate = true)
        {
            extraParams["pdfinfo"] = activate ? "1" : "0";
            return this;
        }

        public SignExtraUploadParams SetPdfForms(bool activate = true)
        {
            SetPdfInfo(true);
            extraParams["pdfforms"] = activate ? "1" : "0";
            return this;
        }
    }
}
