using Coding4Fun.Kinect.KinectService.WinRTClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RehabTest5
{
    public class GetSkeleton
    {
        /// <summary> ccc
        /// For mapping the 16-bit-per-pixel depth image representation into a displayable RGB image. 
        /// For converting the 16-bit format to a usable 32-bit format
        /// Grayscale: correspond to no detected players
        /// Specific color hues: represent pixels that correspond to different players.
        /// It takes the near, far and unknown depth. Then the correct color based on the distance is calculated.
        /// </summary>
        internal byte[] ConvertDepthFrame(byte[] depthFrame, DepthFrameData args)
        {
            // Color divisors for tinting depth pixels 
            int[] intensityShiftByPlayerR = { 1, 2, 0, 2, 0, 0, 2, 0 };
            int[] intensityShiftByPlayerG = { 1, 2, 2, 0, 2, 0, 0, 1 };
            int[] intensityShiftByPlayerB = { 1, 0, 2, 2, 0, 2, 0, 2 };

            const int RedIndex = 2;
            const int GreenIndex = 1;
            const int BlueIndex = 0;

            byte[] depthFrame32 = new byte[args.ImageFrame.Width * args.ImageFrame.Height * 4];

            // Converts a 16-bit grayscale depth frame which includes player indexes into a 32-bit frame 
            // that displays different players in different colors

            for (int i16 = 0, i32 = 0; i16 < depthFrame.Length && i32 < depthFrame.Length * 4; i16 += 2, i32 += 4)
            {
                short val = (short)(depthFrame[i16] | (depthFrame[i16 + 1] << 8));
                int player = val & args.PlayerIndexBitmask;
                int realDepth = val >> args.PlayerIndexBitmaskWidth;

                // transform 13-bit depth information into an 8-bit intensity appropriate for display
                byte intensity = (byte)(~(realDepth >> 4));

                if (player == 0 && realDepth == 0)
                {
                    // white for near distance (Depth)
                    depthFrame32[i32 + RedIndex] = 255;
                    depthFrame32[i32 + GreenIndex] = 255;
                    depthFrame32[i32 + BlueIndex] = 255;
                }
                else
                {
                    // tint the intensity by dividing by per-player values
                    depthFrame32[i32 + RedIndex] = (byte)(intensity >> intensityShiftByPlayerR[player]);
                    depthFrame32[i32 + GreenIndex] = (byte)(intensity >> intensityShiftByPlayerG[player]);
                    depthFrame32[i32 + BlueIndex] = (byte)(intensity >> intensityShiftByPlayerB[player]);
                }
            }
            return depthFrame32;
        }

        /// <summary>
        /// Scales the specified joint according to the specified dimensions.
        /// </summary>
        /// <param name="joint">The joint to scale.</param>
        /// <param name="width">Width.</param>
        /// <param name="height">Height.</param>
        /// <param name="skeletonMaxX">Maximum X.</param>
        /// <param name="skeletonMaxY">Maximum Y.</param>
        /// <returns>The scaled version of the joint.</returns>
        internal Joint ScaleTo(Joint joint, int width, int height, float skeletonMaxX, float skeletonMaxY)
        {
            joint.Position = new SkeletonPoint()
            {
                X = Scale(width, skeletonMaxX, joint.Position.X),
                Y = Scale(height, skeletonMaxY, -joint.Position.Y),
                Z = joint.Position.Z,
            };

            return joint;
        }

        /// <summary>
        /// Returns the scaled value of the specified position.
        /// </summary>
        /// <param name="maxPixel">Width or height, depending on which coordinate is being scaled.</param>
        /// <param name="maxSkeleton">Border (X or Y), set this to 1.</param>
        /// <param name="position">Original position (X or Y).</param>
        /// Divide by 2 for width and height so point is right in the middle 
        /// instead of in top/left corner
        /// <returns>The scaled value of the specified position.</returns>
        private float Scale(int maxPixel, float maxSkeleton, float position)
        {
            float value = ((((maxPixel / maxSkeleton) / 2) * position) + (maxPixel) / 2);
            if (value > maxPixel)
                return maxPixel;
            if (value < 0)
                return 0;
            return value;
        }
    }
}
