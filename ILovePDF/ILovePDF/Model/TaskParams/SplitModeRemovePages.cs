using System;

namespace LovePdf.Model.TaskParams
{
    /// <summary>
    ///     Split Mode Remove Pages
    /// </summary>
    public class SplitModeRemovePages
    {
        /// <summary>
        ///     Pages to remove from a PDF. Accepted format: 1,4,8-12,16.
        /// </summary>
        /// <param name="removePages">Accepted format: 1,4,8-12,16. </param>
        public SplitModeRemovePages(String removePages)
        {
            RemovePages = removePages;
        }

        /// <summary>
        ///     Remove Pages
        /// </summary>
        public String RemovePages { get; set; }
    }
}