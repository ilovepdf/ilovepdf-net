Simple usage looks like:
```csharp
var lovePdfAPi = new LovePdfApi("project_public_id", "project_secret_key");

var task = lovePdfAPi.CreateTask<CompressTask>();
var file = task.AddFile("file1.pdf")
var time = task.Process();
task.DownloadFile("dircetory-to-download");
```
