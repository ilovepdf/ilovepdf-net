using System;
using System.Collections.Generic;

namespace iLovePdf.Core
{
    /// <summary>
    ///     Base class for requests.
    /// </summary>
    public class BaseTaskRequest
    {
        /// <summary>
        ///     Initialize properties
        /// </summary>
        public BaseTaskRequest()
        {
            initFields();
        }

        /// <summary>
        ///     Upload form data to the server
        /// </summary>
        public Dictionary<String, Object> FormData { get; private set; }

        private void initFields()
        {
            FormData = new Dictionary<String, Object>();
        }
    }
}