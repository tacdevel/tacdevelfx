
using TCD.Native;

namespace TCD.Drawing.Text
{
    public static class ContextExtensions
    {
        /// <summary>
        /// Draws a <see cref="TextLayout"/> at the given location in this <see cref="Context"/>.
        /// </summary>
        /// <param name="layout">The text to draw.</param>
        /// <param name="x">The x-coordinate at which to draw the text.</param>
        /// <param name="y">The y-coordinate at which to draw the text.</param>
        public static void DrawText(this Context self, TextLayout layout, double x, double y) => Libui.Call<Libui.uiDrawText>()(self.Surface.Handle, layout, x, y);

        /// <summary>
        /// Draws a <see cref="TextLayout"/> at the given location in this <see cref="Context"/>.
        /// </summary>
        /// <param name="layout">The text to draw.</param>
        /// <param name="location">The location at which to draw the text.</param>
        public static void DrawText(this Context self, TextLayout layout, PointD location) => DrawText(self, layout, location.X, location.Y);
    }
}
