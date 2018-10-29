/****************************************************************************
 * FileName:   SurfaceHandler.cs
 * Assembly:   TCD.UI.dll
 * Package:    TCD.UI
 * Date:       20181008
 * License:    MIT License
 * LicenseUrl: https://github.com/tacdevel/TDCFx/blob/master/LICENSE.md
 ***************************************************************************/


namespace TCD.Drawing
{
    /// <summary>
    /// Defines the events for a drawable surface.
    /// </summary>
    public abstract class SurfaceHandler
    {
        /// <summary>
        /// Called when the surface is created or resized.
        /// </summary>
        /// <param name="surface">The surface.</param>
        /// <param name="args">The event data.</param>
        public virtual void Draw(SurfaceBase surface, ref DrawEventArgs args) { }

        /// <summary>
        /// Called when the mouse is moved or clicked over the surface.
        /// </summary>
        /// <param name="surface">The surface.</param>
        /// <param name="args">The event data.</param>
        public virtual void MouseEvent(SurfaceBase surface, ref MouseEventArgs args) { }

        /// <summary>
        /// Called when the mouse entered or left the surface.
        /// </summary>
        /// <param name="surface">The surface.</param>
        /// <param name="left">The event data.</param>
        public virtual void MouseCrossed(SurfaceBase surface, bool left) { }

        /// <summary>
        /// Called when a mouse drag is ended.
        /// </summary>
        /// <param name="surface">The surface.</param>
        public virtual void DragBroken(SurfaceBase surface) { }

        /// <summary>
        /// Called when a key is pressed.
        /// </summary>
        /// <param name="surface">The surface.</param>
        /// <param name="args">The event data.</param>
        public virtual bool KeyEvent(SurfaceBase surface, ref KeyEventArgs args) => false;
    }
}