/****************************************************************************
 * FileName:   CloseEventData.cs
 * Assembly:   TCD.UI.dll
 * Package:    TCD.UI
 * Date:       20180921
 * License:    MIT License
 * LicenseUrl: https://github.com/tacdevel/TDCFx/blob/master/LICENSE.md
 ***************************************************************************/

namespace TCD.UI
{
    /// <summary>
    /// Provides data for a closing event.
    /// </summary>
    public class CloseEventData : EventData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CloseEventData"/> with the specified event data.
        /// </summary>
        /// <param name="close"><see langword="true"/> to close; <see langword="false"/> to cancel.</param>
        public CloseEventData(bool close = true) => Close = close;

        /// <summary>
        /// Gets a value determining whether to close or not.
        /// </summary>
        public bool Close { get; }
    }
}