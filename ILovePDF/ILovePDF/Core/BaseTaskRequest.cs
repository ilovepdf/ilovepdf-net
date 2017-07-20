using System.Collections.Generic;

namespace LovePdf.Core
{
    /// <summary>
    /// Base class for requests.
    /// </summary>
    public class BaseTaskRequest
    {

        /// <summary>
        ///Upload form data to the server
        /// </summary>
        public Dictionary<string, object> FormData { get; private set; }

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
