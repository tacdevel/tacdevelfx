/****************************************************************************
 * FileName:   Context.cs
 * Assembly:   TCD.UI.dll
 * Package:    TCD.UI
 * Date:       20181003
 * License:    MIT License
 * LicenseUrl: https://github.com/tacdevel/TDCFx/blob/master/LICENSE.md
 ***************************************************************************/
 
using TCD.Native;

namespace TCD.Drawing
{
    /// <summary>
    /// Represents 2D rendering context used to draw shapes, text, and other object onto a <see cref="Surface"/> object.
    /// </summary>
    public sealed class Context
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Context"/> class with the specified parent surface.
        /// </summary>
        /// <param name="surface">The surface this context will draw on.</param>
        public Context(Surface surface) => Surface = surface;

        /// <summary>
        /// Gets the <see cref="Drawing.Surface"/> that this <see cref="Context"/> will draw on.
        /// </summary>
        public Surface Surface { get; }

        /// <summary>
        /// Draws a <see cref="Path"/> in this <see cref="Context"/>.
        /// </summary>
        /// <param name="path">The path to draw.</param>
        /// <param name="brush">The brush to use to draw the path.</param>
        /// <param name="stroke">The type of line to use.</param>
        public void Stroke(Path path, Brush brush, StrokeOptions stroke) => Libui.DrawStroke(Surface.Handle, path.Handle, ref brush, ref stroke);

        /// <summary>
        /// Draws a <see cref="Path"/> filled with color in this <see cref="Context"/>.
        /// </summary>
        /// <param name="path">The path to draw.</param>
        /// <param name="brush">The brush to use to draw the path.</param>
        public void Fill(Path path, Brush brush) => Libui.DrawFill(Surface.Handle, path.Handle, ref brush);

        /// <summary>
        /// Clips a <see cref="Path"/> from this <see cref="Context"/>.
        /// </summary>
        /// <param name="path">The path to clip.</param>
        public void Clip(Path path) => Libui.DrawClip(Surface.Handle, path.Handle);

        /// <summary>
        /// Saves the transformations currently applied to this <see cref="Context"/>.
        /// </summary>
        public void Save() => Libui.DrawSave(Surface.Handle);

        /// <summary>
        /// Restores the previously saved transformations to this <see cref="Context"/>.
        /// </summary>
        public void Restore() => Libui.DrawRestore(Surface.Handle);

        /// <summary>
        /// Applies a transform <see cref="Matrix"/> to this <see cref="Context"/>.
        /// </summary>
        /// <param name="matrix"></param>
        public void Transform(Matrix matrix) => Libui.DrawTransform(Surface.Handle, matrix);

        //TODO: Add this back in when finished with Drawing.TextLayout.
        /* /// <summary>
        /// Draws a <see cref="TextLayout"/> at the given location in this <see cref="Context"/>.
        /// </summary>
        /// <param name="layout">The text to draw.</param>
        /// <param name="x">The x-coordinate at which to draw the text.</param>
        /// <param name="y">The y-coordinate at which to draw the text.</param>
        public void DrawText(TextLayout layout, double x, double y) => Libui.DrawText(Surface.Handle, layout, x, y);

        /// <summary>
        /// Draws a <see cref="TextLayout"/> at the given location in this <see cref="Context"/>.
        /// </summary>
        /// <param name="layout">The text to draw.</param>
        /// <param name="location">The location at which to draw the text.</param>
        public void DrawText(TextLayout layout, PointD location) => DrawText(layout, location.X, location.Y); */
    }
}