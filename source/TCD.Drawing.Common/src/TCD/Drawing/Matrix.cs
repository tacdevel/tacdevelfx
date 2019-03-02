/***************************************************************************************************
 * FileName:             Matrix.cs
  * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System;
using System.Runtime.InteropServices;
using TCD.Native;
using TCD.Numerics.Hashing;

namespace TCD.Drawing
{
    //TODO: ToString() overrides.
    /// <summary>
    /// Defines a transformation, such as a rotation or translation.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public sealed class Matrix : IEquatable<Matrix>
    {
        internal Libui.uiDrawMatrix uiDrawMatrix;

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix"/> class.
        /// </summary>
        public Matrix() => uiDrawMatrix = new Libui.uiDrawMatrix();

        /// <summary>
        /// Represents a specific value in this <see cref="Matrix"/>.
        /// </summary>
        public double M11
        {
            get => uiDrawMatrix.M11;
            set => uiDrawMatrix.M11 = value;
        }

        /// <summary>
        /// Represents a specific value in this <see cref="Matrix"/>.
        /// </summary>
        public double M12
        {
            get => uiDrawMatrix.M12;
            set => uiDrawMatrix.M12 = value;
        }

        /// <summary>
        /// Represents a specific value in this <see cref="Matrix"/>.
        /// </summary>
        public double M21
        {
            get => uiDrawMatrix.M21;
            set => uiDrawMatrix.M21 = value;
        }

        /// <summary>
        /// Represents a specific value in this <see cref="Matrix"/>.
        /// </summary>
        public double M22
        {
            get => uiDrawMatrix.M22;
            set => uiDrawMatrix.M22 = value;
        }

        /// <summary>
        /// Represents a specific value in this <see cref="Matrix"/>.
        /// </summary>
        public double M31
        {
            get => uiDrawMatrix.M31;
            set => uiDrawMatrix.M31 = value;
        }

        /// <summary>
        /// Represents a specific value in this <see cref="Matrix"/>.
        /// </summary>
        public double M32
        {
            get => uiDrawMatrix.M32;
            set => uiDrawMatrix.M32 = value;
        }

        /// <summary>
        /// Sets this <see cref="Matrix"/> structure's identity. After calling this, applying the matrix has no visual sequence. This must be called before any transformations are performed on this <see cref="Matrix"/>.
        /// </summary>
        public void SetIdentity() => Libui.Call<Libui.uiDrawMatrixSetIdentity>()(uiDrawMatrix);

        /// <summary>
        /// Moves paths by the specified amount.
        /// </summary>
        /// <param name="x">The amount to move the path horizontally.</param>
        /// <param name="y">The amount to move the path vertically.</param>
        public void Translate(double x, double y) => Libui.Call<Libui.uiDrawMatrixTranslate>()(uiDrawMatrix, x, y);

        /// <summary>
        /// Scales paths by the specified factors, with a specified scale center.
        /// </summary>
        /// <param name="xCenter">The x-coordinate of the scale center.</param>
        /// <param name="yCenter">The y-coordinate of the scale center.</param>
        /// <param name="x">The x-coordinate of the scale factor.</param>
        /// <param name="y">The y-coordinate of the scale factor.</param>
        public void Scale(double xCenter, double yCenter, double x, double y) => Libui.Call<Libui.uiDrawMatrixScale>()(uiDrawMatrix, xCenter, yCenter, x, y);

        /// <summary>
        /// Rotates paths by the specified radians around the specified points.
        /// </summary>
        /// <param name="x">The x-coordinate of the point.</param>
        /// <param name="y">The y-coordinate of the point.</param>
        /// <param name="amount">The amount to rotate the paths.</param>
        public void Rotate(double x, double y, double amount) => Libui.Call<Libui.uiDrawMatrixRotate>()(uiDrawMatrix, x, y, amount);

        /// <summary>
        /// Skews a path by a specified amount in radians around the specified point.
        /// </summary>
        /// <param name="x">The x-coordinate of the point.</param>
        /// <param name="y">The y-coordinate of the point.</param>
        /// <param name="xamount">The amount to skew the paths horizontally.</param>
        /// <param name="yamount">The amount to skew the paths vertically.</param>
        public void Skew(double x, double y, double xamount, double yamount) => Libui.Call<Libui.uiDrawMatrixSkew>()(uiDrawMatrix, x, y, xamount, yamount);

        /// <summary>
        /// Sets this matrix to the product of itself and the specified matrix.
        /// </summary>
        /// <param name="src">The specified source matrix.</param>
        public void Multiply([In] ref Matrix src) => Multiply(this, src);

        /// <summary>
        /// Sets a matrix to the product of itself and the specified matrix.
        /// </summary>
        /// <param name="dest">The specified destination matrix.</param>
        /// <param name="src">The specified source matrix.</param>
        public static void Multiply([Out]  Matrix dest, [In]  Matrix src) => Libui.Call<Libui.uiDrawMatrixMultiply>()(dest.uiDrawMatrix, src.uiDrawMatrix);

        /// <summary>
        /// Gets a value indicating whether this matrix can be inverted.
        /// </summary>
        /// <returns><see langword="true"/> if the matrix is invertible; else <see langword="false"/>.</returns>
        public bool Invertible() => Libui.Call<Libui.uiDrawMatrixInvertible>()(uiDrawMatrix);

        /// <summary>
        /// Inverts this matrix.
        /// </summary>
        public void Invert() => Libui.Call<Libui.uiDrawMatrixInvert>()(uiDrawMatrix);

        /// <summary>
        /// Gets the transformed point.
        /// </summary>
        /// <returns>The transformed point.</returns>
        public PointD TransformToPoint()
        {
            Libui.Call<Libui.uiDrawMatrixTransformPoint>()(uiDrawMatrix, out double x, out double y);
            return new PointD(x, y);
        }

        /// <summary>
        /// Gets the transformed size.
        /// </summary>
        /// <returns>The transformed size.</returns>
        public SizeD TransformToSize()
        {
            Libui.Call<Libui.uiDrawMatrixTransformSize>()(uiDrawMatrix, out double width, out double height);
            return new SizeD(width, height);
        }

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns>true if obj and this instance are the same type and represent the same value; otherwise, false.</returns>
        public override bool Equals(object obj) => !(obj is Matrix) ? false : Equals((Matrix)obj);

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="matrix">The font to compare with the current instance.</param>
        /// <returns>true if obj and this instance are the same type and represent the same value; otherwise, false.</returns>
        public bool Equals(Matrix matrix) => this == matrix;

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
        public override int GetHashCode() => unchecked(this.GenerateHashCode(M11, M12, M21, M22, M31, M32));

        /// <summary>
        /// Tests whether two specified <see cref="Matrix"/> structures are equivalent.
        /// </summary>
        /// <param name="left">The <see cref="Matrix"/> that is to the left of the equality operator.</param>
        /// <param name="right">The <see cref="Matrix"/> that is to the right of the equality operator.</param>
        /// <returns><see langword="true"/> if the two <see cref="Matrix"/> structures are equal; otherwise, <see langword="false"/>.</returns>
        public static bool operator ==(Matrix left, Matrix right) => left.M11 == right.M11 && left.M12 == right.M12 && left.M21 == right.M21 && left.M22 == right.M22 && left.M31 == right.M31 && left.M32 == right.M32;

        /// <summary>
        /// Tests whether two specified <see cref="Matrix"/> structures are different.
        /// </summary>
        /// <param name="left">The <see cref="Matrix"/> that is to the left of the inequality operator.</param>
        /// <param name="right">The <see cref="Matrix"/> that is to the right of the inequality operator.</param>
        /// <returns><see langword="true"/> if the two <see cref="Matrix"/> structures are different; otherwise, <see langword="false"/>.</returns>
        public static bool operator !=(Matrix left, Matrix right) => !(left == right);
    }
}