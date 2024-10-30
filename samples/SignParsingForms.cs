using iLovePdf.Core;
using iLovePdf.Model.Task;
using iLovePdf.Model.TaskParams;
using iLovePdf.Model.TaskParams.Sign.Elements;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Samples
{
    [SuppressMessage("ReSharper", "UnusedVariable")]
    public class SignParsingForms
    {
        public async Task DoTask()
        {
            var api = new iLovePdfApi("PUBLIC_KEY", "SECRET_KEY");

            //create compress task
            var task = api.CreateTask<SignTask>();
            
            //set pdfforms and pdfinfo params so response will return this values
            var uploadParams = new SignExtraUploadParams().SetPdfForms().SetPdfInfo();

            //file variable contains server file name
            var file = task.AddFile("path/to/file/document.pdf", uploadParams);

            file.GetPdfFormElement();

            var elements = new List<BaseSignElement>();
            
            foreach (var formElement in file.PdfForms)
            {
                var typeOfField = formElement["typeOfField"].ToString();

                if (new[] { "textbox", "signature" }.Contains(typeOfField))
                {
                    var fieldId = formElement["fieldId"].ToString();

                    var widgets = formElement["widgetsInformation"];
                    string widgetsString = JsonConvert.SerializeObject(widgets);
                    List<Dictionary<string, object>> widgetsList = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(widgetsString);

                    var position = widgetsList[0];
                    var currentPage = position["page"];

                    var leftPos = position["left"].ToString();
                    var topPosition = (ConvertToDouble(position["top"]) - ConvertToDouble(formElement["height"])).ToString();
                    var size = Math.Floor(ConvertToDouble(position["left"]) - ConvertToDouble(position["bottom"]));

                    if (typeOfField == "textbox")
                    {
                        if ((bool)formElement["multilineFlag"] || (bool)formElement["passwordFlag"])
                        {
                            return;
                        }

                        var textValue = formElement["textValue"].ToString();

                        if (fieldId.Contains("_input"))
                        {
                            var inputElement = new InputElement
                                (
                                    currentPage.ToString(),             //Pages
                                    new Position(leftPos, topPosition), //Position
                                    (int)size,                          //Size
                                    textValue                           //Label
                                );
                            elements.Add(inputElement);
                        }
                        else
                        {
                            var textElement = new TextElement
                            (
                                textValue,                          //TextContent
                                currentPage.ToString(),             //Pages
                                new Position(leftPos, topPosition), //Position
                                (int)size                           //Size
                            );
                            elements.Add(textElement);
                        }
                    }
                    else if (typeOfField == "signature")
                    {
                        var SignatureField = new SignatureElement
                            (
                                currentPage.ToString(),
                                new Position(leftPos, topPosition),
                                (int)size
                            );

                        elements.Add(SignatureField);
                    }
                }
            }

            // Create task params
            var signParams = new SignParams();

            // Create a signer
            var signer = signParams.AddSigner("Signer", "signer@email.com");

            // Add file that a receiver of type signer needs to sign.
            var signerFile = signer.AddFile(file.ServerFileName);

            // Add signers and their elements;
            var signatureElement = signerFile.AddSignature();
            signatureElement.Position = new Position("left", "bottom");
            signatureElement.Pages = "1";
            signatureElement.Size = 40;

            // Lastly send the signature request
            var signature = await task.RequestSignatureAsync(signParams);

        }

        public double ConvertToDouble(object keyObject)
        {
            return Convert.ToDouble(keyObject, CultureInfo.InvariantCulture);
        }
    }
}

