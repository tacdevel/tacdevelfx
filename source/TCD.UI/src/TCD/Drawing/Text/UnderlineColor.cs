namespace TCD.Drawing.Text
{
    /// <summary>
    /// Identifies the underline color of text.
    /// </summary>
    public enum UnderlineColor : long
    {
        /// <summary>
        /// Custom color.
        /// </summary>
        Custom = 0,

        /// <summary>
        /// Spelling-error color.
        /// </summary>
        Spelling = 1,

        /// <summary>
        /// Grammar-error color.
        /// </summary>
        Grammar = 2,


        /// <summary>
        /// Auxillary color.
        /// </summary>
        Auxillary = 3
    }
}