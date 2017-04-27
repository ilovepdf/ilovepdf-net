Simple usage looks like:

var lovePdfAPi = new LovePdfApi("project_public_id", "project_secret_key");

var task = lovePdfAPi.CreateTask<CompressTask>();
var file = task.AddFile("file1.pdf")
taskResult = task.Process();
task.DownloadFile("dircetory-to-download");