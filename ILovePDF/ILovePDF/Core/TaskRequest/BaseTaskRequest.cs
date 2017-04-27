using System.Collections.Generic;
namespace ILovePDF.Core.TaskRequest
{
    /// <summary>
    /// Base class for requests.
    /// </summary>
    public  class BaseTaskRequest
    {
     
        /// <summary>
        ///Upload form data to the server
        /// </summary>
        public Dictionary<string, object> FormData { get; set; }

        /// <summary>
        /// Initialize properties
        /// </summary>
        public BaseTaskRequest()
        {
            InitFields();
        }

        private void InitFields()
        {
            FormData = new Dictionary<string, object>();
        }
    }
}
