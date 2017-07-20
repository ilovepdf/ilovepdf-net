namespace LovePdf.Model.TaskParams
{
    /// <summary>
    /// Split Mode Remove Pages
    /// </summary>
    public class SplitModeRemovePages
    {
        /// <summary>
        /// Remove Pages
        /// </summary>
        public string RemovePages { get; set; }

        /// <summary>
        /// Pages to remove from a PDF. Accepted format: 1,4,8-12,16. 
        /// </summary>
        /// <param name="removePages">Accepted format: 1,4,8-12,16. </param>
        public SplitModeRemovePages(string removePages)
        {
            RemovePages = removePages;
        }
    }
}