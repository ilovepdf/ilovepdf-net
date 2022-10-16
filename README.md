![iLovePDF](https://www.ilovepdf.com/img/ilovepdf.svg)

[![CI](https://ci.appveyor.com/api/projects/status/ous9pmh1qc3d6q1t?svg=true)](https://github.com/ilovepdf/ilovepdf-net/actions)
[![NuGet](https://img.shields.io/nuget/vpre/ILove_PDF.svg?label=NuGet)](https://www.nuget.org/packages/ILove_PDF/)
[![ILove_PDF_test on fuget.org](https://www.fuget.org/packages/ILove_PDF/badge.svg)](https://www.fuget.org/packages/ILove_PDF)
![GitHub](https://img.shields.io/github/license/ilovepdf/ilovepdf-net)

## iLovePDF Api - C# Library
A library in C# for iLovePDF Api
You can sign up for a iLovePDF account at https://developer.ilovepdf.com

Develop and automate PDF processing tasks like Compress PDF, Merge PDF, Split PDF, convert Office to PDF, PDF to JPG, Images to PDF, add Page Numbers, Rotate PDF, Unlock PDF, stamp a Watermark and Repair PDF. Each one with several settings to get your desired results.

## Requirements
Minimum .NET Framework 4.5

## Install
You can install library from [Nuget](https://www.nuget.org/packages/ILove_PDF/)
```
PM-> Install-Package ILove_PDF
```
### Simple usage looks like:
```csharp
var lovePdfAPi = new LovePdfApi("project_public_id", "project_secret_key");

var task = lovePdfAPi.CreateTask<CompressTask>();
var file = task.AddFile("file1.pdf")
var time = task.Process();
task.DownloadFile("directory-to-download");
```
## Documentation
Please see https://developer.ilovepdf.com/docs for up-to-date documentation.

## License
The code is available as open source under the terms of the [MIT License](http://opensource.org/licenses/MIT).
